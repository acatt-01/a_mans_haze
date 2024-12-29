using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MaterialChangerPlayableAsset : PlayableAsset
{
    public Material targetMaterial;
    public Texture newBaseMap;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MaterialChangerPlayable>.Create(graph);
        var behaviour = playable.GetBehaviour();

        behaviour.targetMaterial = targetMaterial;
        behaviour.newBaseMap = newBaseMap;

        return playable;
    }
}

