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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller._attackCheck.position, _controller._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        { 
            if(hit.GetComponent<EnemyController>() != null)
            {
                hit.GetComponent<EnemyController>().DoGetDamage();
            }
        }
    }
}
