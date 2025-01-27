using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacterController
{
    // Animtion StateMachine
    public PlayerStateMachine _stateMachine { get; private set; }

    #region States
    public PlayerStateIdle _idleState { get; private set; }
    public PlayerStateMove _moveState { get; private set; }
    public PlayerStateJump _jumpState { get; private set; }
    public PlayerStateInAir _inAirState { get; private set; }
    public PlayerStateDash _dashState { get; private set; }
    public PlayerStateWallSlide _wallSlideState { get; private set; }
    public PlayerStateWallJump _wallJumpState { get; private set; }
    public PlayerStatePrimaryAttack _priamaryAttackState { get; private set; }
    public PlayerStateCounterAttack _counterAttackState { get; private set; }
    public PlayerStateAimSword _aimSwordState { get; private set; }
    public PlayerStateCatchSword _catchSwordState { get; private set; }
    #endregion

    // Player Input
    [SerializeField]
    InputAction _moveAction;
    [SerializeField]
    InputAction _jumpAction;
    [SerializeField]
    InputAction _dashAction;
    [SerializeField]
    InputAction _attackAction;
    [SerializeField]
    InputAction _counterAttackAction;
    [SerializeField]
    InputAction _throwSwordAction;

    // Player Move Info
    public float _moveSpeed = 12f;
    public float _jumpForce = 12f;
    public bool _isJumpPressed;
    public bool _isAttackClicked;
    public float _horizontalValue { get; private set; }
    public float _verticalValue { get; private set; }

    // Dash Info
    public float _dashSpeed;
    public float _dashDuration;
    public float _dashDir { get; private set; }

    // Attack Info
    [Header("Attack Details")]
    public Vector2[] _attackMovement;
    public float _counterAttackDuration;
    public bool _isCounterAttackClicked;

    // Skill Info
    public SkillManager _skillManager { get; private set; }
    public bool _isThrowSwordClicked;

    public bool _doingSomething { get; private set; }

    private void OnEnable()
    {
        _moveAction.performed += DoMove;
        _moveAction.canceled += DoStopMove;
        _moveAction.Enable();

        _jumpAction.performed += DoJump;
        _jumpAction.canceled += DoStopJump;
        _jumpAction.Enable();

        _dashAction.performed += DoDash;
        _dashAction.Enable();

        _attackAction.performed += DoAttack;
        _attackAction.canceled += DoStopAttack;
        _attackAction.Enable();

        _counterAttackAction.performed += DoCounterAttack;
        _counterAttackAction.canceled += DoStopCounterAttack;
        _counterAttackAction.Enable();

        _throwSwordAction.performed += DoThrowSword;
        _throwSwordAction.canceled += DoThrowSword;
        _throwSwordAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= DoMove;
        _moveAction.canceled -= DoStopMove;
        _moveAction.Disable();

        _jumpAction.performed -= DoJump;
        _jumpAction.canceled -= DoStopJump;
        _jumpAction.Disable();

        _dashAction.performed -= DoDash;
        _dashAction.Disable();

        _attackAction.performed -= DoAttack;
        _attackAction.canceled -= DoStopAttack;
        _attackAction.Disable();

        _counterAttackAction.performed -= DoCounterAttack;
        _counterAttackAction.canceled -= DoStopCounterAttack;
        _counterAttackAction.Disable();

        _throwSwordAction.performed -= DoThrowSword;
        _throwSwordAction.canceled -= DoThrowSword;
        _throwSwordAction.Disable();
    }

    protected override void Awake()
    {
        base.Awake();

        _stateMachine = new PlayerStateMachine();

        _idleState = new PlayerStateIdle(this, _stateMachine, "Idle");
        _moveState = new PlayerStateMove(this, _stateMachine, "Move");
        _jumpState = new PlayerStateJump(this, _stateMachine, "Jump");
        _inAirState = new PlayerStateInAir(this, _stateMachine, "Jump");
        _dashState = new PlayerStateDash(this, _stateMachine, "Dash");
        _wallSlideState = new PlayerStateWallSlide(this, _stateMachine, "WallSlide");
        _wallJumpState = new PlayerStateWallJump(this, _stateMachine, "WallJump");
        _priamaryAttackState = new PlayerStatePrimaryAttack(this, _stateMachine, "Attack");
        _counterAttackState = new PlayerStateCounterAttack(this, _stateMachine, "CounterAttack");
        _aimSwordState = new PlayerStateAimSword(this, _stateMachine, "AimSword");
        _catchSwordState = new PlayerStateCatchSword(this, _stateMachine, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();

        _skillManager = SkillManager._skillManagerInstance;

        _stateMachine.Init(_idleState);
    }

    protected override void Update()
    {
        base.Update();

        _stateMachine._currentState.Update();
    }

    public IEnumerator DoSomething(float seconds)
    {
        _doingSomething = true;

        yield return new WaitForSeconds(seconds);

        _doingSomething = false;
    }

    void DoMove(InputAction.CallbackContext value)
    {
        _horizontalValue = value.ReadValue<Vector2>().x;
        _verticalValue = value.ReadValue<Vector2>().y;
    }

    void DoStopMove(InputAction.CallbackContext value)
    {
        _horizontalValue = value.ReadValue<Vector2>().x;
        _verticalValue = value.ReadValue<Vector2>().y;
    }

    void DoJump(InputAction.CallbackContext value)
    {
        _isJumpPressed = value.ReadValueAsButton();
    }

    void DoStopJump(InputAction.CallbackContext value)
    { 
        _isJumpPressed = value.ReadValueAsButton();
    }

    void DoDash(InputAction.CallbackContext value)
    {
        if (DoDetectIsFacingWall())
            return;

        if (value.ReadValueAsButton() && SkillManager._skillManagerInstance._skillDash.DoDefineCanUseSkill())
        {
            _dashDir = _moveAction.ReadValue<Vector2>().x;

            if (_dashDir == 0)
                _dashDir = _facingDir;

            _stateMachine.ChangeState(_dashState);
        }
    }

    void DoAttack(InputAction.CallbackContext value)
    {
        _isAttackClicked = value.ReadValueAsButton(); 
    }

    void DoStopAttack(InputAction.CallbackContext value)
    {
        _isAttackClicked = value.ReadValueAsButton();
    }

    void DoCounterAttack(InputAction.CallbackContext value)
    {
        _isCounterAttackClicked = value.ReadValueAsButton();
    }

    void DoStopCounterAttack(InputAction.CallbackContext value)
    {
        _isCounterAttackClicked = value.ReadValueAsButton();
    }

    void DoThrowSword(InputAction.CallbackContext value)
    {
        _isThrowSwordClicked = value.ReadValueAsButton();   
    }

    void DoStopThrowSword(InputAction.CallbackContext value)
    {
        _isThrowSwordClicked = value.ReadValueAsButton();
    }

    public void AnimationTrigger() => _stateMachine._currentState.AnimationFinishTrigger();
}
