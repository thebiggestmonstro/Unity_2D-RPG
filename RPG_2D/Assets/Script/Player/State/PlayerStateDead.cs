using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : PlayerState
{
    public PlayerStateDead(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName)
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

        _controller.SetZeroVelocity();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
