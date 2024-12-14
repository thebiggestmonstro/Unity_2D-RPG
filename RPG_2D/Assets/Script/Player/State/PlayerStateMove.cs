using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateGrounded
{
    public PlayerStateMove(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    // State�� ����
    public override void Enter()
    {
        base.Enter();
    }

    // State���� Ż��
    public override void Exit()
    {
        base.Exit();
    }

    // State���� �� �����Ӹ��� ����
    public override void Update()
    {
        base.Update();

        _controller.SetVelocity(_xInput * _controller._moveSpeed, _rigidbody2D.velocity.y);

        if (_xInput == 0 || _controller.DoDetectIsFacingWall())
            _stateMachine.ChangeState(_controller._idleState);
    }
}
