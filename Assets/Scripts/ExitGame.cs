using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
// This method will be called when the button is clicked
    public void QuitGame()
    {
        // Exits the game when built as an executable
        Application.Quit();
    }
}