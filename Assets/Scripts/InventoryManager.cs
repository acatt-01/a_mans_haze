using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public bool menuActivated;
    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        menuActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // original state, inactive menu
        inventoryMenu.SetActive(menuActivated);

        // if I is pressed and menu is already activated, deactivate and make time pass again
        if (Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
            // Reset settings
            AudioListener.pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        } // open the inventory and stop the time
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;
        }
    }
    
    // add item to inventory
    public void AddItem(string memoryName, Sprite memoryImage, string memoryDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(memoryName, memoryImage, memoryDescription);
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
