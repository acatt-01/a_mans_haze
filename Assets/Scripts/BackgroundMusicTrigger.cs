using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTrigger : MonoBehaviour
{

    public AudioClip newBackgroundSound; // Assign the new background sound for this area
    private AudioSource backgroundAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Find the BackgroundAudioManager in the scene
        backgroundAudioSource = GameObject.Find("BackgroundAudioManager").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered the area
        {
            if (backgroundAudioSource.clip != newBackgroundSound) // Avoid restarting the same clip
            {
                StartCoroutine(FadeAudio(newBackgroundSound)); // Start fading to the new audio
            }
        }
    }

    // Coroutine to fade out the current audio, change the clip, and fade in the new audio
    IEnumerator FadeAudio(AudioClip newClip)
    {
        // Fade out the current audio
        for (float volume = backgroundAudioSource.volume; volume >= 0f; volume -= Time.deltaTime)
        {
            backgroundAudioSource.volume = volume;
            yield return null;
        }

        // Change to the new audio clip and play it
        backgroundAudioSource.clip = newClip;
        backgroundAudioSource.Play();

        // Fade in the new audio
        for (float volume = 0f; volume <= 1f; volume += Time.deltaTime)
        {
            backgroundAudioSource.volume = volume;
            yield return null;
        }
    }
}
