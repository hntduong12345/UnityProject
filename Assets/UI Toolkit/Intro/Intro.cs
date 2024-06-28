using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Intro : MonoBehaviour, IDataPersistence
{
    private int levelIndex = 0;
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //Button Function
        root.Q<Button>("btnResume").clicked += () =>
        {
            SceneManager.LoadScene(levelIndex);
        };

        root.Q<Button>("btnStartNew").clicked += () =>
        {
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        };

        root.Q<Button>("btnOptions").clicked += () =>
        {
            Debug.Log("In Working");
        };

        root.Q<Button>("btnQuit").clicked += () =>
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        };
    }

    //Save and load current scene in playing
    public void LoadData(GameData data)
    {
        this.levelIndex = data.sceneLevelIndex;

        if (!data.isContinue)
        {
            root.Q<Button>("btnResume").visible = false;
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
