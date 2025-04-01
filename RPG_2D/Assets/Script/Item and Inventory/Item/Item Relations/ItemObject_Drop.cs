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
        for (int i = 0; i < _droppableItemList.Length; i++)
        {
            if (Random.Range(0, 100) <= _droppableItemList[i]._dropChance)
                _dropList.Add(_droppableItemList[i]);
        }

        for (int i = 0; i < _amountOfDropItems; i++)
        {
            ItemData randomDropItem = _dropList[Random.Range(0, _dropList.Count - 1)];

            _dropList.Remove(randomDropItem);
            DropItem(randomDropItem);
        }
    }
}
