using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    // Counter for food collected in the current level
    public int foodCount = 0; 
    public TextMeshProUGUI foodCounterText;

    void Start()
    {
        // Reset the level-specific food count to 0 at the start of each level
        PlayerPrefs.SetInt("FoodCount", 0); 
        PlayerPrefs.Save();

        // Initialize the food count
        foodCount = 0;

        // Initialize the UI text
        UpdateFoodCounterUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("food"))
        {
            // Increase the level-specific food count
            foodCount++; 

            // Save to PlayerPrefs
            PlayerPrefs.SetInt("FoodCount", foodCount); 
            // Value is saved persistently
            PlayerPrefs.Save(); 

            // Update the cumulative total food count
            int totalFood = PlayerPrefs.GetInt("TotalFoodCount", 0) + 1;
            PlayerPrefs.SetInt("TotalFoodCount", totalFood);
            PlayerPrefs.Save();

            // Update the UI text
            UpdateFoodCounterUI(); 
            // Destroy the collected food item
            Destroy(collision.gameObject); 
        }
    }

    // Ipdate the on-screen food count during the active level
    void UpdateFoodCounterUI()
    {
        foodCounterText.text = "Food: " + foodCount;
    }
}
