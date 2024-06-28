using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }

    private List<IDataPersistence> dataPersistenceObjects;
    private GameData gameData;

    public bool isFinish;

    [Header("File Storage Config")]
    [SerializeField]
    private string fileName;

    private string dataDirPath = $"D:\\FPT\\FPT_CN7\\PRU212\\Code\\GroupProject\\Data";

    [SerializeField]
    private bool useEncryption;

    private FileDataHandler fileDataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than 1 Data Persistence Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        //Data file save in local Memory
        //this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        //Custom data save file location
        this.fileDataHandler = new FileDataHandler(dataDirPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        fileDataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        //Load saved data
        this.gameData = fileDataHandler.Load();

        //If no save data found => Start new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to default");
            NewGame();
        }

        //Push loaded data to other scripts
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //Pass data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        //Save data to file
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        if (isFinish)
        {
            NewGame();
        }
        else
        {
            SaveGame();
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
