using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    // 적 이동 정보
    [Header("Move Info")]
    public float _moveSpeed;
    public float _idleTime;
    public float _engageTime;

    // 적 공격 정보
    [Header("Attack Info")]
    public float _attackDistance;
    public float _attackCooldown;
    [HideInInspector]
    public float _lastTimeAttacked;

    // 적 스턴 정보
    [Header("Stunned Info")]
    public float _stunDuration;
    public Vector2 _stunDirection;
    protected bool _canBeStunned;
    [SerializeField]
    protected GameObject _counterImage; // 카운터 어택을 당할 수 있는지 조건을 판별하는 오브젝트, 빨간 사각형에 해당함

    public EnemyStateMachine _stateMachine { get; private set; }

    // 제일 초기에 적을 위한 EnemyStateMachine을 설정
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new EnemyStateMachine();
    }

    // EnemyController의 Update에서는 현재 State의 Update를 수행
    protected override void Update()
    {
        base.Update();
        _stateMachine._currentState.Update();
    }

    // 추가적으로 땅과 벽을 위한 Debug Line 말고도 공격 사거리를 그리는 Debug Line을 그림
    protected override void OnDrawGizmos()
    { 
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackDistance * _facingDir, transform.position.y));
    }

    // Player를 탐지하는 함수
    public virtual RaycastHit2D DoDetectPlayer() => Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDir, 50, LayerMask.GetMask("Player"));

    // 현재 State의 AnimationFinishTrigger 함수를 호출
    public void AnimationTrigger() => _stateMachine._currentState.AnimationFinishTrigger();

    // 카운터 어택을 당하기 위한 조건을 설정하는 함수
    public virtual void DoOpenCounterAttackWindow()
    {
        _canBeStunned = true;
        _counterImage.SetActive(true);
    }

    // 카운터 어택을 당하기 위한 조건을 회수하는 함수
    public virtual void DoCloseCounterAttackWindow()
    {
        _canBeStunned = false;
        _counterImage.SetActive(false); 
    }

    // 스턴 상태로 전환될 수 있는지 판단하는 함수
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
