using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TextMeshProUGUI _itemText;

    public ItemInventory _item;

    public void UpdateSlot(ItemInventory newItem)
    {
        _item = newItem;

        _itemImage.color = Color.white;

        if (_item != null)
        {
            _itemImage.sprite = _item._itemData.ItemIcon;

            if (_item._stackSize > 1)
            {
                _itemText.text = _item._stackSize.ToString();
            }
            else
            {
                _itemText.text = "";
            }
        }
    }
}
