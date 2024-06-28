using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour, IDataPersistence
{
    public static GameManagerScript instance;

    //UI
    [SerializeField]
    private TextMeshProUGUI taskUI;
    [SerializeField]
    private TextMeshProUGUI carriedPackagevalue;
    [SerializeField]
    private GameObject completionUI;
    [SerializeField]
    private GameObject failureUI;
    [SerializeField]
    private GameObject pauseUI;

    private bool isPause;

    [SerializeField]
    private float taskTime;

    private float remainingTaskTime;

    [SerializeField]
    private int mainTask;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        TimerScript.instance.StartTimer(remainingTaskTime);
    }

    private void Update()
    {
        PauseAction();
    }

    public void UpdateCarriedPackageValue(int packageValue)
    {
        carriedPackagevalue.text = $"{packageValue}";
    }

    public void UpdateTaskProgress(int progress)
    {
        taskUI.text = $"{progress} / {mainTask}";
    }

    public void CheckTask(int currentTaskValue)
    {
        bool isTimeOut = TimerScript.instance.IsTimeOut();

        if (isTimeOut && currentTaskValue != mainTask)
        {
            Time.timeScale = 0;
            failureUI.SetActive(true);
        }

        if (currentTaskValue == mainTask)
        {
            TimerScript.instance.StopTimer();
            Time.timeScale = 0;
            completionUI.SetActive(true);
        }
    }

    #region SaveLoadFunction
    public void LoadData(GameData data)
    {
        if (data.remainingTaskTime < 0)
            remainingTaskTime = taskTime;
        else
            this.remainingTaskTime = data.remainingTaskTime;
    }

    public void SaveData(ref GameData data)
    {
        data.remainingTaskTime = TimerScript.instance.elapsedTimeRemaining;
    }
    #endregion

    public void PauseAction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
        }
    }
}
