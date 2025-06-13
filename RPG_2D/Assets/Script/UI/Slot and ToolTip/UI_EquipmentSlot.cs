using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType _slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_item == null)
            return;

        InventoryManager._inventoryManagerInstance.UnequipItem(_item._itemData as ItemData_Equipment);
        InventoryManager._inventoryManagerInstance.AddItem(_item._itemData as ItemData_Equipment);

        _uiManager._itemToolTip.HideToolTip();

        CleanUpSlot();
    }
}
