using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // --------- ITEM DATA ----------
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public Sprite emptySprite;
    public bool isFull;


    // ---------- ITEM SLOT ----------
    [SerializeField]
    private Image itemImage;
    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    // ----- ITEM DESCRIPTION SLOT ----------
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemDescription)
    {
        
        this.itemDescription = itemDescription;

        isFull = true;

        itemImage.sprite = itemSprite;
    }

    //string itemName, Sprite itemSprite, 
    //this.itemSprite = itemSprite;
    //this.itemName = itemName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnRightClick()
    {
        throw new NotImplementedException();
    }

    private void OnLeftClick()
    {
        // deselected all slots instead of focusing on deselecting only the previous selected because its simpler
        // turns on the selected shader for the slot we're clicking
        // annoints item as selected
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;

        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;

        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }
}
