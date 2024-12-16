using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    [Header("Move Info")]
    public float _moveSpeed;
    public float _idleTime;

    public EnemyStateMachine _stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        _stateMachine._currentState.Update();
    }
}
