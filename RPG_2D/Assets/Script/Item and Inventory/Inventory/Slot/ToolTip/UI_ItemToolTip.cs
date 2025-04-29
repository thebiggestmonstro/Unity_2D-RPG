using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemNameText;
    [SerializeField]
    private TextMeshProUGUI _itemTypeText;
    [SerializeField]
    private TextMeshProUGUI _itemDescriptionText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowToolTip(ItemData_Equipment item)
    {
        if (item == null)
            return;

        _itemNameText.text = item._itemName;
        _itemTypeText.text = item.EquipmentType.ToString();
        _itemDescriptionText.text = item.GetDescription();

        //if (_itemNameText.text.Length > 13)
        //    _itemNameText.fontSize *= 0.7f;
        //else
        //    _itemNameText.fontSize = 35;

        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
