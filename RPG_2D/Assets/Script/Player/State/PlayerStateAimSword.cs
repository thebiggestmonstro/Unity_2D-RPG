using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAimSword : PlayerState
{
    public PlayerStateAimSword(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _controller._skillManager._skillThrowingSword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _controller.StartCoroutine("DoSomething", 0.2f);
    }

    public override void Update()
    {
        base.Update();

        _controller.SetZeroVelocity();

        if (_isThrowingSword == false)
            _stateMachine.ChangeState(_controller._idleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �÷��̾ ���콺 Ŀ������ �����ʿ� ��ġ�ϸ鼭 �������� ���� �ִ� ���
        if (_controller.transform.position.x > mousePosition.x && _controller._facingDir == 1)
            _controller.Flip();
        // �÷��̾ ���콺 Ŀ������ ���ʿ� ��ġ�ϸ鼭 ������ ���� �ִ� ���
        else if(_controller.transform.position.x < mousePosition.x && _controller._facingDir == -1)
            _controller.Flip();
    }
}
