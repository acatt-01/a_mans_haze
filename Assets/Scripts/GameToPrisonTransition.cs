using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class GameToPrisonTransition : MonoBehaviour
{
    //[SerializeField] private GameObject cutsceneToPlay;
    GameObject player;

    [SerializeField] private CanvasGroup interactableUI;
    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //cutsceneToPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("E key pressed. Activating transition to prison scene.");
            Activate();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger entered.");
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered memory object's trigger zone: " + col.gameObject.name);
            interactableUI.gameObject.SetActive(true);
            LeanTween.cancel(interactableUI.gameObject);
            LeanTween.alphaCanvas(interactableUI, 1, 1);
            isInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exited object's trigger zone.");
            isInRange = false;
            interactableUI.gameObject.SetActive(false);
        }
    }

    public void LoadPrisonScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Prisao");

        Debug.Log("Loading scene...");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Activate()
    {
        interactableUI.gameObject.SetActive(false);
        LeanTween.alphaCanvas(interactableUI, 0, 1)
            .setOnComplete(UIHide);

        //base.Activate();
        Debug.Log("Activating transition to prison scene.");
        LoadPrisonScene();
        player.SetActive(false);
    }

    private void UIHide()
    {

    }

    public void Desactivate()
    {
        //base.Desactivate();
        player.SetActive(true);
    }
}