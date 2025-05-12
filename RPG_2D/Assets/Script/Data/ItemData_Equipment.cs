using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public enum EquipmentType
{ 
    Weapon,
    Armor,
    Amulet,
    Flask,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType EquipmentType;

    [Header("Unique Effect")]
    public ItemEffect[] _itemEffects;
    public float _itemCooldown;
    [TextArea]
    public string _itemEffectDescription;

    [Header("Major stats")]
    public int _strength;
    public int _agility;
    public int _intelligence;
    public int _vitality;

    [Header("Offensive stats")]
    public int _attackPoint;
    public int _critChance;
    public int _critPower;

    [Header("Defensive stats")]
    public int _health;
    public int _armor;
    public int _evasion;
    public int _magicResistance;

    [Header("Magic stats")]
    public int _fireDamage;
    public int _iceDamage;
    public int _lightningDamage;

    [Header("Craft Requiermets")]
    public List<Item_Inventory> _craftMaterials;

    private int _descriptionLength;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();

        playerStats._strength.AddModifier(_strength);
        playerStats._agility.AddModifier(_agility);
        playerStats._intelligence.AddModifier(_intelligence);
        playerStats._vitality.AddModifier(_vitality);

        playerStats._attackPoint.AddModifier(_attackPoint);
        playerStats._critChance.AddModifier(_critChance);
        playerStats._critChance.AddModifier(_critChance);

        playerStats._maxHealth.AddModifier(_health);
        playerStats._armor.AddModifier(_armor);
        playerStats._evasion.AddModifier(_evasion);
        playerStats._magicResistance.AddModifier(_magicResistance);

        playerStats._fireDamage.AddModifier(_fireDamage);
        playerStats._iceDamage.AddModifier(_iceDamage);
        playerStats._lightningDamage.AddModifier(_lightningDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();

        playerStats._strength.RemoveModifier(_strength);
        playerStats._agility.RemoveModifier(_agility);
        playerStats._intelligence.RemoveModifier(_intelligence);
        playerStats._vitality.RemoveModifier(_vitality);

        playerStats._attackPoint.RemoveModifier(_attackPoint);
        playerStats._critChance.RemoveModifier(_critChance);
        playerStats._critPower.RemoveModifier(_critPower);


        playerStats._maxHealth.RemoveModifier(_health);
        playerStats._armor.RemoveModifier(_armor);
        playerStats._evasion.RemoveModifier(_evasion);
        playerStats._magicResistance.RemoveModifier(_magicResistance);


        playerStats._fireDamage.RemoveModifier(_fireDamage);
        playerStats._iceDamage.RemoveModifier(_iceDamage);
        playerStats._lightningDamage.RemoveModifier(_lightningDamage);
    }

    public void ExecuteItemEffect(Transform enemyPosition)
    {
        foreach (ItemEffect itemEffect in _itemEffects)
        {
            itemEffect.ExecuteEffect(enemyPosition);
        }
    }

    public override string GetDescription()
    {
        _stringBuilder.Length = 0;
        _descriptionLength = 0;

        SetItemDescription(_strength, "Strength");
        SetItemDescription(_agility, "Agility");
        SetItemDescription(_intelligence, "Intelligence");
        SetItemDescription(_vitality, "Vitality");

        SetItemDescription(_attackPoint, "AttackPoint");
        SetItemDescription(_critChance, "CritChance");
        SetItemDescription(_critPower, "CritPower");

        SetItemDescription(_health, "Health");
        SetItemDescription(_armor, "Armor");
        SetItemDescription(_evasion, "Evasion");
        SetItemDescription(_magicResistance, "MagicResistance");

        SetItemDescription(_fireDamage, "FireDamage");
        SetItemDescription(_iceDamage, "IceDamage");
        SetItemDescription(_lightningDamage, "LightningDamage");

        if (_itemEffectDescription.Length > 0)
        {
            _stringBuilder.AppendLine();
            _stringBuilder.Append(_itemEffectDescription);
        }

        if (_descriptionLength < 5)
        {
            for (int i = 0; i < 5 - _descriptionLength; i++)
            {
                _stringBuilder.AppendLine();
                _stringBuilder.Append("");
            }
        }

        return _stringBuilder.ToString();
    }

    private void SetItemDescription(int value, string name)
    {
        if (value != 0)
        {
            if (_stringBuilder.Length > 0)
                _stringBuilder.AppendLine();

            if (value > 0)
                _stringBuilder.Append(name + ": " + value);

            _descriptionLength++;
        }
    }
}
