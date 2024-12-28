using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBlock : MonoBehaviour
{
    public GameObject BlockOnPlayer;

    void Start()
    {
        BlockOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            // Check if 'E' key is held down to pick up the Block
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
                BlockOnPlayer.SetActive(true);
            }
        }
    }

}
