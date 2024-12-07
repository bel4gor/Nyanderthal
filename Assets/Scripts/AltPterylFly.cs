using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPterylFly : MonoBehaviour
{
    public float flySpeed;
    public float verticalAmplitude;
    public float verticalFrequency; 
    public SpriteRenderer spriteRenderer;
    private bool movingRight = true;
    private float initialY;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        // Starting Y position
        initialY = transform.position.y; 
    }

    void Update()
    {
        // Horizontal movement
        float direction = movingRight ? 1 : -1;
        transform.position += Vector3.right * flySpeed * direction * Time.deltaTime;

        // Vertical oscillation
        float newY = initialY + Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
