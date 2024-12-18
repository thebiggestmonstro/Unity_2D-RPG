using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateAttack : EnemyState
{
    SkeletonController _skeletonController;

    public SkeletonStateAttack(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController)
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    public override void Enter()
    {
        base.Enter();
    }

    // AttakState는 탈출하면서 마지막으로 공격한 시간인 _lastTimeAttacked를 지금 시간으로 설정함 
    public override void Exit()
    {
        base.Exit();

        _skeletonController._lastTimeAttacked = Time.time;
    }

    // 매 프레임마다 실행하는 AttackState의 Update 함수에서는 이동을 막고
    // AnimationEvent가 발동되었다면, 다시 EngageState(=경계상태)로 돌아감
    public override void Update()
    {
        base.Update();

        _skeletonController.SetZeroVelocity();

        if (_triggerCalled)
            _enemyStateMachine.ChangeState(_skeletonController._engageState);
    }
}
