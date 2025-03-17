using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventory 
{
    public ItemData _itemData;
    public int _stackSize;

    public ItemInventory(ItemData itemData)
    { 
        _itemData = itemData;
        AddStack();
    }

    public void AddStack() => _stackSize++;
    public void RemoveStack() => _stackSize--;
}
