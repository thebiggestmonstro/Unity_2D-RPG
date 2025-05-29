using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager _playerManagerInstance;
    public PlayerController _playerController;

    public int _currencyForSkillUnlock;

    private void Awake()
    {
        if (_playerManagerInstance != null)
            Destroy(_playerManagerInstance.gameObject);
        else
            _playerManagerInstance = this;
    }

    public bool CanUnlockSkill(int cost)
    {
        if (cost > _currencyForSkillUnlock)
            return false;

        _currencyForSkillUnlock -= cost;
        return true;
    }

    public int GetCurrency() => _currencyForSkillUnlock;

    public void LoadData(GameData gameData)
    {
        this._currencyForSkillUnlock = gameData._currentCurrency;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData._currentCurrency = this._currencyForSkillUnlock;
    }
}
