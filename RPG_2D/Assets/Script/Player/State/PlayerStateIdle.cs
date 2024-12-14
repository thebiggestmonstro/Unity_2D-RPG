using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerStateGrounded
{
    public PlayerStateIdle(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName) 
        : base(inController, inStateMachine, inParamName)
    {

    }

    // State�� ����
    public override void Enter()
    {
        base.Enter();

        _controller.SetZeroVelocity();
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

        if (_xInput == _controller._facingDir && _controller.DoDetectIsFacingWall())
            return;

        if (_xInput != 0 && !_controller._doingSomething)
            _stateMachine.ChangeState(_controller._moveState);
    }
}
