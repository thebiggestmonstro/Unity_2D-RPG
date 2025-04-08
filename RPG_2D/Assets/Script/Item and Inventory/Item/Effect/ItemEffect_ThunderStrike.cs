using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Thunder Strike")]
public class ItemEffect_ThunderStrike : ItemEffect
{
    [SerializeField]
    private GameObject _thunderStrikePrefab;

    public override void ExecuteEffect(Transform enemyPosition)
    {
        GameObject newThunderStrike = Instantiate(_thunderStrikePrefab, enemyPosition.position, Quaternion.identity);

        Destroy(newThunderStrike, 0.5f);
    }
}
