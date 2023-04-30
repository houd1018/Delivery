using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates { WALK, DEAD }
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 maxBounds;
    private PlayerStates playerStates;

    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;

    // -------- anim bool----------
    bool isWalk;
    bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Walk();
        // SwitchAnimation();
        SwitchStates();
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

        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }
    private void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpForce);
        }
    }

    private void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // walking state check
        bool HasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walk", HasHorizontalSpeed);
    }

    // restrict moving in the camera scene
    private void RestrictMove()
    {
        if (Math.Abs(transform.position.x) > Math.Abs(maxBounds.x))
        {
            // left side
            if (transform.position.x < 0)
            {
                transform.position = new Vector2(-maxBounds.x, transform.position.y);
            }
            else
            {
                // right side
                transform.position = new Vector2(maxBounds.x, transform.position.y);
            }
        }
    }
}
