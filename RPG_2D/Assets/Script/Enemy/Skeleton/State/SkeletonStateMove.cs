using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateMove : SkeletonStateGrounded
{
    public SkeletonStateMove(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName, enemyController)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _skeletonController.SetVelocity(_enemyBaseController._moveSpeed * _skeletonController._facingDir, _rigidbody2D.velocity.y);

        if (_skeletonController.DoDetectIsFacingWall() || !_skeletonController.DoDetectIsGrounded())
        {
            _skeletonController.Flip();
            _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }
    }
}
