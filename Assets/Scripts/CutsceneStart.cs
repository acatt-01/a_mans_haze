using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(SignalReceiver))]
public class CutsceneStart : Interactable
{
    [SerializeField] private GameObject cutsceneToPlay;
    GameObject player;
    public GameObject leoObject;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        cutsceneToPlay.SetActive(false);
    }

    public override void Activate()
    {
        leoObject.SetActive(true);
        base.Activate();
        cutsceneToPlay.SetActive(true);
        player.SetActive(false);
    }

    public override void Desactivate()
    {
        base.Desactivate();
        cutsceneToPlay.SetActive(false);
        leoObject.SetActive(false);
        player.SetActive(true);
    }
}
