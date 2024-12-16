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

    // Player Move Info
    public float _moveSpeed = 12f;
    public float _jumpForce = 12f;
    public bool _isJumpPressed;
    public bool _isAttackClicked;
    public float _horizontalValue { get; private set; }
    public float _verticalValue { get; private set; }

    // Dash Info
    [SerializeField]
    private float _dashCooldown;
    private float _dashUsageTimer;
    public float _dashSpeed;
    public float _dashDuration;
    public float _dashDir { get; private set; }

    public bool _doingSomething { get; private set; }

    [Header("Attack Details")]
    public Vector2[] _attackMovement;

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
    }

    protected override void Start()
    {
        base.Start();

        _stateMachine.Init(_idleState);
    }

    protected override void Update()
    {
        base.Update();

        _stateMachine._currentState.Update();

        _dashUsageTimer -= Time.deltaTime;
    }

    public IEnumerator DoSomething(float _seconds)
    {
        _doingSomething = true;

        yield return new WaitForSeconds(_seconds);

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

        if (value.ReadValueAsButton() && _dashUsageTimer < 0)
        {
            _dashUsageTimer = _dashCooldown;
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

    public void AnimationTrigger() => _stateMachine._currentState.AnimationFinishTrigger();
}
