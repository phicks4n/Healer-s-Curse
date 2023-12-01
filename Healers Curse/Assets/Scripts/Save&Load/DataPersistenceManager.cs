using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Inventory;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null) 
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // Update the sceneIndex in the loaded gameData
        if (gameData != null && scene.buildIndex != 7)
        {
            gameData.sceneIndex = scene.buildIndex;
            Debug.Log("Current scene index: " + gameData.sceneIndex + " Initial " + gameData.initialSceneIndex);

            SaveSceneIndex();
        }

        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame() 
    {
        this.gameData = new GameData();
        InventoryController.instance.StartNewInventory();
        InventoryController.instance.StartNewEquipment(gameData, 0);
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull) 
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this.gameData == null) 
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (this.gameData == null) 
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    // Save only the sceneIndex without modifying other data
    public void SaveSceneIndex()
    {
        if (this.gameData != null)
        {
            dataHandler.SaveSceneIndex(this.gameData.sceneIndex);
        }
    }

    // Save only the enemyType without modifying other data
    public void SaveEnemyType()
    {
        if (this.gameData != null)
        {
            dataHandler.SaveEnemyType(this.gameData.enemyType);
        }
    }

    // Save only the PlayerPosition without modifying other data
    public void SavePlayerPosition()
    {
        if (this.gameData != null)
        {
            dataHandler.SavePlayerPosition(this.gameData.playerPosition);
        }
    }

    // Save only the specific Player's stat without modifying other data
    public void SavePlayerStat(int playerStat, int stat)
    {
        if (this.gameData != null)
        {

            dataHandler.SavePlayerStat(playerStat, stat);  
        }
    }
    
    private void OnApplicationQuit() 
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    public bool HasGameData() 
    {
        return gameData != null;
    }
}