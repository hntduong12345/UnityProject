using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject bulletSpawner;
    public GameObject healthSpawner;
    public GameObject player;
    public GameObject barrier;
    public GameObject battleStartPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        barrier.SetActive(true);
        player.transform.position = battleStartPoint.transform.position;
        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<Spawner>().SpawnEnermy();
        }
        bulletSpawner.GetComponent<ItemSpawner>().SpawnItem();
        healthSpawner.GetComponent<ItemSpawner>().SpawnItem();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManagerScript.instance.ChangeBGSong();
    }
}
