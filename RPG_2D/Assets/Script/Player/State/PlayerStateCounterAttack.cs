using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCounterAttack : PlayerState
{
    public PlayerStateCounterAttack(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    // 카운터 어택이 성공해야만 카운터 어택 성공 애니메이션을 재생하므로 다음과 같이
    // 카운터 어택 상태로 들어가는 경우, 해당 애니메이터 인자를 false로 설정
    public override void Enter()
    {
        base.Enter();

        _stateTimer = _controller._counterAttackDuration;
        _controller._animator.SetBool("SuccesfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // 카운터 어택동안 이동하지 못하도록 막음
        _controller.SetZeroVelocity();

        // 카운터 어택을 하는 동안 원 형태로 오버랩된 대상들을 저장
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller._attackCheck.position, _controller._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                // 저장된 대상들이 스턴할 수 있는 상태라면,
                if (hit.GetComponent<EnemyController>().DoDefineCanBeStunned())
                {
                    // _stateTimer를 10초로 설정하여 카운터 어택 상태를 유지하고
                    // 카운터 어택이 성공하였으므로 카운터 어택 성공 애니메이션을 재생
                    _stateTimer = 10.0f;
                    _controller._animator.SetBool("SuccesfulCounterAttack", true);
                }
            }
        }

        // _stateTimer가 0보다 작거나, 카운터 어택 애니메이션의 마지막 프레임에 설정된
        // Animation Event가 호출되었다면, 상태를 다시 Idle로 바꿈
        if (_stateTimer < 0 || _triggerCalled)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
