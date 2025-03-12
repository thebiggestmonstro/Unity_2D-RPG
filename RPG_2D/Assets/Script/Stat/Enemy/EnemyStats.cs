using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BaseCharacterStats
{
    EnemyController _enemyController;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _enemyController = GetComponent<SkeletonController>();
    }

    public override void TakeDamage(int opponentAttackPoint)
    {
        base.TakeDamage(opponentAttackPoint);
    }

    protected override void Die()
    {
        base.Die();
        _enemyController.Die();
    }
}
