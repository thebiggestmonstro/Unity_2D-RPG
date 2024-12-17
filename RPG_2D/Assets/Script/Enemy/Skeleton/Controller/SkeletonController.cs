using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    // Skeleton States
    public SkeletonStateIdle _idleState { get; private set; }
    public SkeletonStateMove _moveState { get; private set; }
    public SkeletonStateEngage _engageState { get; private set; }
    public SkeletonStateAttack _attackState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        _idleState = new SkeletonStateIdle(this, _stateMachine, "Idle", this);
        _moveState = new SkeletonStateMove(this, _stateMachine, "Move", this);
        _engageState = new SkeletonStateEngage(this, _stateMachine, "Move", this);
        _attackState = new SkeletonStateAttack(this, _stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        _stateMachine.Init(_idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

}
