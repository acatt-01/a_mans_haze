using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject MainMenuScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneId)
    {
        MainMenuScreen.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        
        

        float progress = 0f;

        while (!operation.isDone)
        {
            LoadingScreen.SetActive(true);
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);
            LoadingBarFill.fillAmount = progress;

            if (operation.progress >= 0.9f)
            {
                // Hold until activation
                LoadingBarFill.fillAmount = 1f;
                operation.allowSceneActivation = true;
            }


            yield return null;
        }
    }
}
