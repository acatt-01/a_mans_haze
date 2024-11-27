using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTrigger : MonoBehaviour
{

    public AudioClip newBackgroundSound; // Assign the new background sound for this area
    private AudioSource backgroundAudioSource;
    private AudioClip originalBackgroundSound; // Store the original background sound
    private float originalPlaybackTime; // Store the playback time of the original sound

    // Start is called before the first frame update
    void Start()
    {
        // Find the BackgroundAudioManager in the scene
        backgroundAudioSource = GameObject.Find("BackgroundAudioManager").GetComponent<AudioSource>();
        originalBackgroundSound = backgroundAudioSource.clip;
        originalPlaybackTime = backgroundAudioSource.time;
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
                originalPlaybackTime = backgroundAudioSource.time;
                StartCoroutine(FadeAudio(newBackgroundSound)); // Start fading to the new audio
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player left the area
        {
            if (backgroundAudioSource.clip != originalBackgroundSound) // Avoid unnecessary fade
            {
                StartCoroutine(FadeBackToOriginalAudio()); // Fade back to the original audio
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
        backgroundAudioSource.time = 0f;
        backgroundAudioSource.Play();

        // Fade in the new audio
        for (float volume = 0f; volume <= 1f; volume += Time.deltaTime)
        {
            backgroundAudioSource.volume = volume;
            yield return null;
        }
    }

    // Coroutine to fade out the current audio, restore the original clip and playback time, and fade it in
    IEnumerator FadeBackToOriginalAudio()
    {
        // Fade out the current audio
        for (float volume = backgroundAudioSource.volume; volume >= 0f; volume -= Time.deltaTime)
        {
            backgroundAudioSource.volume = volume;
            yield return null;
        }

        // Change back to the original audio clip and restore playback time
        backgroundAudioSource.clip = originalBackgroundSound;
        backgroundAudioSource.time = originalPlaybackTime;
        backgroundAudioSource.Play();

        // Fade in the original audio
        for (float volume = 0f; volume <= 1f; volume += Time.deltaTime)
        {
            backgroundAudioSource.volume = volume;
            yield return null;
        }
    }
}
