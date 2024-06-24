using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // start up the auto saving coroutine
        //if (autoSaveCoroutine != null)
        //{
        //    StopCoroutine(autoSaveCoroutine);
        //}
        //autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileID(string newProfileId)
    {
        //update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;
        //load the game, which will use that prfile, updating our game data accordingly
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        //delete the data for this profile id
        dataHandler.Delete(profileId);
       
        //reload the game so that our data matches the newly selected profile id
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        Debug.Log("Loaded");

        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);


        //if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        //push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            Debug.Log("Disable Data Persistence is on");
            return;
        }

        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        Debug.Log("Save");

        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            gameData.currentScene = SceneManager.GetActiveScene().name;
            gameData.movingFromMenu = false;

        }
        else
        {
            gameData.movingFromMenu = true;
        }

        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
            Debug.Log("Passing the data " + dataPersistenceObj);
        }

        //timestamp the data so we know when it was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        //save the data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    //Used when you want to check if the game has any game data yet 
    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string,GameData> GetAllProfileGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    public string GetProfileName() 
    {
        return this.selectedProfileId;
    }

}
