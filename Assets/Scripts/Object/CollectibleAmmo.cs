using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            Ruby triggerObject = collision.GetComponent<Ruby>();
            if (triggerObject != null)
            {
                triggerObject.IncreaseAmmo();

                Destroy(this.gameObject);
            }
        }
        catch(Exception e)
        {
            Debug.LogWarning(e);
        }
        
    }
}
