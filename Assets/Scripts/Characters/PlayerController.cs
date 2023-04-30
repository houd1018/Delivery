using Isekai.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { WALK, DEAD, JUMP }
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float respawnDelay = 3f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 maxBounds;
    private CharacterStats characterStats;
    private PlayerStates playerStates;
    private Vector3 postionThreeSecondsBefore;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;

    bool isDead;
    bool hasDamaged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponentInChildren<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        StartCoroutine(FindPositionThreeSceondsBefore());
    }

    private void FixedUpdate()
    {

    }
    private void Update()
    {
        isDead = characterStats.CurrentHealth == 0;
        Walk();
        SwitchStates();
        FlipSprite();
        TopTrapDamage();
        Fall();
        checkGameStarted();
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
            StartCoroutine(WaitforTopTrapDamage());
        }

    }
    void Fall()
    {
        if (isDead) { return; }
        if (transform.position.y <= Camera.main.ScreenToWorldPoint(Vector3.zero).y)
        {
            transform.position = postionThreeSecondsBefore;
            characterStats.CurrentHealth -= 1;
        }
    }
    void checkGameStarted()
    {
        if (Game.Instance != null)
        {
            if (GameModel.Instance.GameStarted)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

    }

    IEnumerator WaitforTopTrapDamage()
    {
        yield return new WaitForSecondsRealtime(2f);
        hasDamaged = false;
    }

    // update respwan point which is at the postion x seconds ago
    IEnumerator FindPositionThreeSceondsBefore()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnDelay);
            postionThreeSecondsBefore = transform.position;
        }
    }



}
