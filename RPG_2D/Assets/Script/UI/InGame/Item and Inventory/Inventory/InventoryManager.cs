using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISaveManager
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
    public float _potionUsageCooldown;
    private float _lastTimeUsedPotion;
    private float _armorEffectUsageCooldown;
    private float _lastTimeUsedArmorEffect;

    [Header("Data base")]
    public List<ItemData> _itemDataBase;
    public List<Item_Inventory> _loadedItems;
    public List<ItemData_Equipment> _loadedEquipments;

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
        foreach (ItemData_Equipment item in _loadedEquipments)
        {
            EquipItem(item);
        }

        if (_loadedItems.Count > 0)
        {
            foreach (Item_Inventory item in _loadedItems)
            {
                for (int i = 0; i < item._stackSize; i++)
                {
                    AddItem(item._itemData);
                }
            }

            return;
        }

        for (int i = 0; i < _startEquipments.Count; i++)
        {
            if (_startEquipments[i])
                AddItem(_startEquipments[i]);
        }
    }

    public void AddItem(ItemData newItemData)
    {
        if (newItemData.ItemType == ItemType.Equipment && CanAddItem())
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

        for (int i = 0; i < _defaultInventory.Count; i++)
        {
            _inventoryItemSlot[i].UpdateSlot(_defaultInventory[i]);
        }

        for (int i = 0; i < _stashInventory.Count; i++)
        {
            _stashItemSlot[i].UpdateSlot(_stashInventory[i]);
        }

        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < _statSlot.Length; i++) 
        {
            _statSlot[i].UpdateStatValueUI();
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
        foreach (Item_Inventory requiredItem in requireMaterials)
        {
            if (_stashInventoryDictianory.TryGetValue(requiredItem._itemData, out Item_Inventory stashItem))
            {
                if (stashItem._stackSize < requiredItem._stackSize)
                {
                    Debug.Log("Not enough materials: " + requiredItem._itemData.name);
                    return false;
                }
            }
            else
            {
                Debug.Log("Materials not found in stash: " + requiredItem._itemData.name);
                return false;
            }
        }

        foreach (Item_Inventory requiredMaterial in requireMaterials)
        {
            for (int i = 0; i < requiredMaterial._stackSize; i++)
            {
                RemoveItem(requiredMaterial._itemData);
            }
        }

        AddItem(itemToCraft);
        Debug.Log("Craft is succsesful: " + itemToCraft.name);
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

        if (Time.time > _lastTimeUsedPotion + _potionUsageCooldown)
        {
            _potionUsageCooldown = currentPotion._itemCooldown;
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

    public bool CanAddItem()
    {
        if (_defaultInventory.Count >= _inventoryItemSlot.Length)
            return false;
        
        return true;
    }

    public void SaveData(ref GameData data)
    {
        data._inventoryData.Clear();
        data._equipmentId.Clear();

        foreach (KeyValuePair<ItemData, Item_Inventory> pair in _defaultInventoryDictianory)
        {
            data._inventoryData.Add(pair.Key._itemId, pair.Value._stackSize);
        }

        foreach (KeyValuePair<ItemData, Item_Inventory> pair in _stashInventoryDictianory)
        {
            data._inventoryData.Add(pair.Key._itemId, pair.Value._stackSize);
        }

        foreach (KeyValuePair<ItemData_Equipment, Item_Inventory> pair in _equipmentInventoryDictianory)
        {
            data._equipmentId.Add(pair.Key._itemId);
        }
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, int> pair in data._inventoryData)
        {
            foreach (var item in _itemDataBase)
            {
                if (item != null && item._itemId == pair.Key)
                {
                    Item_Inventory itemToLoad = new Item_Inventory(item);
                    itemToLoad._stackSize = pair.Value;

                    _loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (string loadedItemId in data._equipmentId)
        {
            foreach (var item in _itemDataBase)
            {
                if (item != null && loadedItemId == item._itemId)
                {
                    _loadedEquipments.Add(item as ItemData_Equipment);
                }
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Fill up item data base")]
    private void FillUpItemDataBase() => _itemDataBase = new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
#endif
}
