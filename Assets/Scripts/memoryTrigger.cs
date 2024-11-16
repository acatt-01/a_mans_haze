using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class memoryTrigger : MonoBehaviour
{

    public float interactionDistance = 2f;
    public LayerMask interactableLayer;
    //private bool isNearMemoryObject = false;
    //private GameObject currentMemoryObject = null;
    private bool hasTriggeredMemory = false;
    private bool hasPressedEbutton = false;
    private bool hasExitZone = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Memory Trigger Script Started");
    }

    // Update is called once per frame
    void Update()
    {
        // Handle memory triggering on E press
        if (!hasPressedEbutton && hasTriggeredMemory && Input.GetKeyDown(KeyCode.E)) // If the player is near the memory object
        {
            Debug.Log("Memory triggered: " + gameObject.name);
            TriggerMemory(gameObject);
        }
    }

    // Detect when the player enters the trigger zone of the memory object
    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggeredMemory) // Only log once
        {
            Debug.Log("OnTriggerEnter: " + other.gameObject.name);
            if (other.CompareTag("MemoryObject"))
            {
                hasTriggeredMemory = true;  // Set the flag to true
                Debug.Log("Entered memory object's trigger zone: " + other.gameObject.name);
                hasExitZone = false;
            }
        }
    }

    // Detect when the player exits the trigger zone of the memory object
    void OnTriggerExit(Collider other)
    {
        if (!hasExitZone) // Only log once
        {
            Debug.Log("OnTriggerExit: " + other.gameObject.name);
            if (other.CompareTag("MemoryObject"))
            {
                hasExitZone = true;
                hasTriggeredMemory = false;  // Reset the flag when the player exits the trigger zone
                Debug.Log("Exited memory object's trigger zone.");
                hasPressedEbutton = false;
            }
        }
    }

    // Trigger memory event (this is where you'd add memory effects, audio, etc.)
    void TriggerMemory(GameObject memoryObject)
    {
        hasPressedEbutton = true;
        Debug.Log("Carreguei no E: " + memoryObject.name);
        // Add memory effects, audio, visuals, etc. here

    }
}
