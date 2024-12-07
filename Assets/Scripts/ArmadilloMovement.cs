using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveDistance;
    public int damage;
    private Vector3 startPosition;
    private bool movingRight = true;

    private void Start()
    {
        // Store the starting position of the Armadillo
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveArmadillo();
    }

    void MoveArmadillo()
    {
        // Calculate the movement boundaries
        float rightLimit = startPosition.x + moveDistance;
        float leftLimit = startPosition.x - moveDistance;

        // Move the Armadillo based on its current direction
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Reverse direction if the right limit is reached
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                // Flip the sprite to face the other direction
                FlipSprite(); 
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // Reverse direction if the left limit is reached
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                // Flip the sprite to face the other direction
                FlipSprite(); 
            }
        }
    }

    void FlipSprite()
    {
        // Flip the Armadillo's sprite horizontally
        Vector3 scale = transform.localScale;
        // Reverse the X scale to flip the sprite
        scale.x *= -1; 
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player on collision
            CrunkHealth playerHealth = collision.gameObject.GetComponent<CrunkHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
