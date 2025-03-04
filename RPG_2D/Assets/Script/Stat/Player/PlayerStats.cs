using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : BaseCharacterStats
{
    PlayerController _playerController;

    protected override void Start()
    {
        base.Start();

        _playerController = GetComponent<PlayerController>();
    }

    public override void TakeDamage(int opponentAttackPoint)
    {
        base.TakeDamage(opponentAttackPoint);

        _playerController.DoGetDamage();
    }
}
