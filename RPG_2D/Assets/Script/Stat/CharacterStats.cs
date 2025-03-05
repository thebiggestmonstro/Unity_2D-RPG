using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [SerializeField]
    private int _baseValue;

    public List<int> _statModifierList;

    public int GetValue()
    {
        int finaleValue = _baseValue;

        foreach (int modifier in _statModifierList)
        {
            finaleValue += modifier;
        }

        return finaleValue;
    }

    public void AddModifier(int modifier)
    { 
        _statModifierList.Add(modifier);
    }

    public void RemoveModifier(int modifier) 
    { 
        _statModifierList.Remove(modifier);
    }

    public void SetDefaultValue(int value)
    { 
        _baseValue = value;
    }
}
