using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    // Function called when button is clicked
    public void LoadHomeScreen()
    {
        // Return to HomeScreen
        SceneManager.LoadScene("HomeScreen");
    }
}
