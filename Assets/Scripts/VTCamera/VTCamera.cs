using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTCamera : MonoBehaviour
{
   public CinemachineVirtualCamera VirtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VirtualCamera.m_Follow = null;
    }
}
