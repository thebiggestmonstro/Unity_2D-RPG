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

        // 스턴 상태에 돌입한 경우, 0.1초 간격으로 즉시 이펙트 발생
        _enemyBaseController._baseEffectController.InvokeRepeating("RedColorBlink", 0, 0.1f);

        _stateTimer = _skeletonController._stunDuration;

        // SetVelocity를 하는 경우, 적이 스턴 상태에서 이동하게
        // 되므로 다음과 같이 새로운 벡터를 만들어 이동을 새로운 벡터 쪽으로 수행하게끔 고정함
        _rigidbody2D.velocity = new Vector2(_skeletonController._facingDir * _skeletonController._stunDirection.x, _skeletonController._stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        // 스턴 상태가 끝나는 경우, 발동중인 Invoke 함수를 전부 즉시 취소함
        _enemyBaseController._baseEffectController.Invoke("CancelRedColorBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if (_stateTimer < 0)
            _enemyStateMachine.ChangeState(_skeletonController._idleState);
    }
}
