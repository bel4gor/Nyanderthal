using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Assign the Pause Menu UI panel in the Inspector
    public GameObject pauseMenuUI; 
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause menu with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Hide pause menu
        pauseMenuUI.SetActive(false); 
        // Resume game time
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void Pause()
    {
        // Show pause menu
        pauseMenuUI.SetActive(true); 
        // Freeze game time
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void QuitGame()
    {
         // Quit the game
        Application.Quit();
    }
}
