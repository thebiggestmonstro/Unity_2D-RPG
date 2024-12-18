using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    // �� �̵� ����
    [Header("Move Info")]
    public float _moveSpeed;
    public float _idleTime;
    public float _engageTime;

    // �� ���� ����
    [Header("Attack Info")]
    public float _attackDistance;
    public float _attackCooldown;
    [HideInInspector]
    public float _lastTimeAttacked;

    public EnemyStateMachine _stateMachine { get; private set; }

    // ���� �ʱ⿡ ���� ���� EnemyStateMachine�� ����
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new EnemyStateMachine();
    }

    // EnemyController�� Update������ ���� State�� Update�� ����
    protected override void Update()
    {
        base.Update();
        _stateMachine._currentState.Update();
    }

    // �߰������� ���� ���� ���� Debug Line ���� ���� ��Ÿ��� �׸��� Debug Line�� �׸�
    protected override void OnDrawGizmos()
    { 
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackDistance * _facingDir, transform.position.y));
    }

    // Player�� Ž���ϴ� �Լ�
    public virtual RaycastHit2D DoDetectPlayer() => Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDir, 50, LayerMask.GetMask("Player"));

    // ���� State�� AnimationFinishTrigger �Լ��� ȣ��
    public void AnimationTrigger() => _stateMachine._currentState.AnimationFinishTrigger();
}
