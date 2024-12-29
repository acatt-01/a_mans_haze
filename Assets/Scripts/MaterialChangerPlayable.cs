using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MaterialChangerPlayable : PlayableBehaviour
{
    public Material targetMaterial;
    public Texture newBaseMap;

    private Texture originalBaseMap;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (targetMaterial == null || newBaseMap == null)
            return;

        // Save the original texture
        originalBaseMap = targetMaterial.GetTexture("_BaseMap");

        // Set the new texture
        Debug.Log("Mudei a textura!!");
        targetMaterial.SetTexture("_BaseMap", newBaseMap);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (targetMaterial == null || originalBaseMap == null)
            return;

        // Restore the original texture
        Debug.Log("Mudeia de volta");
        targetMaterial.SetTexture("_BaseMap", originalBaseMap);
    }
}
