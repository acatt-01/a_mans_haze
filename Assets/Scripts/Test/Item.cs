using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string memoryName;
    [TextArea]
    [SerializeField]
    private string memoryDescription;
    [SerializeField]
    private Sprite memoryImage;
    
    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player!");
            inventoryManager.AddItem(memoryName, memoryImage, memoryDescription);
            
        }
    }
}
