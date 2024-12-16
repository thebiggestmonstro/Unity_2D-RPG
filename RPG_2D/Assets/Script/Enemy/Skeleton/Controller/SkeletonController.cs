using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    // Skeleton States
    public SkeletonStateIdle _idleState { get; private set; }
    public SkeletonStateMove _moveState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        _idleState = new SkeletonStateIdle(this, _stateMachine, "Idle", this);
        _moveState = new SkeletonStateMove(this, _stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        _stateMachine.Initialize(_idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
