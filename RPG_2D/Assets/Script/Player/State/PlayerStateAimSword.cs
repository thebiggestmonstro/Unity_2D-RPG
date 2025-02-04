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

        // 플레이어가 마우스 커서보다 오른쪽에 위치하면서 오른쪽을 보고 있는 경우
        if (_controller.transform.position.x > mousePosition.x && _controller._facingDir == 1)
            _controller.Flip();
        // 플레이어가 마우스 커서보다 왼쪽에 위치하면서 왼족을 보고 있는 경우
        else if(_controller.transform.position.x < mousePosition.x && _controller._facingDir == -1)
            _controller.Flip();
    }
}
