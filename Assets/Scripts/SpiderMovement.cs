using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    // Horizontal range for movement
    public float moveRangeX; 
    // Vertical range for jumping
    public float moveRangeY; 
    // Speed of movement
    public float moveSpeed;
     // Force for jumping
    public float jumpForce;
    // Time to pause between movements
    public float idleTime = 2f; 

    // Layer for detecting ground
    public LayerMask groundLayer; 

    // Sound to play when the spider takes damage
    public AudioClip damageSound;
     // Loot to drop when the spider is destroyed
    public GameObject foodPrefab;
    public AudioSource audioSource;

    // The spider's starting position
    private Vector2 startPosition; 
    private Rigidbody2D rb;
    private bool isMoving = false;
    private bool isGrounded = false;

    void Start()
    {
        // Record the starting position
        startPosition = transform.position; 
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>(); 
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        // Start random movement routine
        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        // Check if the spider is on the ground
        CheckGrounded(); 
    }

    void CheckGrounded()
    {
        // Use a raycast to check if the spider is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
        isGrounded = hit.collider != null;
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            if (!isMoving)
            {
                isMoving = true;

                // Randomize movement direction and distance
                float randomX = Random.Range(-moveRangeX, moveRangeX);
                float targetX = Mathf.Clamp(startPosition.x + randomX, startPosition.x - moveRangeX, startPosition.x + moveRangeX);

                // Horizontal movement
                while (Mathf.Abs(transform.position.x - targetX) > 0.1f)
                {
                    float direction = targetX > transform.position.x ? 1f : -1f;
                    rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
                    yield return null;
                }
                // Stop horizontal movement
                rb.velocity = new Vector2(0, rb.velocity.y);

                // Random jump has 50% chance of occuring
                if (isGrounded && Random.value > 0.5f) 
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                // Idle for a moment
                yield return new WaitForSeconds(idleTime);

                isMoving = false;
            }
        }
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.collider.CompareTag("Boundary"))
    {
        // Reverse the spider's horizontal velocity
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    else if (collision.collider.CompareTag("Player"))
    {
        Rigidbody2D crunkRb = collision.collider.GetComponent<Rigidbody2D>();
        Vector2 contactPoint = collision.GetContact(0).point;
        Vector2 spiderCenter = GetComponent<Collider2D>().bounds.center;

        // Check if Crunk's feet are above the spider's center
        float crunkBottom = collision.collider.bounds.min.y;
        float spiderTop = spiderCenter.y + (GetComponent<Collider2D>().bounds.size.y / 2);

        if (crunkBottom > spiderTop)
        {
            // Crunk jumps on the spider and spider is destroyed
            DestroySpider();
            if (crunkRb != null)
            {
                // Bounce Crunk upward
                crunkRb.velocity = new Vector2(crunkRb.velocity.x, 10f); 
            }
        }
        else
        {
            // Spider damages Crunk
            CrunkHealth crunkHealth = collision.collider.GetComponent<CrunkHealth>();
            if (crunkHealth != null)
            {
                crunkHealth.TakeDamage(1);
            }
        }
    }
}

private void DestroySpider()
{
    // Drop food at spider's position
    if (foodPrefab != null)
{
    Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
    Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
}

    // Play damage sound on a temporary GameObject
    if (damageSound != null)
    {
        GameObject tempAudio = new GameObject("SpiderDamageSound");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.clip = damageSound;
        tempAudioSource.Play();

        // Destroy the temporary GameObject after the sound finishes
        Destroy(tempAudio, damageSound.length);
    }

    // Destroy the spider GameObject immediately
    Destroy(gameObject);
}
}
