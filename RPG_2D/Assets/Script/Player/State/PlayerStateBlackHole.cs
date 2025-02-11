using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBlackHole : PlayerState
{
    private float _flyTime = 0.4f;
    private bool _skillUsed;
    private float _defaultGravity;

    public PlayerStateBlackHole(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName)
        : base(inController, inStateMachine, inParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _defaultGravity = _controller._rigidbody2D.gravityScale;

        _skillUsed = false;
        _stateTimer = _flyTime;
        _rigidbody2D.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        _controller._rigidbody2D.gravityScale = _defaultGravity;
        _controller.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if (_stateTimer > 0)
            _rigidbody2D.velocity = new Vector2(0, 15);

        if (_stateTimer < 0)
        {
            _rigidbody2D.velocity = new Vector2(0, -0.1f);

            if (_skillUsed == false)
            {
                if (_controller._skillManager._skillBlackHole.DoDefineCanUseSkill())
                {
                    _controller._skillManager._skillBlackHole.DoUseSkill();
                    _skillUsed = true;
                }
            }
        }

        if (_controller._skillManager._skillBlackHole.BlackHoleSkillCompleted())
            _stateMachine.ChangeState(_controller._inAirState);
    }
}
