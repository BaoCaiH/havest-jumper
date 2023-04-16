using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int extraJumpsValue = 1;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 28f;
    [SerializeField] private float checkRadius = 0.5f;
    [SerializeField] private float wallSlidingSpeed = 5f;
    [SerializeField] private float wallForceX = 7f;
    [SerializeField] private float wallForceY = 24f;
    [SerializeField] private float wallJumpTime = 0.05f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;

    private int extraJumps;
    private int animationState;
    private float inputHorizontal;
    //private float inputVertical;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isWalled;
    private bool isWallSliding;
    private bool isWallJumping;
    private Rigidbody2D rb;
    private Animator anim;
    //private enum AnimationState { idle, run, jump, fall, doubleJump, wallJump };


    // Start is called before the first frame update
    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update direction
        inputHorizontal = Input.GetAxis("Horizontal");
        //inputVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
        if (isFacingRight ^ inputHorizontal > 0 && inputHorizontal != 0) { Flip(); }

        // Update ground
        isGrounded = IsGrounded();
        // Update wall
        isWalled = IsWalled();
        isWallSliding = IsWallSliding();

        // Reset Jumps
        if (isGrounded || isWalled)
        {
            extraJumps = extraJumpsValue;
        }

        // Verticle movement
        if (KeyJumpDown() && extraJumps > 0 && !isWallSliding)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (KeyJumpDown() && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // Wall sliding
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        // Wall jumping
        if (KeyJumpDown() && isWallSliding)
        {
            isWallJumping = true;
            Invoke("StopWallJump", wallJumpTime);
        }
        if (isWallJumping)
        {
            rb.velocity = new Vector2(wallForceX * -inputHorizontal, wallForceY);
        }

        UpdateAnimation();
    }

    private void FixedUpdate() { }

    private void Flip()
    {
        // Instead of flipping sprite, this will flip the collision box as well
        //sprite.flipX = !sprite.flipX;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
        isFacingRight = !isFacingRight;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, checkRadius, wall);
    }

    private bool IsWallSliding()
    {
        return isWalled && !isGrounded && inputHorizontal != 0;
    }

    private bool KeyJumpDown()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private void UpdateAnimation()
    {
        if (inputHorizontal != 0 && isGrounded)
        {
            animationState = 1;
        }
        else if (rb.velocity.y > 0 && extraJumps > 0)
        {
            animationState = 2;
        }
        else if (rb.velocity.y < 0 && !isWallSliding)
        {
            animationState = 3;
        }
        else if (rb.velocity.y > 0 && extraJumps == 0)
        {
            animationState = 4;
        }
        else if (isWallSliding)
        {
            animationState = 5;
        }
        else
        {
            animationState = 0;
        }
        anim.SetInteger("animationState", animationState);
    }
}
