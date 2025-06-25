using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateStunnded : EnemyState
{
    SkeletonController _skeletonController;

    public SkeletonStateStunnded(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController)
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    public override void Enter()
    {
        base.Enter();

        // ���� ���¿� ������ ���, 0.1�� �������� ��� ����Ʈ �߻�
        _enemyBaseController._baseEffectController.InvokeRepeating("RedColorBlink", 0, 0.1f);

        _stateTimer = _skeletonController._stunDuration;

        // SetVelocity�� �ϴ� ���, ���� ���� ���¿��� �̵��ϰ�
        // �ǹǷ� ������ ���� ���ο� ���͸� ����� �̵��� ���ο� ���� ������ �����ϰԲ� ������
        _rigidbody2D.linearVelocity = new Vector2(_skeletonController._facingDir * _skeletonController._stunDirection.x, _skeletonController._stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        // ���� ���°� ������ ���, �ߵ����� Invoke �Լ��� ���� ��� �����
        _enemyBaseController._baseEffectController.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (_stateTimer < 0)
            _enemyStateMachine.ChangeState(_skeletonController._idleState);
    }
}
