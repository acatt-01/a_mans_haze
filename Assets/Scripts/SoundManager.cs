using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip laughTrack;
    public AudioClip ambientMemoryMusic;

    private string currentLineAudio = null;
    //private HashSet<string> playedLines = new HashSet<string>();
    //private bool isAudioPlaying = false;

    //private VoiceOverController voiceOverManager;


    // Start is called before the first frame update
    void Start()
    {
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner != null)
        {
            //voiceOverManager = FindObjectOfType<VoiceOverManager>();

            // Hook into Yarn Spinner's line delivery event
            dialogueRunner.AddCommandHandler<string>("playLine", PlayAudioForLine);
            dialogueRunner.AddCommandHandler<string>("stopLine", StopAudioForLine);

            dialogueRunner.AddCommandHandler<string>("playSound", PlaySound);
            dialogueRunner.AddCommandHandler<string>("stopSound", StopSound);
            //dialogueRunner.AddCommandHandler("playVoiceOver", new System.Action<string[]>(PlayVoiceOver));
            dialogueRunner.onDialogueStart.AddListener(ResetAudioState);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ResetAudioState()
    {
        Debug.Log("Resetting audio state for new dialogue.");
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = null;
        currentLineAudio = null;
    }

    private void PlayAudioForLine(string audioLineName)
    {

        if (currentLineAudio == audioLineName && audioSource.isPlaying)
        {
            Debug.Log("Audio is already playing. Ignoring this request.");
            return;
        }

        // Load the audio file
        AudioClip clip = Resources.Load<AudioClip>($"Voice/{audioLineName}");
        if (clip != null)
        {
            Debug.Log($"Playing audio for node: {audioLineName}");

            // Stop any currently playing audio
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = clip;
            audioSource.Play();
            currentLineAudio = audioLineName;
        }
        else
        {
            Debug.LogError($"Audio file not found for node: {audioLineName}");
        }
    }

    public void StopAudioForLine(string audioLineName)
    {
        if (currentLineAudio == audioLineName && audioSource.isPlaying)
        {
            Debug.Log($"Stopping audio for line: {audioLineName}");
            audioSource.Stop();
            audioSource.clip = null;
            currentLineAudio = null;
        }
        else
        {
            Debug.Log($"Audio for line {audioLineName} is not currently playing.");
        }
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "laughTrack":
                audioSource.clip = laughTrack;
                audioSource.Play();
                break;
            case "ambientMemoryMusic":
                audioSource.clip = ambientMemoryMusic;
                audioSource.loop = true; // Loop for ambient sounds
                audioSource.Play();
                break;
            default:
                Debug.LogError("Sound name not recognized: " + soundName);
                break;
        }
    }

    public void StopSound(string soundName)
    {
        if (audioSource.clip != null && audioSource.clip.name == soundName)
        {
            audioSource.Stop();
        }
    }

    /*// New method to play voiceovers (added for integration with Yarn Spinner)
    public void PlayVoiceOver(string[] parameters)
    {
        if (parameters.Length > 0)
        {
            int voiceOverIndex = int.Parse(parameters[0]); // Assuming you pass the index of the voiceover to play
            voiceOverManager.PlayVoiceOver(voiceOverIndex);
        }
    }*/
}
