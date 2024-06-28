using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    public float elapsedTimeRemaining {  get { return elapsedTime; } }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "00:00.00";
        //timerGoing = false;
    }

    public void StartTimer(float taskTime)
    {
        timerGoing = true;
        elapsedTime = taskTime;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    public bool IsTimeOut()
    {
        return elapsedTime <= 0;
    }

    private IEnumerator UpdateTimer()
    {
        if (elapsedTime <= 0) timerGoing = false;
        while (timerGoing)
        {
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            Debug.Log(timePlaying.ToString(@"mm\:ss\.f"));
            timerText.text = timePlaying.ToString(@"mm\:ss\.f");

            yield return null;
        }
    }
}
