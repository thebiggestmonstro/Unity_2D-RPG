using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect_ThunderStrikeController : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>() != null)
        {
            PlayerStats playerStat = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();
            EnemyStats enemyTarget = collision.GetComponent<EnemyStats>();
            playerStat.GiveMagicalDamage(enemyTarget);
        }
    }
}
