using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameOptions : MonoBehaviour
{
    // UI Elements
    public TMP_Dropdown resolutionDropdown;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;

    // Resolution options
    private Resolution[] resolutions;

    void Start()
    {
        // Load saved settings
        LoadSettings();

        // Populate resolutions dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Add listeners for UI changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    public void LoadSettings()
    {
        // Load saved resolution
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Load saved volume levels
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        masterVolumeSlider.value = savedMasterVolume;

        // Load saved fullscreen preference
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        fullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}
