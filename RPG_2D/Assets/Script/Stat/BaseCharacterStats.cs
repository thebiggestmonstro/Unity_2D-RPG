using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStats : MonoBehaviour
{
    [Header("Primary Stats")]
    public CharacterStats _strength;
    public CharacterStats _agility;
    public CharacterStats _intelligence;
    public CharacterStats _vitality;

    [Header("Defensive Stats")]
    public CharacterStats _maxHealth;
    public CharacterStats _armor;
    public CharacterStats _evasion;

    [Header("Offensive Stats")]
    public CharacterStats _attackPoint;
    public CharacterStats _critChance;
    public CharacterStats _critPower;

    [SerializeField]
    private int _currentHealth;

    protected virtual void Start()
    {
        _critPower.SetDefaultValue(150);
        _currentHealth = _maxHealth.GetValue();
    }

    public virtual void GiveDamage(BaseCharacterStats targetStats)
    {
        if (CheckTargetCanEvadeAttack(targetStats))
            return;

        int totalDamage = _attackPoint.GetValue() + _strength.GetValue();

        if (CheckCanGiveCriticalDamage())
            totalDamage = CalculateCriticalDamage(totalDamage);

        totalDamage = CheckTargetArmor(targetStats, totalDamage);

        targetStats.TakeDamage(totalDamage);
    }

    private bool CheckTargetCanEvadeAttack(BaseCharacterStats targetStats)
    {
        int totalEvasion = targetStats._evasion.GetValue() + targetStats._agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Target Evaded Attack...");
            return true;
        }

        return false;
    }

    private int CheckTargetArmor(BaseCharacterStats targetStats, int totalDamage)
    {
        totalDamage -= targetStats._armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CheckCanGiveCriticalDamage()
    { 
        int totalCritChance = _critChance.GetValue() + _agility.GetValue();

        if (Random.Range(0, 100) <= totalCritChance)
            return true;

        return false;
    }

    private int CalculateCriticalDamage(int damage)
    {
        float totalCriticalPower = (_critPower.GetValue() + _strength.GetValue()) * 0.1f;
        float critDamage = damage * totalCriticalPower;

        Debug.Log("Give Critical Damage!!!");
        return Mathf.RoundToInt(critDamage);
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
