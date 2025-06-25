using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateGrounded
{
    public PlayerStateMove(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        AudioManager._audioManagerInstance.PlaySFX(14, null);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager._audioManagerInstance.StopSFX(14);
    }

    public override void Update()
    {
        base.Update();

        _controller.SetVelocity(_xInput * _controller._moveSpeed, _rigidbody2D.linearVelocity.y);

        if (_xInput == 0 || _controller.DoDetectIsFacingWall())
            _stateMachine.ChangeState(_controller._idleState);
    }
}
