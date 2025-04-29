using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ItemType
{ 
    Material,
    Equipment,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public string _itemName;
    public Sprite _itemIcon;

    [Range(0, 100)]
    public float _dropChance;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}
