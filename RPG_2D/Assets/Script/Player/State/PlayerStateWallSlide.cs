using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWallSlide : PlayerState
{
    public PlayerStateWallSlide(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
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

        if (_controller.DoDetectIsFacingWall() == false)
            return;

        if (_isJumping)
        {
            _stateMachine.ChangeState(_controller._wallJumpState);
            return;
        }

        if (_xInput != 0 && _controller._facingDir != _xInput)
            _stateMachine.ChangeState(_controller._idleState);

        if (_yInput < 0.0f)
            _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocity.y);
        else
            _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocity.y * 0.7f);

        if (_controller.DoDetectIsGrounded())
            _stateMachine.ChangeState(_controller._idleState);
    }
}
