using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class ItemEffect_Buff : ItemEffect
{
    private PlayerStats _playerStats;

    [SerializeField]
    private StatType _statToBuff;
    [SerializeField]
    private int _buffAmount;
    [SerializeField]
    private float _buffDuration;

    public override void ExecuteEffect(Transform enemyPosition)
    {
        _playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();
        _playerStats.IncreaseStats(_buffAmount, _buffDuration, _playerStats.GetStatByStatType(_statToBuff));
    }
}
