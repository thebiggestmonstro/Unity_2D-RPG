using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCounterAttack : PlayerState
{
    public PlayerStateCounterAttack(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    // ī���� ������ �����ؾ߸� ī���� ���� ���� �ִϸ��̼��� ����ϹǷ� ������ ����
    // ī���� ���� ���·� ���� ���, �ش� �ִϸ����� ���ڸ� false�� ����
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

        // ī���� ���õ��� �̵����� ���ϵ��� ����
        _controller.SetZeroVelocity();

        // ī���� ������ �ϴ� ���� �� ���·� �������� ������ ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller._attackCheck.position, _controller._attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                // ����� ������ ������ �� �ִ� ���¶��,
                if (hit.GetComponent<EnemyController>().DoDefineCanBeStunned())
                {
                    // _stateTimer�� 10�ʷ� �����Ͽ� ī���� ���� ���¸� �����ϰ�
                    // ī���� ������ �����Ͽ����Ƿ� ī���� ���� ���� �ִϸ��̼��� ���
                    _stateTimer = 10.0f;
                    _controller._animator.SetBool("SuccesfulCounterAttack", true);
                }
            }
        }

        // _stateTimer�� 0���� �۰ų�, ī���� ���� �ִϸ��̼��� ������ �����ӿ� ������
        // Animation Event�� ȣ��Ǿ��ٸ�, ���¸� �ٽ� Idle�� �ٲ�
        if (_stateTimer < 0 || _triggerCalled)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
