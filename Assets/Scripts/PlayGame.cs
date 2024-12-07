using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    // Scene to load from inspector
    public string SceneLoad; 

    // Crunk's maximum health
    public int maxHealth = 5;

    void Start()
{
            // Reset level-specific food count
            PlayerPrefs.SetInt("FoodCount", 0); 
            // Reset cumulative total food count
            PlayerPrefs.SetInt("TotalFoodCount", 0); 
            // Reset Crunk's health to full when starting from the Main Menu
            PlayerPrefs.SetInt("CrunkHealth", maxHealth);
            
            // Save values persistently
            PlayerPrefs.Save(); 
         Debug.Log("Food counts reset in Main Menu. TotalFoodCount: " + PlayerPrefs.GetInt("TotalFoodCount"));
}

    public void LoadScene()
    {
        // Load the scene from the inspector
        SceneManager.LoadScene(SceneLoad);
    }
}