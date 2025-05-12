using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected PlayerController _playerContorller;

    [SerializeField] 
    protected Image _itemImage;
    [SerializeField] 
    protected TextMeshProUGUI _itemText;

    public Item_Inventory _item;
    protected UIManager _uiManager;

    protected virtual void Start()
    {
        _playerContorller = FindObjectOfType<PlayerController>();
        _uiManager = GetComponentInParent<UIManager>();
    }

    public void UpdateSlot(Item_Inventory newItem)
    {
        _item = newItem;

        _itemImage.color = Color.white;

        if (_item != null)
        {
            _itemImage.sprite = _item._itemData._itemIcon;

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
        if (_item == null)
            return;

        if (_playerContorller._isRemovedItem == true)
        {
            InventoryManager._inventoryManagerInstance.RemoveItem(_item._itemData);
            return;
        }

        if (_item._itemData.ItemType == ItemType.Equipment)
            InventoryManager._inventoryManagerInstance.EquipItem(_item._itemData);

        _uiManager._itemToolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item == null)
            return;

        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 600)
            xOffset = -150;
        else
            xOffset = 150;

        if (mousePosition.y > 320)
            yOffset = -150;
        else
            yOffset = 150;

        _uiManager._itemToolTip.ShowToolTip(_item._itemData as ItemData_Equipment);
        _uiManager._itemToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_item == null)
            return;

        _uiManager._itemToolTip.HideToolTip();
    }
}
