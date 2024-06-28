using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 10f;

    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private bool canSpawn = true;

    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    public void SpawnItem()
    {
        if (canSpawn)
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

            Instantiate(itemPrefab,
                        new Vector3(Random.Range(startPoint.transform.position.x, endPoint.transform.position.x),
                                    Random.Range(startPoint.transform.position.y, endPoint.transform.position.y),0),
                        Quaternion.identity);
        }
    }

    public void ChangeSpawnerStatus()
    {
        canSpawn = !canSpawn;
    }
}
