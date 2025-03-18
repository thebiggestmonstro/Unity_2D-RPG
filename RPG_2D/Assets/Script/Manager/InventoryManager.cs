using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _inventoryManagerInstance;

    public List<ItemInventory> _itemInventories;
    public Dictionary<ItemData, ItemInventory> _itemInventoryDictionary;

    public List<ItemInventory> _stashItemInventories;
    public Dictionary<ItemData, ItemInventory> _stashItemInventoryDictionary;

    [Header("Inventory UI")]
    [SerializeField]
    private Transform _inventorySlotParent;
    [SerializeField]
    private Transform _stachSlotParent;
    private UI_InventorySlot[] _inventorySlotList;
    private UI_InventorySlot[] _stashInventorySlotList;

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

        _stashItemInventories = new List<ItemInventory>();
        _stashItemInventoryDictionary = new Dictionary<ItemData, ItemInventory>();

        _inventorySlotList = _inventorySlotParent.GetComponentsInChildren<UI_InventorySlot>();
        _stashInventorySlotList = _stachSlotParent.GetComponentsInChildren<UI_InventorySlot>();
    }

    private void UpdateUIInventorySlot()
    {
        for (int i = 0; i < _itemInventories.Count(); i++)
        {
            _inventorySlotList[i].UpdateSlot(_itemInventories[i]);
        }

        for (int i = 0; i < _stashItemInventories.Count(); i++)
        {
            _stashInventorySlotList[i].UpdateSlot(_stashItemInventories[i]);
        }
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.ItemType == ItemType.Equipment)
        {
            AddToInventory(itemData);
        }
        else if (itemData.ItemType == ItemType.Material)
        {
            AddToStashInventory(itemData);
        }

        UpdateUIInventorySlot();
    }

    private void AddToInventory(ItemData itemData)
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

    private void AddToStashInventory(ItemData itemData)
    {
        if (_stashItemInventoryDictionary.TryGetValue(itemData, out ItemInventory value))
        {
            value.AddStack();
        }
        else
        {
            ItemInventory newItem = new ItemInventory(itemData);
            _stashItemInventories.Add(newItem);
            _stashItemInventoryDictionary.Add(itemData, newItem);
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

        if (_itemInventoryDictionary.TryGetValue(itemData, out ItemInventory stashValue))
        {
            if (stashValue._stackSize < -1)
            {
                _stashItemInventories.Remove(stashValue);
                _stashItemInventoryDictionary.Remove(itemData);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateUIInventorySlot();
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
