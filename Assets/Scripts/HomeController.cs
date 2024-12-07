using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    // Name of the scene to load when Crunk enters home
    public string nextLevelName;
    // Tracks if Crunk is in the home zone
    private bool isInHomeZone = false; 

    void Update()
    {
        if (isInHomeZone && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            GoHome();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInHomeZone = true;
            Debug.Log("Crunk has reached home. Press 'W' or Up Arrow to enter.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInHomeZone = false;
        }
    }

    private void GoHome()
    {
        Debug.Log("Crunk has entered his home!");
         // Load the specified next level in inspector
        SceneManager.LoadScene(nextLevelName);
    }
}