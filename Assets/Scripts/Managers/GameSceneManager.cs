using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IDataPersistence
{
    private int levelIndex = 0;

    public static GameSceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene("Intro");
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); 
    }

    public void FinishGame()
    {
        Time.timeScale = 1.0f;
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("Intro");
    }

    //Save and load current scene in playing
    public void LoadData(GameData data)
    {
        this.levelIndex = data.sceneLevelIndex;
    }

    public void SaveData(ref GameData data)
    {
        data.sceneLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.GetActiveScene().buildIndex == 0 && !data.isContinue)
        {
            data.isContinue = false;
        }
        else
        {
            data.isContinue = true;
        }
    }
}
