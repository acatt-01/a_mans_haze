using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text memoryText; // Drag and drop your TextMeshPro UI element here
    [SerializeField] private int totalMemories; // Total memories to check

    private int memoriesCompleted = 0; // Counter for completed memories

    public void MarkMemoryComplete()
    {
        memoriesCompleted++; // Increment the counter
        UpdateMemoryText(); // Update the displayed text
    }

    private void UpdateMemoryText()
    {
        // Update the UI text with the new memory count
        memoryText.text = $"Memorys: {memoriesCompleted}/{totalMemories}";
        memoryText.fontSize = 15;
    }

    public int getTotalMemories()
    {
        return totalMemories;
    }

    public int getCompletedMemories()
    {
        return memoriesCompleted;
    }
}
