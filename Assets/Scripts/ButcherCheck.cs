using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ButcherCheck : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    //[SerializeField] private SignalAsset activateSignal;

    //private SignalReceiver signalReceiver;

    private bool hasTimelineStarted = false;

    private MemoryTracker check_data;

    void Start()
    {
        check_data = FindObjectOfType<MemoryTracker>();

        if (director != null)
        {
            director.stopped += OnTimelineStopped; // Subscribe to stopped event
        }
    }

    private void Update()
    {
        // Trigger the timeline only when the condition is met
        if (!hasTimelineStarted && check_data != null && check_data.getCompletedMemories() == check_data.getTotalMemories())
        {
            hasTimelineStarted = true;
            TriggerTimeline();
        }
    }

    private void TriggerTimeline()
    {
        if (director != null /*&& activateSignal != null*/)
        {
            // Activate the director and play the timeline
            director.gameObject.SetActive(true);

            // Fire the signal to trigger actions in the timeline (e.g., start cutscene)
            //signalReceiver.ReceiveSignal(activateSignal);

            // Play the timeline after the signal is sent
            director.Play();
        }
    }

    private void OnTimelineStopped(PlayableDirector stoppedDirector)
    {
        Debug.Log("Timeline has finished.");
        director.gameObject.SetActive(false); // Optional: Deactivate the director
    }
}
