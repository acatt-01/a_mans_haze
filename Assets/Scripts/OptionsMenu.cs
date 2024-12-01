/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenuUI; // Reference to the Panel
    public GameObject pauseMenuUI;   // Reference to the Panel
    public GameObject volumeMenuUI;   // Reference to the Panel
    public GameObject resolutionsMenuUI;   // Reference to the Panel

    public Slider volumeSlider;      // Reference to the volume slider
    public TMP_Dropdown resolutionDropdown; // Reference to the resolution dropdown

    public AudioSource backgroundMusic; // Reference to background music (if any)

    // Start is called before the first frame update
    void Start()
    {
        // Initialize volume slider and apply the current volume
        volumeSlider.value = backgroundMusic.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Initialize resolution dropdown
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        SetUpResolutionDropdown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenVolumeMenu()
    {
        optionsMenuUI.SetActive(false);  // Hide Pause Menu
        volumeMenuUI.SetActive(true);  // Show Options Menu
    }

    public void OpenResolutionsMenu()
    {
        optionsMenuUI.SetActive(false);  // Hide Pause Menu
        resolutionsMenuUI.SetActive(true);  // Show Options Menu
    }

    // Hide the Options Menu and return to the Pause Menu
    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false); // Hide options menu
        pauseMenuUI.SetActive(true);    // Show pause menu
    }

    public void BackToOptionsMenu()
    {
        volumeMenuUI.SetActive(false); // Hide options menu
        resolutionsMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);    // Show pause menu
    }

    // Change volume based on slider value
    public void ChangeVolume(float volume)
    {
        backgroundMusic.volume = volume; // Adjust the background music volume
        AudioListener.volume = volume;    // Adjust the global volume
    }

    // Set up the available screen resolutions
    public void SetUpResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        Resolution[] resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        /*if (resolutions.Length == 0)
        {
            options.Add("1920x1080");
            options.Add("1280x720");
            options.Add("1024x768");
        }
        else
        {*/
/*foreach (Resolution resolution in resolutions)
{
    string option = resolution.width + "x" + resolution.height;
    options.Add(option);
}
//}

resolutionDropdown.AddOptions(options);

// Set the current resolution as the default selected option
string currentResolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
resolutionDropdown.value = options.IndexOf(currentResolution);

resolutionDropdown.RefreshShownValue();
}

// Change screen resolution based on dropdown selection
public void ChangeResolution(int resolutionIndex)
{
Resolution[] resolutions = Screen.resolutions;
Resolution selectedResolution = resolutions[resolutionIndex];

// Apply the new resolution
Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

// Optionally save this resolution for the next time the game runs
PlayerPrefs.SetInt("ResolutionWidth", selectedResolution.width);
PlayerPrefs.SetInt("ResolutionHeight", selectedResolution.height);
}
}*/
