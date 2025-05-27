using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item_Inventory
{
    public ItemData _itemData;
    public int _stackSize;
    public Item_Inventory(ItemData newItemData)
    {
        _itemData = newItemData;
        AddStack();
    }

    public void AddStack() => _stackSize++;
    public void RemoveStack() => _stackSize--;
}
