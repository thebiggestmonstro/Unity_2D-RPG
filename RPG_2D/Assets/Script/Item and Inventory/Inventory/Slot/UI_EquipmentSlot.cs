using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager._inventoryManagerInstance.UnequipItem(_item._itemData as ItemData_Equipment);
        InventoryManager._inventoryManagerInstance.AddItem(_item._itemData as ItemData_Equipment);

        CleanUpSlot();
    }
}
