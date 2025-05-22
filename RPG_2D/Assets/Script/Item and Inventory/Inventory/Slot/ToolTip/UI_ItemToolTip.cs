using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField]
    private TextMeshProUGUI _itemNameText;
    [SerializeField]
    private TextMeshProUGUI _itemTypeText;
    [SerializeField]
    private TextMeshProUGUI _itemDescriptionText;

    [SerializeField] 
    private int _defaultFontSize = 32;

    public void ShowToolTip(ItemData_Equipment item)
    {
        if (item == null)
            return;

        _itemNameText.text = item._itemName;
        _itemTypeText.text = item.EquipmentType.ToString();
        _itemDescriptionText.text = item.GetDescription();

        AdjustFontSize(_itemNameText);
        AdjustPosition();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        _itemNameText.fontSize = _defaultFontSize;
    }
}
