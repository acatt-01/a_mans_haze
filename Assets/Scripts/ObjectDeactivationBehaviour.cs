using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ObjectDeactivationBehaviour : PlayableBehaviour
{
    public GameObject targetObject;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);  // Deactivate the object when the timeline plays
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        /*if (targetObject != null)
        {
            targetObject.SetActive(true);
        }*/
    }
}
