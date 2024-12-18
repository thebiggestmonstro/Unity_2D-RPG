using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
   SkeletonController _skeletonController => GetComponentInParent<SkeletonController>();

    // SkeletonController�� AnimationTrigger �Լ��� ȣ��
    private void AnimationTrigger()
    {
        _skeletonController.AnimationTrigger();
    }
}
