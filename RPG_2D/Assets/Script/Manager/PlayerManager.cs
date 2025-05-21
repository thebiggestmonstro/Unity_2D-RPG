using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _playerManagerInstance;
    public PlayerController _playerController;

    public int _currencyForSkillUnlock;

    private void Awake()
    {
        if (_playerManagerInstance != null)
            Destroy(_playerManagerInstance.gameObject);
        
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
}
