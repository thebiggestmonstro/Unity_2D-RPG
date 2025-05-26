using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int _currentCurrency;
    public SerializableDictionary<string, bool> _skillTreeData;
    public SerializableDictionary<string, int> _inventoryData;
    public List<string> _equipmentId;

    public GameData()
    {
        this._currentCurrency = 0;
        _skillTreeData = new SerializableDictionary<string, bool>();
        _inventoryData = new SerializableDictionary<string, int>();
        _equipmentId = new List<string>();
    }
}
