using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _inventoryManagerInstance;
    public List<ItemInventory> _itemInventories;
    public Dictionary<ItemData, ItemInventory> _itemInventoryDictionary;

    private void Awake()
    {
        if (_inventoryManagerInstance == null)
            _inventoryManagerInstance = this;
        else
            Destroy(_inventoryManagerInstance);
    }

    private void Start()
    {
        _itemInventories = new List<ItemInventory>();
        _itemInventoryDictionary = new Dictionary<ItemData, ItemInventory>();
    }

    public void AddItem(ItemData itemData)
    {
        if (_itemInventoryDictionary.TryGetValue(itemData, out ItemInventory value))
        {
            value.AddStack();
        }
        else
        { 
            ItemInventory newItem = new ItemInventory(itemData);
            _itemInventories.Add(newItem);
            _itemInventoryDictionary.Add(itemData, newItem);
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if (_itemInventoryDictionary.TryGetValue(itemData, out ItemInventory value))
        {
            if (value._stackSize < -1)
            {
                _itemInventories.Remove(value);
                _itemInventoryDictionary.Remove(itemData);
            }
            else
            {
                value.RemoveStack();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ItemData newItem = _itemInventories[_itemInventories.Count - 1]._itemData;
            RemoveItem(newItem);
        }
    }
}
