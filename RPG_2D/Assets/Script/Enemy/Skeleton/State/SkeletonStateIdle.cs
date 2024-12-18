using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : SkeletonStateGrounded
{
    public SkeletonStateIdle(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName, enemyController)
    {

    }

    // IdleState에서는 들어간 순간에 _stateTimer를 _idleTimer 값으로 설정함
    public override void Enter()
    {
        base.Enter();

        _stateTimer = _enemyBaseController._idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // 매 프레임마다 실행하는 Update에서는 _stateTimer가 0보다 작다면 IdleState를 탈출하고 MoveState로 들어감 
    public override void Update()
    {
        base.Update();
        
        if (_stateTimer < 0)
        {
            _enemyStateMachine.ChangeState(_skeletonController._moveState);
        }
    }
}
