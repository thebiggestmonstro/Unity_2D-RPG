using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(fileName = "Armor Effect", menuName = "Data/Item Effect/Armor Effect")]

public class ItemEffect_Armor : ItemEffect
{
    [SerializeField]
    private float _freezeDuration;
    [SerializeField]
    private float _freezeRadius = 2.0f;

    public override void ExecuteEffect(Transform playerPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition.position, _freezeRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                hit.GetComponent<EnemyController>().FreezeEnemy(_freezeDuration);
            }
        }
    }
}
