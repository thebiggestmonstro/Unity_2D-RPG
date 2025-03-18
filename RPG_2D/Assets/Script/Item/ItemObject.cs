using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private ItemData _itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = _itemData.ItemIcon;
        gameObject.name = "Item Object : "  + _itemData.ItemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            InventoryManager._inventoryManagerInstance.AddItem(_itemData);
            Destroy(gameObject);
        }
    }
}
