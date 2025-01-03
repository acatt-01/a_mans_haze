using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    private TextMeshProUGUI messageText; // Reference to the Text component

    void Start()
    {
        // Find the Text component on the same GameObject
        messageText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string newText)
    {
        //messageText.text = newText;
        messageText.SetText(newText);

    }
}
