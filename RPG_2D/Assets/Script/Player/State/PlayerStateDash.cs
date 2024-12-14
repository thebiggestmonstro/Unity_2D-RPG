using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDash : PlayerState
{
    public PlayerStateDash(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    // State�� ����
    public override void Enter()
    {
        base.Enter();

        _stateTimer = _controller._dashDuration;
    }

    // State���� Ż��
    public override void Exit()
    {
        base.Exit();

        _controller.SetVelocity(0, _rigidbody2D.velocity.y);
    }

    // State���� �� �����Ӹ��� ����
    public override void Update()
    {
        base.Update();

        if (!_controller.DoDetectIsGrounded() && _controller.DoDetectIsFacingWall())
            _stateMachine.ChangeState(_controller._wallSlideState);

        _controller.SetVelocity(_controller._dashSpeed * _controller._dashDir, 0);

        if (_stateTimer < 0.0f)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
