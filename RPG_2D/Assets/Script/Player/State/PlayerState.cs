using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    // Animtion StateMachine
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _controller;
    private string _animatorBoolParamName;
    
    // Player Input
    protected float _xInput;
    protected float _yInput;
    protected bool _isJumping;
    protected bool _isAttacking;
    protected bool _isCounterAttacking;
    protected bool _isThrowingSword;
    protected bool _isCastingBlackHole;
    protected Rigidbody2D _rigidbody2D;

    // Animation Event
    protected bool _triggerCalled;

    // State Timer
    protected float _stateTimer;

    public PlayerState(PlayerController inController, PlayerStateMachine inStateMachine, string inParamName)
    { 
        this._controller = inController;
        this._stateMachine = inStateMachine;
        this._animatorBoolParamName = inParamName;
    }

    // State�� ����
    public virtual void Enter()
    {
        _controller._animator.SetBool(_animatorBoolParamName, true);
        _rigidbody2D = _controller._rigidbody2D;
        _triggerCalled = false;
    }

    // State���� Ż��
    public virtual void Exit()
    {
        _controller._animator.SetBool(_animatorBoolParamName, false);
    }

    // State���� �� �����Ӹ��� ����
    public virtual void Update()
    {
        _stateTimer -= Time.deltaTime;

        _xInput = _controller._horizontalValue;
        _yInput = _controller._verticalValue;
        _isJumping = _controller._isJumpPressed;
        _isAttacking = _controller._isAttackClicked;
        _isCounterAttacking = _controller._isCounterAttackClicked;
        _isThrowingSword = _controller._isThrowSwordClicked;
        _isCastingBlackHole = _controller._isCastingBlackHole;
        _controller._animator.SetFloat("yVelocity", _rigidbody2D.linearVelocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
