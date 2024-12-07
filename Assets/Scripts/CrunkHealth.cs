using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrunkHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int health;

    // Array to hold the PawLife images
    public Image[] pawLifeImages; 
    public AudioClip catSound;
    public AudioClip damageSound;
    public AudioSource audioSource;

    private void Start()
    {
        // Sets player health at the start
        health = PlayerPrefs.GetInt("CrunkHealth", maxHealth);
        // Initialize the UI at the start
        UpdateHealthUI();
    }

    // Method to take damage
  public void TakeDamage(int damage)
    {
        // Decrease health by damage amount
        health -= damage; 
        Debug.Log("Crunk health: " + health);

        // Play the damage sound
        if (catSound != null && damageSound != null)
        {
            audioSource.PlayOneShot(catSound);
            audioSource.PlayOneShot(damageSound);
        }

        // Update the UI after taking damage
        UpdateHealthUI();

        // Save the updated health to PlayerPrefs
        PlayerPrefs.SetInt("CrunkHealth", health);

        if (health <= 0)
        {
            // Call GameOver when health reaches zero
            GameOver();
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < pawLifeImages.Length; i++)
        {
            pawLifeImages[i].gameObject.SetActive(i < health);
        }
    }

    private void GameOver()
    {
        // Reset health in PlayerPrefs for a new game
        PlayerPrefs.DeleteKey("CrunkHealth");
        SceneManager.LoadScene("GameOver");
    }
}