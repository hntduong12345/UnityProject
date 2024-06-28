using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int bulletAmmo;
    public int playerHealth;
    public int sceneLevelIndex;
    public bool isContinue;

    public GameData()
    {
        playerPosition = Vector3.zero;
        bulletAmmo = 5;
        playerHealth = 5;
        sceneLevelIndex = 0;
        isContinue = false;
    }
}
