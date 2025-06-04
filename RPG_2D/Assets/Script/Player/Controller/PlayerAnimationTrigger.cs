using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerController _controller => GetComponentInParent<PlayerController>();

    private void AnimationTrigger()
    {
        _controller.AnimationTrigger();
    }

    private void AttackAnimationTrigger()
    {
        AudioManager._audioManagerInstance.PlaySFX(2, null);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller._attackCheck.position, _controller._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        { 
            if(hit.GetComponent<EnemyController>() != null)
            {
                EnemyStats target = hit.GetComponent<EnemyStats>();

                if(target)
                    _controller._characterStats.GiveDamage(target);

                ItemData_Equipment currentEquipedWeapon = InventoryManager._inventoryManagerInstance.GetEquipment(EquipmentType.Weapon);
                if (currentEquipedWeapon)
                    currentEquipedWeapon.ExecuteItemEffect(target.transform);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager._skillManagerInstance._skillThrowingSword.CreateSword();
    }
}
