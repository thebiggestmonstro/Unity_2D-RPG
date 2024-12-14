using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInAir : PlayerState
{
    public PlayerStateInAir(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_controller.DoDetectIsFacingWall())
            _stateMachine.ChangeState(_controller._wallSlideState);

        if (_controller.DoDetectIsGrounded())
            _stateMachine.ChangeState(_controller._idleState);

        if (_xInput != 0)
            _controller.SetVelocity(_controller._moveSpeed * 0.8f * _xInput, _rigidbody2D.velocity.y);
    }
}
