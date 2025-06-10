using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : BaseCharacterStats
{
    PlayerController _playerController;
    ItemObject_DropFromPlayer _ItemDropSystem;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _playerController = GetComponent<PlayerController>();
        _ItemDropSystem = GetComponent<ItemObject_DropFromPlayer>();
    }

    public override void TakeDamage(int opponentAttackPoint)
    {
        base.TakeDamage(opponentAttackPoint);
    }

    protected override void Die()
    {
        base.Die();
        _playerController.Die();
        _ItemDropSystem.GenerateDrop();
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);

        if (_isDead)
            return;

        if (damage > GetMaxHealthValue() * .3f)
            _playerController.SetupKnockbackPower(new Vector2(10, 6));

        ItemData_Equipment equippedArmor = InventoryManager._inventoryManagerInstance.GetEquipment(EquipmentType.Armor);
        if (equippedArmor && InventoryManager._inventoryManagerInstance.CanUseArmorEffect())
            equippedArmor.ExecuteItemEffect(_playerController.transform);
    }

    public override void EvadeSuccess()
    {
        _playerController._skillManager._skillDodge.CreateMirageOnDodge();
    }

    public void CloneDoDamage(BaseCharacterStats targetStats, float multiplier)
    {
        if (CheckTargetCanEvadeAttack(targetStats))
            return;

        int totalDamage = _attackPoint.GetValue() + _strength.GetValue();

        if (multiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * multiplier);

        if (CheckCanGiveCriticalDamage())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(targetStats, totalDamage);
        targetStats.TakeDamage(totalDamage);

        // GiveMagicalDamage(targetStats); 
    }
}
