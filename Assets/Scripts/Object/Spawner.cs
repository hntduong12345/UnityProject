using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private GameObject enermyPrefab;

    [SerializeField]
    private bool canSpawn = true;

    [SerializeField]
    private int maxUnit;

    private int count = 0;

    private void Update()
    {
        if(count >= maxUnit)
        {
            StopAllCoroutines();
        }
    }

    public void SpawnEnermy()
    {
        if (canSpawn && count < maxUnit)
        {
            StartCoroutine(Spawning());
        }
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (true)
        {
            yield return wait;

            count++;
            Instantiate(enermyPrefab, transform.position, Quaternion.identity);
        }
    }

    public void ChangeSpawnerStatus()
    {
        canSpawn = !canSpawn;
    }
}
