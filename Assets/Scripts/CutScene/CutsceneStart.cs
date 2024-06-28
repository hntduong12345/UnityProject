using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SignalReceiver))]
public class CutsceneStart : Interactable
{
    [SerializeField]
    private GameObject cutsceneToPlay;
    [SerializeField]
    private bool isOneTimeOnly;

    public override void Activate()
    {
        base.Activate();
        cutsceneToPlay.SetActive(true);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        cutsceneToPlay.SetActive(false);

        if (isOneTimeOnly)
        {
            Destroy(cutsceneToPlay);
            Destroy(this.gameObject);
        }
    }
}
