using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MaterialChangerPlayable : PlayableBehaviour
{
    public Material targetMaterial;
    public Texture newBaseMap;

    private Texture originalBaseMap;
    private float originalMetallicValue;

    // Store original texture and metallic values only once
    private bool isMaterialInitialized = false;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (targetMaterial == null || newBaseMap == null)
            return;

        if (!isMaterialInitialized)
        {
            // Save the original texture
            originalBaseMap = targetMaterial.GetTexture("_BaseMap");
            originalMetallicValue = targetMaterial.GetFloat("_Metallic");
            isMaterialInitialized = true;
        }

        // Set the new texture
        Debug.Log("Mudei a textura!!");
        targetMaterial.SetTexture("_BaseMap", newBaseMap);
        targetMaterial.SetFloat("_Metallic", 1f);

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

        if (targetMaterial == null || originalBaseMap == null)
        {
            if (targetMaterial.name.Contains("Plasterl") || targetMaterial.name.Contains("Doors"))
            {
                targetMaterial.SetTexture("_BaseMap", null);
                targetMaterial.SetFloat("_Metallic", originalMetallicValue);
            }
            return;
        }

        // Restore the original texture
        Debug.Log("Mudeia de volta");
        targetMaterial.SetTexture("_BaseMap", originalBaseMap);
        targetMaterial.SetFloat("_Metallic", originalMetallicValue);
    }
    public override void OnGraphStop(Playable playable)
    {
        // Ensure material resets even if the Timeline stops
        OnBehaviourPause(playable, new FrameData());
    }
}
