using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    // The sound effect to play when picked up
    public AudioClip pickupSound; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collision tag is Player
        if (collision.CompareTag("Player")) 
        {
            // Play the sound effect
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destroy the food object after pickup
            Destroy(gameObject);
        }
    }
}