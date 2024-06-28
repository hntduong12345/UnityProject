using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isInteract;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ruby"))
        {
            Ruby.Instance.ChangeActionableState();
            isInteract = true;   
        }
    }

    private void Update()
    {
        if (isInteract)
        {
            Activate();
        }
    }

    public virtual void Activate()
    {

    }

    public virtual void Deactivate()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ruby"))
        {
            Ruby.Instance.ChangeActionableState();
            isInteract = false;
        }
    }
}
