using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Highlight : MonoBehaviour
{
    //we assign all the renderers here through the inspector
    [SerializeField]
    public List<Renderer> renderers;
    [SerializeField]
    private Color color = Color.cyan;

    //helper list to cache all the materials ofd this object
    private List<Material> materials;

    public TMP_Text interactionMessage = null;

    //Gets all the materials from each renderer
    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            //A single child-object might have mutliple materials on it
            //that is why we need to all materials with "s"
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                color.a = 0.5f;
                material.SetColor("_EmissionColor", color * 5f);

                // Show the interaction message
                interactionMessage.gameObject.SetActive(true);
                interactionMessage.text = "Press E to view the memory";



            }
        }
        else
        {
            foreach (var material in materials)
            {
                //we can just disable the EMISSION
                //if we don't use emission color anywhere else
                material.DisableKeyword("_EMISSION");
                color.a = 1f;
                interactionMessage.gameObject.SetActive(false);
            }
        }
    }
}
