using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // The prefab for the food token
    public GameObject foodPrefab;
    // Maximum number of food tokens to spawn set in the inspector
    public int maxFoodTokens; 
    // Bottom-left corner of the spawn area
    public Vector2 spawnAreaMin; 
    // Top-right corner of the spawn area
    public Vector2 spawnAreaMax; 

    // Layer mask to identify the ground
    public LayerMask groundLayer; 
    // Layer mask to identify obstacles
    public LayerMask obstacleLayer; 

    // Maximum distance for the raycast
    public float raycastDistance; 
    // Radius to check for overlap with obstacles
    public float spawnCheckRadius; 
    // Minimum distance between food tokens
    public float minSpawnDistance; 

    // Sound effect for picking up food
    public AudioClip pickupSound; 
    // List to track spawned positions
    private List<Vector2> spawnedPositions = new List<Vector2>();

    private void Start()
    {
        // Spawn food at start of game
        GenerateFoodTokens();
    }

    void GenerateFoodTokens()
{
    // initial food tokens set to zero
    int spawnedTokens = 0;

    while (spawnedTokens < maxFoodTokens)
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        Vector2 rayOrigin = new Vector2(randomX, spawnAreaMax.y);

        // Perform a raycast and exclude the Cloud layer
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Clouds"))
        {
            Vector2 spawnPosition = new Vector2(hit.point.x, hit.point.y + 0.1f);

            if (!Physics2D.OverlapCircle(spawnPosition, spawnCheckRadius, obstacleLayer) &&
                IsFarEnoughFromOthers(spawnPosition))
            {
                // Instantiate food and add the FoodPickup component
                GameObject spawnedFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

                // Add a collider to the spawned food for triggering pickup
                Collider2D foodCollider = spawnedFood.GetComponent<Collider2D>();
                if (foodCollider == null)
                {
                    foodCollider = spawnedFood.AddComponent<CircleCollider2D>();
                    foodCollider.isTrigger = true; // Ensure the collider is set to trigger
                }

                // Add the FoodPickup script for sound and pickup behavior
                FoodPickup foodPickup = spawnedFood.AddComponent<FoodPickup>();
                foodPickup.pickupSound = pickupSound;

                spawnedPositions.Add(spawnPosition);
                spawnedTokens++;
            }
        }
    }
}

    // Check that food items are spawned far enough away
    bool IsFarEnoughFromOthers(Vector2 spawnPosition)
    {
        foreach (Vector2 existingPosition in spawnedPositions)
        {
            if (Vector2.Distance(existingPosition, spawnPosition) < minSpawnDistance)
            {
                return false;
            }
        }
        return true;
    }
}