using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ObjectActivationBehaviour : PlayableBehaviour
{
    public GameObject targetObject;
    public bool activate;

    public override void OnGraphStart(Playable playable)
    {
        // When the Timeline starts, the object is initially hidden
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // Activate or deactivate the object based on the 'activate' value
        if (targetObject != null)
        {
            targetObject.SetActive(activate);
            // Debug.Log("Activated: " + targetObject.name);
        }
    }
}
