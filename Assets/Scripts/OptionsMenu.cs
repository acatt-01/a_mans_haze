using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenuUI; // Reference to the Panel
    public GameObject pauseMenuUI;   // Reference to the Panel

    //public Slider volumeSlider;      // Reference to the volume slider
    //public Dropdown resolutionDropdown; // Reference to the resolution dropdown

    //public AudioSource backgroundMusic; // Reference to background music (if any)

    // Start is called before the first frame update
    void Start()
    {/*
        // Initialize volume slider and apply the current volume
        volumeSlider.value = backgroundMusic.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Initialize resolution dropdown
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        SetUpResolutionDropdown();*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
        // Show the Options Menu
        public void ShowOptionsMenu()
        {
            pauseMenuUI.SetActive(false);   // Hide pause menu
            optionsMenuUI.SetActive(true);  // Show options menu
        }*/

    // Hide the Options Menu and return to the Pause Menu
    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false); // Hide options menu
        pauseMenuUI.SetActive(true);    // Show pause menu
    }
    /*
        // Change volume based on slider value
        public void ChangeVolume(float volume)
        {
            backgroundMusic.volume = volume; // Adjust the background music volume
            AudioListener.volume = volume;    // Adjust the global volume
        }

        // Set up the available screen resolutions
        void SetUpResolutionDropdown()
        {
            resolutionDropdown.ClearOptions();

            Resolution[] resolutions = Screen.resolutions;
            List<string> options = new List<string>();

            foreach (Resolution resolution in resolutions)
            {
                string option = resolution.width + "x" + resolution.height;
                options.Add(option);
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = options.IndexOf(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
        }

        // Change screen resolution based on dropdown selection
        public void ChangeResolution(int resolutionIndex)
        {
            Resolution[] resolutions = Screen.resolutions;
            Resolution selectedResolution = resolutions[resolutionIndex];

            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }*/
}
