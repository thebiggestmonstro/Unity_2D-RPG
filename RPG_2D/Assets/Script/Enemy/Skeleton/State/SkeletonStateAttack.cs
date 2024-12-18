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

    // AttakState�� Ż���ϸ鼭 ���������� ������ �ð��� _lastTimeAttacked�� ���� �ð����� ������ 
    public override void Exit()
    {
        base.Exit();

        _skeletonController._lastTimeAttacked = Time.time;
    }

    // �� �����Ӹ��� �����ϴ� AttackState�� Update �Լ������� �̵��� ����
    // AnimationEvent�� �ߵ��Ǿ��ٸ�, �ٽ� EngageState(=������)�� ���ư�
    public override void Update()
    {
        base.Update();

        _skeletonController.SetZeroVelocity();

        if (_triggerCalled)
            _enemyStateMachine.ChangeState(_skeletonController._engageState);
    }
}
