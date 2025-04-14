using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _inventoryManagerInstance;

    public List<ItemData> _startEquipments;

    public List<Item_Inventory> _defaultInventory;
    public Dictionary<ItemData, Item_Inventory> _defaultInventoryDictianory;

    public List<Item_Inventory> _stashInventory;
    public Dictionary<ItemData, Item_Inventory> _stashInventoryDictianory;

    public List<Item_Inventory> _equipmentInventory;
    public Dictionary<ItemData_Equipment, Item_Inventory> _equipmentInventoryDictianory;

    [Header("Inventory UI")]
    [SerializeField] 
    private Transform _inventorySlotParent;
    [SerializeField] 
    private Transform _stashSlotParent;
    [SerializeField] 
    private Transform _equpmentSlotParent;
    [SerializeField]
    private Transform _statSlotParent;

    private UI_ItemSlot[] _inventoryItemSlot;
    private UI_ItemSlot[] _stashItemSlot;
    private UI_EquipmentSlot[] _equipmentSlot;
    private UI_StatSlot[] _statSlot;

    [Header("Item Cooldown")]
    private float _postionUsageCooldown;
    private float _lastTimeUsedPotion;
    private float _armorEffectUsageCooldown;
    private float _lastTimeUsedArmorEffect;

    private void Awake()
    {
        if (_inventoryManagerInstance == null)
            _inventoryManagerInstance = this;
        else
            Destroy( _inventoryManagerInstance );
    }

    private void Start()
    {
        _defaultInventory = new List<Item_Inventory>();
        _defaultInventoryDictianory = new Dictionary<ItemData, Item_Inventory>();

        _stashInventory = new List<Item_Inventory>();
        _stashInventoryDictianory = new Dictionary<ItemData, Item_Inventory>();

        _equipmentInventory = new List<Item_Inventory>();
        _equipmentInventoryDictianory = new Dictionary<ItemData_Equipment, Item_Inventory>();

        _inventoryItemSlot = _inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        _stashItemSlot = _stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        _equipmentSlot = _equpmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        _statSlot = _statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        AddStartingItem();
    }

    private void AddStartingItem()
    {
        for (int i = 0; i < _startEquipments.Count; i++)
        {
            AddItem(_startEquipments[i]);
        }
    }

    private void Update()
    {
        
    }

    public void AddItem(ItemData newItemData)
    {
        if (newItemData.ItemType == ItemType.Equipment)
            AddToDefaultInventory(newItemData);
        else if (newItemData.ItemType == ItemType.Material)
            AddToStashInventory(newItemData);

        UpdateSlotUI();
    }

    public void RemoveItem(ItemData deletedItemData)
    {
        if (_defaultInventoryDictianory.TryGetValue(deletedItemData, out Item_Inventory value))
        {
            if (value._stackSize <= 1)
            {
                _defaultInventory.Remove(value);
                _defaultInventoryDictianory.Remove(deletedItemData);
            }
            else
                value.RemoveStack();
        }

        if (_stashInventoryDictianory.TryGetValue(deletedItemData, out Item_Inventory stashValue))
        {
            if (stashValue._stackSize <= 1)
            {
                _stashInventory.Remove(stashValue);
                _stashInventoryDictianory.Remove(deletedItemData);
            }
            else
                stashValue.RemoveStack();
        }

        UpdateSlotUI();
    }

    private void AddToStashInventory(ItemData itemData)
    {
        if (_stashInventoryDictianory.TryGetValue(itemData, out Item_Inventory value))
        {
            value.AddStack();
        }
        else
        {
            Item_Inventory newItem = new Item_Inventory(itemData);
            _stashInventory.Add(newItem);
            _stashInventoryDictianory.Add(itemData, newItem);
        }
    }

    private void AddToDefaultInventory(ItemData _item)
    {
        if (_defaultInventoryDictianory.TryGetValue(_item, out Item_Inventory value))
        {
            value.AddStack();
        }
        else
        {
            Item_Inventory newItem = new Item_Inventory(_item);
            _defaultInventory.Add(newItem);
            _defaultInventoryDictianory.Add(_item, newItem);
        }
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < _inventoryItemSlot.Length; i++)
        {
            _inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < _stashItemSlot.Length; i++)
        {
            _stashItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < _equipmentSlot.Length; i++)
        {
            _equipmentSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < _equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, Item_Inventory> item in _equipmentInventoryDictianory)
            {
                if (item.Key.EquipmentType == _equipmentSlot[i]._slotType)
                    _equipmentSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < _statSlot.Length; i++)
        {
            _statSlot[i].UpdateStatValueUI();
        }

        for (int i = 0; i < _defaultInventory.Count; i++)
        {
            _inventoryItemSlot[i].UpdateSlot(_defaultInventory[i]);
        }

        for (int i = 0; i < _stashInventory.Count; i++)
        {
            _stashItemSlot[i].UpdateSlot(_stashInventory[i]);
        } 
    }

    public void EquipItem(ItemData itemData)
    {
        ItemData_Equipment newEquipment = itemData as ItemData_Equipment;
        Item_Inventory newItem = new Item_Inventory(newEquipment);

        ItemData_Equipment oldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, Item_Inventory> item in _equipmentInventoryDictianory)
        {
            if (item.Key.EquipmentType == newEquipment.EquipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
            UnequipItem(oldEquipment);        

        _equipmentInventory.Add(newItem);
        _equipmentInventoryDictianory.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(itemData);

        UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (_equipmentInventoryDictianory.TryGetValue(itemToRemove, out Item_Inventory value))
        {
            _equipmentInventory.Remove(value);
            _equipmentInventoryDictianory.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public bool CraftEquipment(ItemData_Equipment itemToCraft, List<Item_Inventory> requireMaterials)
    {
        List<Item_Inventory> materialsForCrafting = new List<Item_Inventory>();

        for (int i = 0; i < requireMaterials.Count; i++)
        {
            if (_stashInventoryDictianory.TryGetValue(requireMaterials[i]._itemData, out Item_Inventory stashValue))
            {
                if (stashValue._stackSize < requireMaterials[i]._stackSize)
                {
                    Debug.Log("Not Enough Materials");
                    return false;
                }
                else
                { 
                    materialsForCrafting.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("Not Enough Materials");
                return false;
            }
        }

        for (int i = 0; i < materialsForCrafting.Count; i++)
        {
            RemoveItem(materialsForCrafting[i]._itemData);
        }

        AddItem(itemToCraft);
        Debug.Log("Crated Item : " + itemToCraft.ItemName);

        return true;
    }

    public List<Item_Inventory> GetEquipmentList() => _equipmentInventory;

    public List<Item_Inventory> GetStashList() => _stashInventory;

    public ItemData_Equipment GetEquipment(EquipmentType type)
    {
        ItemData_Equipment euippedItem = null;

        foreach (KeyValuePair<ItemData_Equipment, Item_Inventory> item in _equipmentInventoryDictianory)
        {
            if (item.Key.EquipmentType == type)
                euippedItem = item.Key;
        }

        return euippedItem;
    }

    public void UsePotion()
    {
        ItemData_Equipment currentPotion = GetEquipment(EquipmentType.Flask);
        if (currentPotion == null)
            return;

        if (Time.time > _lastTimeUsedPotion + _postionUsageCooldown)
        {
            _postionUsageCooldown = currentPotion._itemCooldown;
            currentPotion.ExecuteItemEffect(null);
            _lastTimeUsedPotion = Time.time;
        }
        else
            Debug.Log("Using Potion is on Cooldown");
    }

    public bool CanUseArmorEffect()
    {
        ItemData_Equipment equippedArmor = GetEquipment(EquipmentType.Armor);
        if (equippedArmor == null)
            return false;

        if (Time.time > _lastTimeUsedArmorEffect + _armorEffectUsageCooldown)
        {
            _armorEffectUsageCooldown = equippedArmor._itemCooldown;
            _lastTimeUsedArmorEffect = Time.time;
            return true;
        }

        Debug.Log("Armor on cooldown");
        return false;
    }
}
