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
    }

    public override void Update()
    {
        base.Update();

        if (_isThrowingSword == false)
            _stateMachine.ChangeState(_controller._idleState);
    }
}
