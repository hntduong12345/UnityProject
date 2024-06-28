using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        float height = transform.position.z;
        Vector2 newPosition = player.position;
        transform.position = new Vector3(newPosition.x, newPosition.y ,height);
        transform.rotation = Quaternion.identity;
    }
}
