using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SkeletonAnimationTrigger : MonoBehaviour
{
   SkeletonController _skeletonController => GetComponentInParent<SkeletonController>();

    // SkeletonController의 AnimationTrigger 함수를 호출
    private void AnimationTrigger()
    {
        _skeletonController.AnimationTrigger();
    }

    private void AttackAnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_skeletonController._attackCheck.position, _skeletonController._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<PlayerController>() != null)
            {
                hit.GetComponent<PlayerController>().DoGetDamage();
            }
        }
    }

    private void DoOpenCounterWindow() => _skeletonController.DoOpenCounterAttackWindow();
    private void DoCloseCounterWindow() => _skeletonController.DoCloseCounterAttackWindow();
}
