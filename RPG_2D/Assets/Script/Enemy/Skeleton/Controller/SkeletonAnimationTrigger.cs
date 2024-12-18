using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
   SkeletonController _skeletonController => GetComponentInParent<SkeletonController>();

    // SkeletonController의 AnimationTrigger 함수를 호출
    private void AnimationTrigger()
    {
        _skeletonController.AnimationTrigger();
    }
}
