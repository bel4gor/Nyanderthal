using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCloud : MonoBehaviour
{
    // Speed at which the cloud moves
    public float moveSpeed; 
    // Distance the cloud moves in one direction
    public float moveDistance;
    // Sets starting position of the cloud
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        // Store the initial position of the cloud
        startPosition = transform.position;
    }

    void Update()
    {
        MoveCloud();
    }

    void MoveCloud()
    {
        // Calculate the target positions
        float rightLimit = startPosition.x + moveDistance;
        float leftLimit = startPosition.x - moveDistance;

        // Move the cloud in the current direction
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // If the cloud reaches the right limit, reverse direction
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // If the cloud reaches the left limit, reverse direction
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the cloud so it moves with the platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unparent the player when they leave the platform
            collision.transform.SetParent(null);
        }
    }
}
