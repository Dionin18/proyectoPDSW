using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Mas de una instancia de los datos del juego en escena. Borrando la mas nueva.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // Asegurarse de tener un ID de perfil seleccionado antes de cargar
        if (string.IsNullOrEmpty(selectedProfileId))
        {
            Debug.LogWarning("No hay un ID de perfil seleccionado. Creando uno nuevo por defecto.");
            selectedProfileId = "defaultProfile";
            NewGame();
        }
        else
        {
            LoadGame();
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        if (string.IsNullOrEmpty(newProfileId))
        {
            Debug.LogError("El nuevo ID del perfil no puede estar vacío o ser nulo.");
            return;
        }
        this.selectedProfileId = newProfileId;
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        Debug.Log("Nueva partida creada.");
        SaveGame(); // Guardar datos inmediatamente después de crear un nuevo juego
    }

    public void LoadGame()
    {
        if (string.IsNullOrEmpty(selectedProfileId))
        {
            Debug.LogError("El ID del perfil seleccionado está vacío o es nulo. No se pueden cargar los datos.");
            return;
        }

        try
        {
            this.gameData = dataHandler.Load(selectedProfileId);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al cargar los datos: " + e.Message);
            return;
        }

        if (this.gameData == null)
        {
            Debug.Log("No se han encontrado datos guardados, creando una nueva partida...");
            this.gameData = new GameData();
            SaveGame(); // Guardar datos predeterminados
        }
        else
        {
            Debug.Log("Datos cargados correctamente.");
        }

        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            Debug.LogWarning("No se encontraron objetos de persistencia de datos.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj == null)
            {
                Debug.LogWarning("Se encontró un objeto nulo en dataPersistenceObjects.");
                continue;
            }
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (gameData == null)
        {
            Debug.LogWarning("No hay datos de juego para guardar.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }
        }

        try
        {
            dataHandler.Save(gameData, selectedProfileId);
            Debug.Log("Datos guardados correctamente.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar los datos: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        try
        {
            return dataHandler.LoadAllProfiles();
        }
        catch (Exception e)
        {
            Debug.LogError("Error al cargar todos los perfiles de datos: " + e.Message);
            return new Dictionary<string, GameData>();
        }
    }
}
