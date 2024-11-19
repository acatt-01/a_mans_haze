using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenuUI; // Reference to the Panel
    public GameObject optionsMenuUI;
    private bool isPaused = false;
    //public Material blurMaterial;
    //public RawImage backgroundImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (optionsMenuUI.activeSelf) // If the Options Menu is active, don't listen to Escape key
        {
            return; // Skip the rest of the code in Update() when Options Menu is active
        }

        // Check for Escape or Enter key press to resume
        if (isPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)))
        {
            ResumeGame(); // Call ResumeGame if either key is pressed
        }

        // Toggle pause on Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f;        // Freeze the game



        isPaused = true;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
    }

    public void OpenOptionsMenu()
    {
        pauseMenuUI.SetActive(false);  // Hide Pause Menu
        optionsMenuUI.SetActive(true);  // Show Options Menu
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;          // Resume the game
        isPaused = false;



        // Reset settings
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        // Reset settings
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    /* public void BackToPauseMenu()
     {
         optionsMenuUI.SetActive(false);  // Hide Options Menu
         pauseMenuUI.SetActive(true);    // Show Pause Menu
     }*/

    public void QuitGame()
    {
        // Reset settings
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Quitting game...");
        Application.Quit(); // Quit the application (works only in build)
    }
}
