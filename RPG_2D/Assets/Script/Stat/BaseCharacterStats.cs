using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStats : MonoBehaviour
{
    public CharacterStats _attackPoint;
    public CharacterStats _maxHealth;
    public CharacterStats _strength;

    [SerializeField]
    private int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth.GetValue();

        _attackPoint.AddModifier(4);
    }

    public virtual void GiveDamage(BaseCharacterStats targetStats)
    {
        int totalDamage = _attackPoint.GetValue() + _strength.GetValue();

        targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int opponentAttackPoint)
    {
        _currentHealth -= opponentAttackPoint;

        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Die() 
    {

    }
}
