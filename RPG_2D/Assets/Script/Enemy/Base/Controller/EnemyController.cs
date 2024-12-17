using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    [Header("Move Info")]
    public float _moveSpeed;
    public float _idleTime;
    public float _battleTime;

    [Header("Attack Info")]
    public float _attackDistance;
    public float _attackCooldown;
    [HideInInspector]
    public float _lastTimeAttacked;

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

    protected override void OnDrawGizmos()
    { 
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackDistance * _facingDir, transform.position.y));
    }

    public virtual RaycastHit2D DoDetectPlayer() => Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDir, 50, LayerMask.GetMask("Player"));

    public void AnimationTrigger() => _stateMachine._currentState.AnimationFinishTrigger();
}
