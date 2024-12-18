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

    // 매 프레임마다 실행되는 MoveState의 Update함수의 경우 다음과 같이 처리함
    public override void Update()
    {
        base.Update();

        // 기본적으로 Skeleton을 움지이게끔 하되,
        _skeletonController.SetVelocity(_enemyBaseController._moveSpeed * _skeletonController._facingDir, _rigidbody2D.velocity.y);

        // 만약 벽을 마주하고 있거나, 땅을 딛고 있지 않다면
        if (_skeletonController.DoDetectIsFacingWall() || !_skeletonController.DoDetectIsGrounded())
        {
            // 방향을 전환하고 IdleState로 변환함
            _skeletonController.Flip();
            _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }
    }
}
