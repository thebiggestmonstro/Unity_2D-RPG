using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private ItemData _itemData;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    private void SetupItemVisuals()
    {
        if (_itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = _itemData._itemIcon;
        gameObject.name = "Item Object - " + _itemData._itemName;
    }

    public void SetupItem(ItemData itemData, Vector2 velocity)
    { 
        _itemData = itemData;
        _rigidbody2D.velocity = velocity;

        SetupItemVisuals();
    }

    public void PickupItem()
    {
        if (InventoryManager._inventoryManagerInstance.CanAddItem() == false && _itemData.ItemType == ItemType.Equipment)
        {
            _rigidbody2D.velocity = new Vector2(0, 7);
            return;
        }

        InventoryManager._inventoryManagerInstance.AddItem(_itemData);
        Destroy(gameObject);
    }
}
