using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using UnityEngine;

public enum StatType
{
    _strength,
    _agility,
    _intelligence,
    _vitality,
    _maxHealth,
    _armor,
    _evasion,
    _magicResistance,
    _attackPoint,
    _critChance,
    _critPower,
    _fireDamage,
    _iceDamage,
    _lightningDamage,
}

public class BaseCharacterStats : MonoBehaviour
{
    private BaseEffectController _effectController;

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

    [SerializeField]
    private float _alimentDuration = 4.0f;
    private float _ignitedTimer;
    private float _freezedTimer;
    private float _shockedTimer;

    private float _igniteDamageCooldown = 0.3f;
    private float _igniteDamageTimer;
    private int _ignitedDamage;

    [SerializeField]
    private GameObject _thunderLightningPrefab;
    private int _thunderDamage;

    public int _currentHealth;
    public System.Action onHealthChanged;
    public bool _isDead { get; private set; }

    protected virtual void Awake()
    {
        _currentHealth = GetMaxHealthValue();
    }

    protected virtual void Start()
    {
        _effectController = GetComponent<BaseEffectController>();
    }

    protected virtual void Update()
    {
        _ignitedTimer -= Time.deltaTime;
        _freezedTimer -= Time.deltaTime;
        _shockedTimer -= Time.deltaTime;
        _igniteDamageTimer -= Time.deltaTime;

        if (_ignitedTimer < 0)
            _isIgnited = false;

        if (_freezedTimer < 0)
            _isFreezed = false;

        if (_shockedTimer < 0)
            _isShocked = false;

        GiveIgniteDamage();
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

        GetComponent<BaseCharacterController>().DoGetDamage();
        _effectController.StartCoroutine("DoMakeFlashFX");

        if (_currentHealth <= 0 && !_isDead)
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

        AttemptToApplyAliments(targetStats, fireDamage, iceDamage, lightningDamage);
    }

    private void AttemptToApplyAliments(BaseCharacterStats targetStats, int fireDamage, int iceDamage, int lightningDamage)
    {
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

        if (canApplyShock)
            targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(lightningDamage * .1f));

        targetStats.ApplyAliments(canApplyIgnite, canApplyFreeze, canApplyShock);
    }

    private int CheckTargetMagicResistance(BaseCharacterStats targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= targetStats._magicResistance.GetValue() + (targetStats._intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAliments(bool isIgnited, bool isFreezed, bool isShocked)
    {
        bool canApplyIgnite = !_isIgnited && !_isFreezed && !_isShocked;
        bool canApplyFreeze = !_isIgnited && !_isFreezed && !_isShocked;
        bool canApplyShock = !_isIgnited && !_isFreezed;

        if (isIgnited && canApplyIgnite)
        {
            _isIgnited = isIgnited;
            _ignitedTimer = _alimentDuration;

            _effectController.PaintIgnitedColorFX(_ignitedTimer);
        }

        if (isFreezed && canApplyFreeze)
        {
            _isFreezed = isFreezed;
            _freezedTimer = _alimentDuration;

            float slowPercentage = 0.2f;
            GetComponent<BaseCharacterController>().MakeCharacterSlow(slowPercentage, _freezedTimer);
            _effectController.PaintFreezedColorFX(_freezedTimer);
        }

        if (isShocked && canApplyShock)
        {
            // 적이 전기속성 상태이상에 걸리지 않은 경우
            if (!_isShocked)
            {
                ApplyShock(isShocked);
            }
            // 적이 이미 전기속성 상태이상에 걸린 경우
            else
            {
                if (GetComponent<PlayerController>() != null)
                    return;

                HitNearestTargetWithThunderStrike();
            }
        }
    }

    private void GiveIgniteDamage()
    {
        if (_igniteDamageTimer < 0 && _isIgnited)
        {
            DecreaseHealth(_ignitedDamage);

            if (_currentHealth <= 0.0f && !_isDead)
                Die();

            _igniteDamageTimer = _igniteDamageCooldown;
        }
    }

    public void SetIgniteDamage(int damage) =>  _ignitedDamage = damage;
    public void SetupShockStrikeDamage(int damage) => _thunderDamage = damage;

    public void ApplyShock(bool isShocked)
    {
        if (_isShocked)
            return;

        _shockedTimer = _alimentDuration;
        _isShocked = isShocked;

        _effectController.PaintShockedColorFX(_alimentDuration);
    }

    private void HitNearestTargetWithThunderStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

            if (closestEnemy == null)           
                closestEnemy = transform;
        }

        if (closestEnemy != null)
        {
            GameObject newThunder = Instantiate(_thunderLightningPrefab, transform.position, Quaternion.identity);
            newThunder.GetComponent<ThunderController>().SetupThunder(_thunderDamage, closestEnemy.GetComponent<BaseCharacterStats>());
        }
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
        _isDead = true;
    }

    public virtual void HealHealth(int amount)
    {
        _currentHealth += amount;

        if(_currentHealth > GetMaxHealthValue())
            _currentHealth = GetMaxHealthValue();

        if(onHealthChanged != null)
            onHealthChanged();
    }

    public virtual void IncreaseStats(int modifier, float increaseDuration, CharacterStats statToModify)
    {
        StartCoroutine(ModifyStatTemp(modifier, increaseDuration, statToModify));
    }

    IEnumerator ModifyStatTemp(int modifier, float increaseDuration, CharacterStats statToModify)
    { 
        statToModify.AddModifier(modifier);

        yield return new WaitForSeconds(increaseDuration);

        statToModify.RemoveModifier(modifier);
    }

    public CharacterStats GetStatByStatType(StatType typeofStat)
    {
        if (typeofStat == StatType._strength)
            return _strength;
        else if (typeofStat == StatType._agility)
            return _agility;
         else if (typeofStat == StatType._intelligence)
            return _intelligence;
         else if (typeofStat == StatType._vitality)
            return _vitality;
         else if (typeofStat == StatType._maxHealth)
            return _maxHealth;
         else if (typeofStat == StatType._armor)
            return _armor;
         else if (typeofStat == StatType._evasion)
            return _evasion;
         else if (typeofStat == StatType._magicResistance)
            return _magicResistance;
         else if (typeofStat == StatType._attackPoint)
            return _attackPoint;
         else if (typeofStat == StatType._critChance)
            return _critChance;         
        else if (typeofStat == StatType._critPower)
            return _critPower;         
        else if (typeofStat == StatType._fireDamage)
            return _fireDamage;         
        else if (typeofStat == StatType._iceDamage)
            return _iceDamage;
        else if (typeofStat == StatType._lightningDamage)
            return _lightningDamage;
        
       return null;
    }
}
