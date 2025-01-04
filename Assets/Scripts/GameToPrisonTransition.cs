using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class GameToPrisonTransition : Interactable
{
    //[SerializeField] private GameObject cutsceneToPlay;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //cutsceneToPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadPrisonScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Prisao");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public override void Activate()
    {
        base.Activate();
        LoadPrisonScene();
        player.SetActive(false);
    }

    public override void Desactivate()
    {
        base.Desactivate();
        player.SetActive(true);
    }
}