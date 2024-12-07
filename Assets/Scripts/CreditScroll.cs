using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    // Speed at which the credits scroll in the Inspector
    public float scrollSpeed; 
     // Assign the RectTransform of the credits text in the Inspector
    public RectTransform creditsText;
     // Top boundary of the credits area in the Inspector
    public float topBoundaryY;
    // Name of the scene to load after credits
    public string nextScene;

    void Update()
    {
        if (creditsText != null)
        {
            // Check if the text has not yet reached the top boundary
            if (creditsText.localPosition.y < topBoundaryY)
            {
                // Move the credits text upward
                creditsText.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
            }
            else
            {
                // Stop scrolling and load the next scene
                if (!string.IsNullOrEmpty(nextScene))
                {
                    SceneManager.LoadScene(nextScene);
                }
            }
        }
    }
}