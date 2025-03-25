using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BaseCharacterStats
{
    EnemyController _enemyController;

    [Header("Level Details")]
    [SerializeField]
    private int _level = 1;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _modifierPercentage = 0.2f;

    protected override void Awake()
    {
        ApplyModiferEachStats();

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

    private void ModifyStat(CharacterStats stat)
    {
        for (int i = 1; i < _level; i++)
        { 
            float modifyValue = stat.GetValue() * _modifierPercentage;

            stat.AddModifier(Mathf.RoundToInt(modifyValue));
        }
    }

    private void ApplyModiferEachStats()
    {
        ModifyStat(_strength);
        ModifyStat(_agility);
        ModifyStat(_intelligence);
        ModifyStat(_vitality);

        ModifyStat(_maxHealth);
        ModifyStat(_armor);
        ModifyStat(_evasion);
        ModifyStat(_magicResistance);

        ModifyStat(_attackPoint);
        ModifyStat(_critChance);
        ModifyStat(_critPower);

        ModifyStat(_fireDamage);
        ModifyStat(_iceDamage);
        ModifyStat(_lightningDamage);
    }
}
