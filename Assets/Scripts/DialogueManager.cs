using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueCanvas;  // Reference to the dialogue Canvas

    //public DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    void Start()
    {
        /*dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner not found!");
        }*/

        // Hide the dialogue initially
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerDialogue(string dialogueName)
    {
        Debug.Log("TriggerDialogue entrei!!");
        dialogueCanvas.SetActive(true);  // Show the dialogue UI
        //dialogueRunner.StartDialogue(dialogueName);  // Start the dialogue
    }

    // You can add this to close the dialogue manually if needed
    public void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
