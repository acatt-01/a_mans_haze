using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // ITEM DATA
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;


    // ITEM SLOT
    [SerializeField]
    private Image itemImage;


    public void AddItem(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
    }
}
