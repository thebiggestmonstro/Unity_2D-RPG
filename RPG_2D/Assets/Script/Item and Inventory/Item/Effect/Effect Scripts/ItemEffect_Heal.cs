using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
public class ItemEffect_Heal : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _healPercent;

    public override void ExecuteEffect(Transform playerPosition)
    {
         PlayerStats playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * _healPercent);

        playerStats.HealHealth(healAmount);
    }
}
