using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> packagesCarried;
    public SerializableDictionary<string, bool> packagesDeliveried;

    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public float remainingTaskTime;

    public GameData()
    {
        packagesCarried = new SerializableDictionary<string, bool>();
        packagesDeliveried = new SerializableDictionary<string, bool>();
        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
        remainingTaskTime = -1;
    }
}
