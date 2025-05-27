using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();
    }

    public void SetupCraftSlot(ItemData_Equipment data)
    {
        if (data == null)
            return;

        _item._itemData = data;
        _itemImage.sprite = data._itemIcon;
        _itemText.text = data._itemName;

        //if (_itemText.text.Length > 12)
        //    _itemText.fontSize = _itemText.fontSize * .7f;
        //else
        //    _itemText.fontSize = 24;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _uiManager._craftWindow.SetupCraftWindow(_item._itemData as ItemData_Equipment);
    }
}
