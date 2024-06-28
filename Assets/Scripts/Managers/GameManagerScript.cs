using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject? pausePanel;
    private bool isPause = false;

    public AudioClip bossSong;

    public static GameManagerScript instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!isPause);
            isPause = !isPause;
            if(isPause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void ChangeBGSong()
    {
        this.gameObject.GetComponentInChildren<AudioSource>().clip = bossSong;
        this.gameObject.GetComponentInChildren<AudioSource>().Play();
    }
}
