using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MemoryObject : MonoBehaviour
{
    public string memoryDescription; // Memory description specific to this object
    public string yarnNode;          // Yarn node for dialogue

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered trigger zone of: " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited trigger zone of: " + gameObject.name);
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }
}

