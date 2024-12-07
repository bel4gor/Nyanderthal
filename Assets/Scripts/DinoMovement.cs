using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    // Dino speed
    public float moveSpeed; 
    public bool movingRight = false; 
    public SpriteRenderer spriteRenderer;
    // Dino deals more damage
    public int damage = 2; 

    void Start()
    {
        // Initialize the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = movingRight;
    }

    void Update()
    {
        // Move the Dino horizontally
        float direction = movingRight ? 1 : -1; // 1 for right, -1 for left
        transform.position += Vector3.right * moveSpeed * direction * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            // Flip direction when hitting a boundary
            movingRight = !movingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Crunk takes damage when collided into
            CrunkHealth crunkHealth = collision.gameObject.GetComponent<CrunkHealth>();
            if (crunkHealth != null)
            {
                crunkHealth.TakeDamage(damage);
            }
        }
    }
}