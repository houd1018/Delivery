using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounceBlock : BaseBlock
{
    [SerializeField] float jumpHeight = 9f;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    protected override void Start()
    {
        base.Start();
        // EnableMaterial();
    }
    protected override void Update()
    {
        base.Update();
        checkBlockBounce();
    }

    void checkBlockBounce()
    {
        if (IsPlayerStand)
        {
            playerRB = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponentInChildren<Animator>();
            bool HasVerticalSpeed = Mathf.Abs(playerRB.velocity.y) > Mathf.Epsilon;

            if (!HasVerticalSpeed)
            {
                playerRB.velocity += new Vector2(0f, jumpHeight);
                playerAnimator.SetTrigger("Jump");
            }
        }

    }

    void EnableMaterial()
    {
        PhysicsMaterial2D material2D = Resources.Load<PhysicsMaterial2D>("Material/Bounce");
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = material2D;
    }




}
