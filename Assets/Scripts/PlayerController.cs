using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int extraJumpsValue = 1;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkRadius;
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float wallForceX;
    [SerializeField] private float wallForceY;
    [SerializeField] private float wallJumpTime;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;

    private int extraJumps;
    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isWalled;
    private bool isWallSliding;
    private bool isWallJumping;
    private Rigidbody2D rb;
    //private SpriteRenderer sprite;


    // Start is called before the first frame update
    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update direction
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        if (isFacingRight ^ horizontalInput > 0) { Flip(); }

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
        if (GetKeyJump() && extraJumps > 0 && !isWallSliding)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (GetKeyJump() && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // Wall sliding
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        // Wall jumping
        if (GetKeyJump() && isWallSliding)
        {
            isWallJumping = true;
            Invoke("StopWallJump", wallJumpTime);
        }
        if (isWallJumping)
        {
            rb.velocity = new Vector2(wallForceX * -horizontalInput, wallForceY);
        }
    }

    void FixedUpdate()
    {
        //// Update ground
        //isGrounded = IsGrounded();
        //// Update direction
        //horizontalInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        //if (sprite.flipX ^ horizontalInput < 0) { Flip(); }
    }

    void Flip()
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
        return isWalled && !isGrounded && horizontalInput != 0;
    }

    private bool GetKeyJump()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }
}
