using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_DropFromPlayer : ItemObject_Drop
{
    [Header("Player`s Drop")]
    [SerializeField]
    private float _chanceToLooseItems;

    public override void GenerateDrop()
    {
        InventoryManager inventoryInstance = InventoryManager._inventoryManagerInstance;
        List<Item_Inventory> currentEquipment = inventoryInstance.GetEquipmentList();
        List<Item_Inventory> itemsToUnequip = new List<Item_Inventory>();
        List<Item_Inventory> currentStash = inventoryInstance.GetStashList();
        List<Item_Inventory> itemsToDrop = new List<Item_Inventory>();

        foreach (Item_Inventory item in currentEquipment)
        {
            if (Random.Range(0, 100) <= _chanceToLooseItems)
            {
                DropItem(item._itemData);
                itemsToUnequip.Add(item);
            }
        }

        for (int i = 0; i < itemsToUnequip.Count; i++)
        {
            inventoryInstance.UnequipItem(itemsToUnequip[i]._itemData as ItemData_Equipment);
        }

        foreach (Item_Inventory item in currentStash)
        {
            if (Random.Range(0, 100) <= _chanceToLooseItems)
            {
                DropItem(item._itemData);
                itemsToDrop.Add(item);
            }
        }

        for (int i = 0; i < itemsToDrop.Count; i++)
        {
            inventoryInstance.RemoveItem(itemsToDrop[i]._itemData);
        }

        inventoryInstance.UpdateSlotUI();
    }
}
