using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager _gameSaveManagerinstance;

    [SerializeField] 
    private string _fileName;
    [SerializeField] 
    private bool _encryptData;

    private GameData _gameData;
    private List<ISaveManager> _saveManagers;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        if (_gameSaveManagerinstance != null)
            Destroy(_gameSaveManagerinstance.gameObject);
            
        _gameSaveManagerinstance = this;

        _saveManagers = new List<ISaveManager>();
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _encryptData);
    }

    private void Start()
    {
        _saveManagers = FindAllSaveManagers();
        LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            Debug.Log("No saved data found!");
            NewGame();
        }

        foreach (ISaveManager saveManager in _saveManagers)
        {
            saveManager.LoadData(_gameData);
        }
    }

    public void OnLevelWasLoaded()
    {
        _saveManagers.Clear();
        _saveManagers = FindAllSaveManagers();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in _saveManagers)
        {
            saveManager.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete Save File")]
    public void DeleteSavedData()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _encryptData);
        _dataHandler.Delete();
    }

    public bool FindSavedData()
    {
        if (_dataHandler.Load() != null)
        {
            return true;
        }

        return false;
    }
}
