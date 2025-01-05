using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class BackToMainMenu : MonoBehaviour
{
    private void Start()
    {
        // Play the cutscene
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
            cutsceneDirector.stopped += OnCutsceneEnded;
        }
    }
    public PlayableDirector cutsceneDirector;
    // Start is called before the first frame update
    private void OnCutsceneEnded(PlayableDirector director)
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }
}
