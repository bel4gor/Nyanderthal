using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PterylFly : MonoBehaviour
{
    // Speed at which the Pteryl flies
    public float flySpeed; 
    // Reference to the SpriteRenderer
    public SpriteRenderer spriteRenderer;

    // Track the current movement direction
    private bool movingRight = true;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        // Move the Pteryl horizontally based on the current direction
        float direction = movingRight ? 1 : -1;
        transform.position += Vector3.right * flySpeed * direction * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is tagged as "Boundary"
        if (collision.gameObject.CompareTag("Boundary"))
        {
            // Flip the sprite on the X-axis
            spriteRenderer.flipX = !spriteRenderer.flipX;

            // Reverse the movement direction
            movingRight = !movingRight;
        }
    }
}
