using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private ItemData _itemData;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.ItemIcon;
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
