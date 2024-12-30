using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ObjectDeactivationAsset : PlayableAsset
{
    public ExposedReference<GameObject> targetObject;
    public bool desactivate = false;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ObjectDeactivationBehaviour>.Create(graph);
        ObjectDeactivationBehaviour behaviour = playable.GetBehaviour();
        behaviour.targetObject = targetObject.Resolve(graph.GetResolver());
        behaviour.activate = desactivate;  // Set the target object for deactivation
        return playable;
    }
}
