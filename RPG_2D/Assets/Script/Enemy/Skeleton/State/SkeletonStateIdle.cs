using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : SkeletonStateGrounded
{
    public SkeletonStateIdle(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName, enemyController)
    {

    }

    // IdleState������ �� ������ _stateTimer�� _idleTimer ������ ������
    public override void Enter()
    {
        base.Enter();

        _stateTimer = _enemyBaseController._idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // �� �����Ӹ��� �����ϴ� Update������ _stateTimer�� 0���� �۴ٸ� IdleState�� Ż���ϰ� MoveState�� �� 
    public override void Update()
    {
        base.Update();
        
        if (_stateTimer < 0)
        {
            _enemyStateMachine.ChangeState(_skeletonController._moveState);
        }
    }
}
