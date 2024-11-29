using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip laughTrack;
    public AudioClip ambientMemoryMusic;

    //private VoiceOverController voiceOverManager;


    // Start is called before the first frame update
    void Start()
    {
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        //voiceOverManager = FindObjectOfType<VoiceOverManager>();

        // Hook into Yarn Spinner's line delivery event
        //dialogueRunner.onNodeStart.AddListener(OnNodeStart);


        /*dialogueRunner.AddCommandHandler("playSound", (string[] parameters) =>
        {
            PlaySound(parameters);
        });*/

        dialogueRunner.AddCommandHandler<string>("playSound", PlaySound);
        dialogueRunner.AddCommandHandler<string>("stopSound", StopSound);
        //dialogueRunner.AddCommandHandler("playVoiceOver", new System.Action<string[]>(PlayVoiceOver));

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnNodeStart(string nodeName)
    {
        Debug.Log($"Node started: {nodeName}");

        // Attempt to load a corresponding audio file
        PlayAudioForNode(nodeName);
    }

    private void PlayAudioForNode(string audioFileName)
    {
        // Load the audio file
        AudioClip clip = Resources.Load<AudioClip>($"Voice/{audioFileName}");
        if (clip != null)
        {
            Debug.Log($"Playing audio for node: {audioFileName}");
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"Audio file not found for node: {audioFileName}");
        }
    }

    public void PlaySound(string soundName)
    {
        /* Debug.Log($"Received parameters: {string.Join(", ", parameters ?? new string[0])}");

         if (parameters.Length > 0)
         {
             string soundName = parameters[0];*/
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
        /*}
        else
        {
            Debug.LogError("No parameters provided for playSound command.");
        }*/
    }

    public void StopSound(string soundName)
    {
        /*if (parameters.Length > 0)
        {*/
        //string soundName = parameters[0];
        if (audioSource.clip != null && audioSource.clip.name == soundName)
        {
            audioSource.Stop();
        }
        // }
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
