using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Drop : MonoBehaviour
{
    [SerializeField]
    private GameObject _droppableItemPrefab;

    [SerializeField]
    private int _amountOfDropItems;
    [SerializeField]
    private ItemData[] _droppableItemList;
    private List<ItemData> _dropList = new List<ItemData>();

    public virtual void DropItem(ItemData dropItem)
    {
        GameObject newDropItem = Instantiate(_droppableItemPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDropItem.GetComponent<ItemObject>().SetupItem(dropItem, randomVelocity);
    }

    public virtual void GenerateDrop()
    {
        if (_droppableItemList.Length == 0)
        {
            Debug.Log("Item Pool is empty. Enemy cannot drop items.");
            return;
        }

        foreach (ItemData item in _droppableItemList)
        {
            if (item != null && Random.Range(0, 100) < item._dropChance)
                _dropList.Add(item);
        }

        for (int i = 0; i < _amountOfDropItems; i++)
        {
            if (_dropList.Count > 0)
            {
                int randomIndex = Random.Range(0, _dropList.Count);
                ItemData itemToDrop = _dropList[randomIndex];

                DropItem(itemToDrop);
                _dropList.Remove(itemToDrop);
            }
        }
    }
}
