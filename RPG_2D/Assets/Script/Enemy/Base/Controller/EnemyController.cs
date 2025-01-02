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

    // �� ���� ����
    [Header("Stunned Info")]
    public float _stunDuration;
    public Vector2 _stunDirection;
    protected bool _canBeStunned;
    [SerializeField]
    protected GameObject _counterImage; // ī���� ������ ���� �� �ִ��� ������ �Ǻ��ϴ� ������Ʈ, ���� �簢���� �ش���

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

    // ī���� ������ ���ϱ� ���� ������ �����ϴ� �Լ�
    public virtual void DoOpenCounterAttackWindow()
    {
        _canBeStunned = true;
        _counterImage.SetActive(true);
    }

    // ī���� ������ ���ϱ� ���� ������ ȸ���ϴ� �Լ�
    public virtual void DoCloseCounterAttackWindow()
    {
        _canBeStunned = false;
        _counterImage.SetActive(false); 
    }

    // ���� ���·� ��ȯ�� �� �ִ��� �Ǵ��ϴ� �Լ�
    public virtual bool DoDefineCanBeStunned()
    {
        if (_canBeStunned)
        {
            DoCloseCounterAttackWindow();
            return true;
        }

        return false;
    }
}
