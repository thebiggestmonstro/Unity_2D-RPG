using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public CharacterStats _magicResistance;

    [Header("Offensive Stats")]
    public CharacterStats _attackPoint;
    public CharacterStats _critChance;
    public CharacterStats _critPower;

    [Header("Magical Stats")]
    public CharacterStats _fireDamage;
    public CharacterStats _iceDamage;
    public CharacterStats _lightningDamage;

    public bool _isIgnited;
    public bool _isFreezed;
    public bool _isShocked;

    private float _ignitedTimer;
    private float _freezedTimer;
    private float _shockedTimer;

    private float _igniteDamageCooldown = 0.3f;
    private float _igniteDamageTimer;
    private int _ignitedDamage;

    public int _currentHealth;
    public System.Action onHealthChanged;

    protected virtual void Awake()
    {
        _currentHealth = GetMaxHealthValue();
    }

    protected virtual void Start()
    {
        _critPower.SetDefaultValue(150);
    }

    protected virtual void Update()
    {
        _ignitedTimer -= Time.deltaTime;
        _freezedTimer -= Time.deltaTime;
        _shockedTimer -= Time.deltaTime;
        _igniteDamageTimer -= Time.deltaTime;

        if (_ignitedTimer < 0)
            _isIgnited = false;

        if(_freezedTimer < 0)
            _isFreezed = false;

        if (_shockedTimer < 0)
            _isShocked = false;

        if (_igniteDamageTimer < 0 && _isIgnited)
        {
            DecreaseHealth(_ignitedDamage);

            if (_currentHealth <= 0.0f)
                Die();

            _igniteDamageTimer = _igniteDamageCooldown;
        }
    }

    public virtual void GiveDamage(BaseCharacterStats targetStats)
    {
        if (CheckTargetCanEvadeAttack(targetStats))
            return;

        int totalDamage = _attackPoint.GetValue() + _strength.GetValue();

        if (CheckCanGiveCriticalDamage())
            totalDamage = CalculateCriticalDamage(totalDamage);

        totalDamage = CheckTargetArmor(targetStats, totalDamage);

        // targetStats.TakeDamage(totalDamage);
        GiveMagicalDamage(targetStats);
    }

    private bool CheckTargetCanEvadeAttack(BaseCharacterStats targetStats)
    {
        int totalEvasion = targetStats._evasion.GetValue() + targetStats._agility.GetValue();

        if (_isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Target Evaded Attack...");
            return true;
        }

        return false;
    }

    private int CheckTargetArmor(BaseCharacterStats targetStats, int totalDamage)
    {
        if (targetStats._isFreezed)
            totalDamage -= Mathf.RoundToInt(targetStats._armor.GetValue() * 0.8f);
        else
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
        DecreaseHealth(opponentAttackPoint);

        if (_currentHealth <= 0)
            Die();
    }

    public virtual void GiveMagicalDamage(BaseCharacterStats targetStats)
    {
        int fireDamage = _fireDamage.GetValue();
        int iceDamage = _iceDamage.GetValue();
        int lightningDamage = _lightningDamage.GetValue();

        int totalMagicalDamage = fireDamage + iceDamage + lightningDamage + _intelligence.GetValue();
        totalMagicalDamage = CheckTargetMagicResistance(targetStats, totalMagicalDamage);

        targetStats.TakeDamage(totalMagicalDamage);

        if (Mathf.Max(fireDamage, iceDamage, lightningDamage) <= 0)
            return;

        bool canApplyIgnite = fireDamage > iceDamage && fireDamage > lightningDamage;
        bool canApplyFreeze = iceDamage > fireDamage && iceDamage > lightningDamage;
        bool canApplyShock = lightningDamage > iceDamage && lightningDamage > fireDamage;

        while (!canApplyIgnite && !canApplyFreeze && !canApplyShock)
        {
            if (Random.value < 0.5f && fireDamage > 0)
            {
                canApplyIgnite = true;
                targetStats.ApplyAliments(canApplyIgnite, canApplyFreeze, canApplyShock);
                return;
            }
            else if (Random.value < 0.5f && iceDamage > 0)
            {
                canApplyFreeze = true;
                targetStats.ApplyAliments(canApplyIgnite, canApplyFreeze, canApplyShock);
                return;
            }
            else if (Random.value < 0.5f && lightningDamage > 0)
            {
                canApplyShock = true;
                targetStats.ApplyAliments(canApplyIgnite, canApplyFreeze, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
            targetStats.SetIgniteDamage(Mathf.RoundToInt(fireDamage * 0.2f));

        targetStats.ApplyAliments(canApplyIgnite, canApplyFreeze, canApplyShock);
    }

    private static int CheckTargetMagicResistance(BaseCharacterStats targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= targetStats._magicResistance.GetValue() + (targetStats._intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAliments(bool isIgnited, bool isFreezed, bool isShocked)
    {
        if (isIgnited || isFreezed || isShocked)
            return;

        if (isIgnited)
        {
            _isIgnited = isIgnited;
            _ignitedTimer = 4.0f;
        }

        if (isFreezed)
        { 
            _isFreezed = isFreezed;
            _freezedTimer = 2.0f;
        }

        if (isShocked)
        {
            _isShocked = isShocked;
            _shockedTimer = 2.0f;
        }
    }

    public void SetIgniteDamage(int damage)
    {
        _ignitedDamage = damage;
    }

    public int GetMaxHealthValue()
    {
        return _maxHealth.GetValue() + _vitality.GetValue() * 5;
    }

    protected virtual void DecreaseHealth(int damage)
    {
        _currentHealth -= damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die() 
    {

    }
}
