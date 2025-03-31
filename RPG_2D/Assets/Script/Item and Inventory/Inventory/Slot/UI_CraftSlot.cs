using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    private void OnEnable()
    {
        UpdateSlot(_item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemData_Equipment craftItemData = _item._itemData as ItemData_Equipment;

        InventoryManager._inventoryManagerInstance.CraftEquipment(craftItemData, craftItemData._craftMaterials);
    }
}
