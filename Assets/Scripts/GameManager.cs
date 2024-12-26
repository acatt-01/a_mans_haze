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
        Debug.Log("GetSceneLoadProgress started.");
        float fakeProgress = 0f;
        float fakeSpeed = 10f; // Adjust this for how quickly the fake progress grows

        // Start updating fake progress immediately
        while (fakeProgress < 90f)
        {
            fakeProgress += fakeSpeed * Time.deltaTime; // Increment fake progress
            progressBar.current = Mathf.RoundToInt(fakeProgress);
            Debug.Log($"Fake Progress: {fakeProgress}");
            yield return null;
        }

        // Wait for real scene loading to catch up
        while (!AllScenesLoaded())
        {
            totalSceneProgress = 0;

            foreach (AsyncOperation operation in scenesLoading)
            {
                totalSceneProgress += operation.progress;
            }

            totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

            // Ensure fakeProgress syncs with real progress
            fakeProgress = Mathf.Max(fakeProgress, totalSceneProgress);
            progressBar.current = Mathf.RoundToInt(fakeProgress);

            Debug.Log($"Total Progress: {totalSceneProgress}, Fake Progress: {fakeProgress}");
            yield return null;
        }

        // Ensure the bar finishes at 100%
        progressBar.current = 100;
        yield return new WaitForSeconds(0.5f); // Small delay for smooth transition

        loadingScreen.SetActive(false);
        Debug.Log("Loading complete!");
    }

    // Helper to check if all scenes are loaded
    private bool AllScenesLoaded()
    {
        foreach (AsyncOperation operation in scenesLoading)
        {
            if (!operation.isDone)
                return false;
        }
        return true;
    }
}
