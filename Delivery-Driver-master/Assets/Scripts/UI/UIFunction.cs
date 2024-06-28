using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunction : MonoBehaviour
{
    public void Restart()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {

    }

    public void BackToMenu()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
