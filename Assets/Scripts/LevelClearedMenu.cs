using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelClearedMenu : MonoBehaviour
{
     // Name of the next level/scene to load
    public string nextLevelName;
    public TextMeshProUGUI foodCountText;

    // Duration of the fade-in effect in seconds
    public float fadeDuration; 

    void Start()
    {
        // Retrieve the level-specific food count
        int foodCount = PlayerPrefs.GetInt("FoodCount", 0);

        // Retrieve the cumulative total food count
        int totalFoodCount = PlayerPrefs.GetInt("TotalFoodCount", 0);

        // Set the initial text
        if (foodCountText != null)
        {
            foodCountText.text = "Crunk collected " + foodCount + " food!\nTotal food collected: " + totalFoodCount;

            // Start with the text fully transparent
            Color textColor = foodCountText.color;
            textColor.a = 0f; // Set alpha to 0
            foodCountText.color = textColor;

            // Start the fade-in effect
            StartCoroutine(FadeInText(foodCountText));
        }
    }

    public void ContinueToNextLevel()
    {
        // Reset the level-specific food count for the next level
        PlayerPrefs.SetInt("FoodCount", 0);
        PlayerPrefs.Save();

        // Load the next level
        SceneManager.LoadScene(nextLevelName);
    }

    private IEnumerator FadeInText(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;
        Color textColor = text.color;

        // Gradually increase the alpha value over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            text.color = textColor;
            yield return null;
        }

        // Text fully visible at the end
        textColor.a = 1f;
        text.color = textColor;
    }
}