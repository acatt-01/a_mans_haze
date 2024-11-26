using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;

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
        } // open the inventory and stop the time
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
    
    public void AddItem(string memoryName, Image memoryImage)
    {
        Debug.Log("memory name = " + memoryName);
    }
}
