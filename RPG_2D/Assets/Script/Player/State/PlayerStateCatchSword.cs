using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCatchSword : PlayerState
{
    private Transform _sword;

    public PlayerStateCatchSword(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _sword = _controller._sword.transform;

        // 플레이어가 회수되는 단검보다 오른쪽에 위치하면서 오른쪽을 보고 있는 경우
        if (_controller.transform.position.x > _sword.position.x && _controller._facingDir == 1)
            _controller.Flip();
        // 플레이어가 회수되는 단검보다 왼쪽에 위치하면서 왼족을 보고 있는 경우
        else if (_controller.transform.position.x < _sword.position.x && _controller._facingDir == -1)
            _controller.Flip();

        _rigidbody2D.velocity = new Vector2(_controller._swordReturnImpact * -_controller._facingDir, _rigidbody2D.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        _controller.StartCoroutine("DoSomething", 0.1f);
    }

    public override void Update()
    {
        base.Update();

        if (_triggerCalled)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
