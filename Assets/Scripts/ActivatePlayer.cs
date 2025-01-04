using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivatePlayer : MonoBehaviour
{
    public PlayableDirector cutsceneDirector; // Reference to the PlayableDirector
    public GameObject gameplayController;    // Reference to the gameplay manager or controller
    public GameObject characterDummy;

    private void Start()
    {
        // Ensure gameplay is inactive during the cutscene
        if (gameplayController != null)
        {
            gameplayController.SetActive(false);
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
            gameplayController.SetActive(true);
            characterDummy.SetActive(false);
        }

        // Optionally, disable the cutscene GameObject
        gameObject.SetActive(false);
    }

}
