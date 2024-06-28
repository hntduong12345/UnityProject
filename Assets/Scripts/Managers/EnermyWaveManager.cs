using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyWaveManager : MonoBehaviour
{
    [SerializeField] private int completeTarget;
    private int count = 0;

    public GameObject bulletSpawner;
    public GameObject healthSpawner;

    public GameObject completePanel;

    public static EnermyWaveManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (count >= completeTarget)
        {
            bulletSpawner.GetComponent<ItemSpawner>().ChangeSpawnerStatus();
            healthSpawner.GetComponent<ItemSpawner>().ChangeSpawnerStatus();
            completePanel.SetActive(true);
        }
    }

    public void IncreaseCounter()
    {
        count++;
    }
}
