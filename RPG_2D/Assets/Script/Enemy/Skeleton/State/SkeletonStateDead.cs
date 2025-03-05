using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateDead : EnemyState
{
    private SkeletonController _skeletonController;

    public SkeletonStateDead(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController skeletonController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = skeletonController;
    }

    public override void Enter()
    {
        base.Enter();

        _skeletonController._animator.SetBool(_skeletonController._lastAnimBoolName, true);
        _skeletonController._animator.speed = 0;
        _skeletonController._capsuleCollider.enabled = false;

        _stateTimer = 0.15f;
    }

    public override void Exit()
    {
        base.Exit();

        if (_stateTimer > 0)
            _rigidbody2D.velocity = new Vector2(0, 10);
    }
}
