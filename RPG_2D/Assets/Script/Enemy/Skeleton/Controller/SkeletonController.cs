using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    public SkeletonStateIdle _idleState { get; private set; }
    public SkeletonStateMove _moveState { get; private set; }
    public SkeletonStateEngage _engageState { get; private set; }
    public SkeletonStateAttack _attackState { get; private set; }
    public SkeletonStateStunnded _stunnedState { get; private set; }
    public SkeletonStateDead _deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _idleState = new SkeletonStateIdle(this, _stateMachine, "Idle", this);
        _moveState = new SkeletonStateMove(this, _stateMachine, "Move", this);
        _engageState = new SkeletonStateEngage(this, _stateMachine, "Engage", this);
        _attackState = new SkeletonStateAttack(this, _stateMachine, "Attack", this);
        _stunnedState = new SkeletonStateStunnded(this, _stateMachine, "Stunned", this);
        _deadState = new SkeletonStateDead(this, _stateMachine, "Idle", this);
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

    public override bool DoDefineCanBeStunned()
    {
        if (base.DoDefineCanBeStunned())
        {
            _stateMachine.ChangeState(_stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();

        _stateMachine.ChangeState(_deadState);
    }
}
