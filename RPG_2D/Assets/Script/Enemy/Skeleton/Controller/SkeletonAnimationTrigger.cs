using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
   SkeletonController _skeletonController => GetComponentInParent<SkeletonController>();

    private void AnimationTrigger()
    {
        _skeletonController.AnimationTrigger();
    }
}
