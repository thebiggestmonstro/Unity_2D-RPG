using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    _strength,
    _agility,
    _intelligence,
    _vitality,
    _maxHealth,
    _armor,
    _evasion,
    _magicResistance,
    _attackPoint,
    _critChance,
    _critPower,
    _fireDamage,
    _iceDamage,
    _lightningDamage,
}

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class ItemEffect_Buff : ItemEffect
{
    private PlayerStats _playerStats;

    [SerializeField]
    private StatType _statToBuff;
    [SerializeField]
    private int _buffAmount;
    [SerializeField]
    private float _buffDuration;

    public override void ExecuteEffect(Transform enemyPosition)
    {
        _playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();
        _playerStats.IncreaseStats(_buffAmount, _buffDuration, GetStatToModify());
    }

    private CharacterStats GetStatToModify()
    {
        if (_statToBuff == StatType._strength)
            return _playerStats._strength;
        else if (_statToBuff == StatType._agility)
            return _playerStats._agility;
         else if (_statToBuff == StatType._intelligence)
            return _playerStats._intelligence;
         else if (_statToBuff == StatType._vitality)
            return _playerStats._vitality;
         else if (_statToBuff == StatType._maxHealth)
            return _playerStats._maxHealth;
         else if (_statToBuff == StatType._armor)
            return _playerStats._armor;
         else if (_statToBuff == StatType._evasion)
            return _playerStats._evasion;
         else if (_statToBuff == StatType._magicResistance)
            return _playerStats._magicResistance;
         else if (_statToBuff == StatType._attackPoint)
            return _playerStats._attackPoint;
         else if (_statToBuff == StatType._critChance)
            return _playerStats._critChance;         
        else if (_statToBuff == StatType._critPower)
            return _playerStats._critPower;         
        else if (_statToBuff == StatType._fireDamage)
            return _playerStats._fireDamage;         
        else if (_statToBuff == StatType._iceDamage)
            return _playerStats._iceDamage;
        else if (_statToBuff == StatType._lightningDamage)
            return _playerStats._lightningDamage;
        
       return null;
    }
}
