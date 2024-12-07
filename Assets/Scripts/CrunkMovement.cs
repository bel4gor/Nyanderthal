using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce; 
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
     // Layer for ground detection
    public LayerMask groundLayer;
    // Crunk's feet for ground check
    public Transform feetPosition; 
     // Radius of ground check
    public float groundCheck = 0.1f;
    private bool isGrounded;
    // track jumps
    private int jumpCount = 0;
    // cap for double jumps set in inspector
    public int maxJumps; 

    void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Check if Crunk is on the ground
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheck, groundLayer);

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Handle jumping and double jumping
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
