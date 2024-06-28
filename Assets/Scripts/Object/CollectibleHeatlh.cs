using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleHeatlh : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            Ruby triggerObject = collision.GetComponent<Ruby>();
            if (triggerObject != null)
            {
                triggerObject.ChangeHealth(1);

                Destroy(this.gameObject);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }

    }
}
