using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : EnemyState
{
    SkeletonController _skeletonController;

    public SkeletonStateIdle(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController)
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        _skeletonController = enemyController;
    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = _enemyBaseController._idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (_stateTimer < 0)
        {
            _enemyStateMachine.ChangeState(_skeletonController._moveState);
        }
    }
}
