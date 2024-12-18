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

    // �� �����Ӹ��� ����Ǵ� MoveState�� Update�Լ��� ��� ������ ���� ó����
    public override void Update()
    {
        base.Update();

        // �⺻������ Skeleton�� �����̰Բ� �ϵ�,
        _skeletonController.SetVelocity(_enemyBaseController._moveSpeed * _skeletonController._facingDir, _rigidbody2D.velocity.y);

        // ���� ���� �����ϰ� �ְų�, ���� ��� ���� �ʴٸ�
        if (_skeletonController.DoDetectIsFacingWall() || !_skeletonController.DoDetectIsGrounded())
        {
            // ������ ��ȯ�ϰ� IdleState�� ��ȯ��
            _skeletonController.Flip();
            _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }
    }
}
