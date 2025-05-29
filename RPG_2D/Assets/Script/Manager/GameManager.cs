using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager _gameManagerinstance;

    [SerializeField] private Interact_CheckPoint[] _checkpoints;
    [SerializeField] private string _closestCheckpointId;

    private void Awake()
    {
        if (_gameManagerinstance != null)
            Destroy(_gameManagerinstance.gameObject);

        _gameManagerinstance = this;

        _checkpoints = FindObjectsOfType<Interact_CheckPoint>();
    }

    public void RestartScene()
    {
        GameSaveManager._gameSaveManagerinstance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        GameSaveManager._gameSaveManagerinstance.OnLevelWasLoaded();
    }

    public void SaveData(ref GameData data)
    {
        if (FindClosestCheckpoint() != null)
            data._closestCheckpointId = FindClosestCheckpoint()._checkPointId;

        data._checkpoints.Clear();

        foreach (Interact_CheckPoint checkpoint in _checkpoints)
        {
            data._checkpoints.Add(checkpoint._checkPointId, checkpoint._isActivating);
        }
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data._checkpoints)
        {
            foreach (Interact_CheckPoint checkpoint in _checkpoints)
            {
                if (checkpoint._checkPointId == pair.Key && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }

        LoadClosestCheckpoint(data);
    }

    private void LoadClosestCheckpoint(GameData data)
    {
        if (data._closestCheckpointId == null)
            return;

        _closestCheckpointId = data._closestCheckpointId;

        foreach (Interact_CheckPoint checkpoint in _checkpoints)
        {
            if (_closestCheckpointId == checkpoint._checkPointId)
                PlayerManager._playerManagerInstance._playerController.transform.position = checkpoint.transform.position;
        }
    }

    private Interact_CheckPoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Interact_CheckPoint closestCheckpoint = null;

        foreach (Interact_CheckPoint checkpoint in _checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(PlayerManager._playerManagerInstance._playerController.transform.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint._isActivating == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }
}
