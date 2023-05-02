using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { WALK, DEAD, JUMP }
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int respawnBlockDistance = 2;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float respawnDelay = 3f;

    [SerializeField] private float dashDuration = 0.01f;
    [SerializeField] private float dashSpeed = 2f;

    private bool isDashing;
    private Queue<Transform> blockQueue;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 maxBounds;
    private CharacterStats characterStats;
    private PlayerStates playerStates;
    private TrailRenderer trailRenderer;
    private Vector3 postionThreeSecondsBefore; // Obsolete
    Ray2D middleRay;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    Vector3 respawnMiddlePoint;

    bool isDead;
    bool hasDamaged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponentInChildren<Animator>();
        characterStats = GetComponent<CharacterStats>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        blockQueue = new Queue<Transform>(respawnBlockDistance);
    }

    private void FixedUpdate()
    {

    }
    private void Update()
    {
        isDead = characterStats.CurrentHealth <= 0;
        Walk();
        SwitchStates();
        FlipSprite();
        TopTrapDamage();
        Fall();
        // CheckDash();
        checkGameStarted();
        checkIsDead();
        // checkBlockInMiddle();
    }
    void SwitchStates()
    {
        switch (playerStates)
        {
            case PlayerStates.WALK:
                // do things in the WALK
                break;
            case PlayerStates.DEAD:
                // do things when player is dead
                break;
            case PlayerStates.JUMP:
                // do thigns when jumping
                break;
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }
    private void OnJump(InputValue value)
    {
        if (isDead) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpForce);
            myAnimator.SetTrigger("Jump");
            playerStates = PlayerStates.JUMP;
        }
    }

    private void Walk()
    {
        if (isDead) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // walking state check
        bool HasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walk", HasHorizontalSpeed);
        playerStates = PlayerStates.WALK;
    }

    void FlipSprite()
    {
        if (isDead) { return; }
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(-(rb.velocity.x)), 1f);
        }
    }

    void TopTrapDamage()
    {
        if (isDead) { return; }
        if (!hasDamaged && transform.position.y >= Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
        {
            hasDamaged = true;
            characterStats.CurrentHealth -= 1;
            StartCoroutine(WaitforTrapDamage());
        }

    }

    // Fall and Respawn
    void Fall()
    {
        if (isDead) { return; }
        if (!hasDamaged && transform.position.y <= Camera.main.ScreenToWorldPoint(Vector3.zero).y)
        {
            hasDamaged = true;
            Vector3 offset = new Vector3(0f, 1f, 0f);
            Vector3 respawnPoint = blockQueue.Peek().position + offset;
            transform.position = respawnPoint;
            characterStats.CurrentHealth -= 1;
            StartCoroutine(WaitforTrapDamage());
        }
    }
    void checkGameStarted()
    {
        /*        if (Game.Instance != null)
                {
                    if (GameModel.Instance.GameStarted)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }
                    else
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    }
                }*/

    }
    void checkIsDead()
    {
        var dialogues = Resources.Load<DialogueDatas>("Data/RandomDeathDialogue/RandomDeathDialogues");
        if (isDead && GameModel.Instance.GameStarted)
        {
            Game.Instance.PauseGame();
            DialogueManager.Instance.PushMessages(dialogues.Dialogues[UnityEngine.Random.Range((int)0, dialogues.Dialogues.Length)].Dialogues,
                () =>
                {
                    EventSystem.Instance.SendEvent<GameOverEvent>(typeof(GameOverEvent), new GameOverEvent());
                    var popup = PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup,
                        new PopupData()
                        {
                            OnCancelClicked = Game.Instance.OnClickBackToMenu,
                            OnConfirmClicked = OnClickPlayAgain
                        });
                    popup.SetTitle("Mission Failed");
                    popup.SetConfirmButton("Retry");
                    popup.SetCancelButton("Back");
                });
        }
    }
    public void OnClickPlayAgain()
    {
        LevelManager.Instance.TransitionToScene("Heaven", () =>
        {
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
            var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
            playerdata.currentHealth = 1;
            playerdata.maxHealth = 1;
            ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
            new HUDScreenViewModel(playerdata)
            {

            });
        }, 0, "Tips: Use WASD to move and click to continue with dialogues").Forget();
    }
    // void CheckDash()
    // {
    //     if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
    //     {
    //         Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Math.Abs(Input.GetAxisRaw("Vertical")));
    //         StartCoroutine(Dash(direction));
    //     }
    // }
    IEnumerator WaitforTrapDamage()
    {
        yield return new WaitForSecondsRealtime(2f);
        hasDamaged = false;
    }

    // Obsolete
    // update respwan point which is at the postion x seconds ago
    IEnumerator FindPositionThreeSceondsBefore()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnDelay);
            postionThreeSecondsBefore = transform.position;
        }
    }
    // IEnumerator Dash(Vector2 direction)
    // {
    //     isDashing = true;
    //     trailRenderer.emitting = true;
    //     myFeetCollider.enabled = false;
    //     myBodyCollider.enabled = false;

    //     rb.AddForce(direction.normalized * dashSpeed, ForceMode2D.Impulse);
    //     yield return new WaitForSeconds(dashDuration);

    //     isDashing = false;
    //     trailRenderer.emitting = false;
    //     myFeetCollider.enabled = true;
    //     myBodyCollider.enabled = true;
    // }

    private void checkBlockInMiddle()
    {
        middleRay = new Ray2D(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 10, Screen.height / 2, 0)), transform.right.normalized);
        Debug.DrawRay(middleRay.origin, middleRay.direction * 4.5f, Color.yellow);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(middleRay.origin, middleRay.direction * 4.5f);
        if (raycastHit2D)
        {
            GameObject hitObject = raycastHit2D.collider.gameObject;
            // if (hitObject.layer == LayerMask.GetMask("Ground"))
            // {
                Vector3 offset = new Vector3(0f, 1f, 0f);
                Debug.Log("Hit object: " + hitObject.name);
                respawnMiddlePoint =  hitObject.transform.position + offset;
            // }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (!blockQueue.Contains(other.gameObject.transform))
            {
                blockQueue.Enqueue(other.gameObject.transform);
            }
            if (blockQueue.Count > respawnBlockDistance)
            {
                blockQueue.Dequeue();
            }
        }
    }


}
