using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoorInteraction : MonoBehaviour
{

    public GameObject fleeingNPC; // Reference to the AI character
    public float triggerRadius = 2f; // Distance to trigger the door interaction

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && Vector3.Distance(transform.position, Camera.main.transform.position) < triggerRadius && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        Debug.Log("Door opened!");
        if (fleeingNPC != null)
        {
            Debug.Log("Fleeing NPC is not null, triggering flee.");
            fleeingNPC.GetComponent<NPCFleeAI>().RunAway();
        }
        else
        {
            Debug.Log("Fleeing NPC is not assigned.");
        }
    }

}
