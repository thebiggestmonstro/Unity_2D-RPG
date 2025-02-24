using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCounterAttack : PlayerState
{
    private bool _checkCloneCreated;

    public PlayerStateCounterAttack(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _checkCloneCreated = true;
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

        _controller.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller._attackCheck.position, _controller._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                if (hit.GetComponent<EnemyController>().DoDefineCanBeStunned())
                {
                    _stateTimer = 10.0f;
                    _controller._animator.SetBool("SuccesfulCounterAttack", true);

                    if (_checkCloneCreated)
                    {
                        _checkCloneCreated = false;
                        _controller._skillManager._skillCloning.CreateCloneOnCounterAttack(hit.transform);
                    }
                }
            }
        }

        if (_stateTimer < 0 || _triggerCalled)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
