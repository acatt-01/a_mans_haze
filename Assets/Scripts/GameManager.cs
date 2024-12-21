using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingScreen;
    public ProgressBar progressBar;

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {
        loadingScreen.SetActive(true);
        
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));
    
        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        totalSceneProgress = 0;

        foreach (AsyncOperation operation in scenesLoading)
        {
            operation.allowSceneActivation = false; // Prevent the scene from activating immediately
        }
    
        // Smooth progress bar updates
        while (totalSceneProgress < 0.9f)
        {
            totalSceneProgress = 0;
    
            foreach (AsyncOperation operation in scenesLoading)
            {
                totalSceneProgress += operation.progress; // Accumulate progress (0.0 to 0.9)
            }
    
            totalSceneProgress = totalSceneProgress / scenesLoading.Count; // Average progress
            progressBar.current = Mathf.RoundToInt(totalSceneProgress * 100); // Update progress bar
    
            yield return null;
        }
    
        // Simulate the last 10% of loading progress (0.9 to 1.0)
        float fakeProgress = totalSceneProgress;
        while (fakeProgress < 1.0f)
        {
            fakeProgress += Time.deltaTime * 0.1f; // Incrementally increase progress
            progressBar.current = Mathf.RoundToInt(fakeProgress * 100);
            yield return null;
        }
    
        foreach (AsyncOperation operation in scenesLoading)
        {
            operation.allowSceneActivation = true; // Allow scene activation
        }
    
        loadingScreen.gameObject.SetActive(false);
    }
}
