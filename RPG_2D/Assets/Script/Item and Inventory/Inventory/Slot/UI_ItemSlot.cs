using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    protected PlayerController _playerContorller;

    [SerializeField] 
    private Image _itemImage;
    [SerializeField] 
    private TextMeshProUGUI _itemText;

    public Item_Inventory _item;

    void Start()
    {
        _playerContorller = FindObjectOfType<PlayerController>();
    }

    public void UpdateSlot(Item_Inventory newItem)
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

    public void CleanUpSlot()
    {
        _item = null;

        _itemImage.sprite = null;
        _itemImage.color = Color.clear;
        _itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (_playerContorller._isRemovedItem == true)
        {
            InventoryManager._inventoryManagerInstance.RemoveItem(_item._itemData);
            return;
        }

        if (_item._itemData.ItemType == ItemType.Equipment)
            InventoryManager._inventoryManagerInstance.EquipItem(_item._itemData);
    }
}
