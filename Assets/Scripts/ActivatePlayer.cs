using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivatePlayer : MonoBehaviour
{
    public PlayableDirector cutsceneDirector; // Reference to the PlayableDirector
    public GameObject gameplayController;    // Reference to the gameplay manager or controller
    public GameObject characterDummy;
    public GameObject custsceneCamera;
    public GameObject playerCamera;

    private Vector3 originalPosition;        // Store the original position of the gameplay controller
    private Vector3 cutscenePosition = new Vector3(100f, 1.15f, 100f); // Far away position

    private void Start()
    {
        // Ensure gameplay is inactive during the cutscene
        if (gameplayController != null)
        {
            originalPosition = gameplayController.transform.position;
            gameplayController.transform.position = cutscenePosition; // Move out of scene
            playerCamera.SetActive(false);
        }

        // Play the cutscene
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
            cutsceneDirector.stopped += OnCutsceneEnded;
        }
    }

    private void OnCutsceneEnded(PlayableDirector director)
    {
        // Activate gameplay once the cutscene ends
        if (gameplayController != null)
        {
            gameplayController.transform.position = originalPosition;
            //gameplayController.SetActive(true);
            characterDummy.SetActive(false);
            custsceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }

        // Optionally, disable the cutscene GameObject
        //gameObject.SetActive(false);
    }

}
