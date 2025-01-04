using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private CanvasGroup interactableUI;
    private bool isInRange;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered memory object's trigger zone: " + col.gameObject.name);
            interactableUI.gameObject.SetActive(true);
            LeanTween.cancel(interactableUI.gameObject);
            LeanTween.alphaCanvas(interactableUI, 1, 1);
            isInRange = true;
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("E key pressed. Activating cutscene.");
            Activate();
        }
    }

    public virtual void Activate()
    {
        interactableUI.gameObject.SetActive(false);
        LeanTween.alphaCanvas(interactableUI, 0, 1)
            .setOnComplete(UIHide);
    }

    private void UIHide()
    {

    }

    public virtual void Desactivate()
    {

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
}
