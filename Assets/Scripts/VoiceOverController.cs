using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverController : MonoBehaviour
{

    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip[] voiceOverClips; // Array of voiceover clips

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Play a specific voiceover by index
    public void PlayVoiceOver(int index)
    {
        if (index >= 0 && index < voiceOverClips.Length)
        {
            audioSource.clip = voiceOverClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("VoiceOver index out of range!");
        }
    }

    // Stop the current voiceover
    public void StopVoiceOver()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
