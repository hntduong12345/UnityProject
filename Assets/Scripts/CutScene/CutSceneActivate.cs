using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SignalReceiver))]
public class CutSceneActivate : MonoBehaviour
{
    [SerializeField]
    private GameObject cutsceneToPlay;
    [SerializeField]
    private bool isOneTimeOnly;

    public void Activate()
    {
        cutsceneToPlay.SetActive(true);
    }

    public void Deactivate()
    {
        cutsceneToPlay.SetActive(false);

        if (isOneTimeOnly)
        {
            Destroy(cutsceneToPlay);
            Destroy(this.gameObject);
        }
    }
}
