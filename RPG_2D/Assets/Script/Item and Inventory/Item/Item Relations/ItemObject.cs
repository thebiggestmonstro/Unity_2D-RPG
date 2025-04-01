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

        GetComponent<SpriteRenderer>().sprite = _itemData.ItemIcon;
        gameObject.name = "Item Object - " + _itemData.ItemName;
    }

    public void SetupItem(ItemData itemData, Vector2 velocity)
    { 
        _itemData = itemData;
        _rigidbody2D.velocity = velocity;

        SetupItemVisuals();
    }

    public void PickupItem()
    {
        InventoryManager._inventoryManagerInstance.AddItem(_itemData);
        Destroy(gameObject);
    }
}
