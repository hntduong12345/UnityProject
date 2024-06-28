using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour, IDataPersistence
{
    public GameObject continueBtn;
    private int levelIndex = 0;

    public static MenuSceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //Save and load current scene in playing
    public void LoadData(GameData data)
    {
        this.levelIndex = data.sceneLevelIndex;

        if (data.isContinue)
        {
            continueBtn.SetActive(true);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.sceneLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex == 0 && !data.isContinue)
        {
            data.isContinue = false;
        }
        else
        {
            data.isContinue = true;
        }
    }
}
