using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    [Header("Move Info")]
    public float _moveSpeed;
    public float _idleTime;
    public float _engageTime;
    private float _defaultMoveSpeed;

    [Header("Attack Info")]
    public float _attackDistance;
    public float _attackCooldown;
    [HideInInspector]
    public float _lastTimeAttacked;

    [Header("Stunned Info")]
    public float _stunDuration;
    public Vector2 _stunDirection;
    protected bool _canBeStunned;
    [SerializeField]
    protected GameObject _counterImage; 

    public EnemyStateMachine _stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new EnemyStateMachine();
        _defaultMoveSpeed = _moveSpeed;
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

    public virtual void DoOpenCounterAttackWindow()
    {
        _canBeStunned = true;
        _counterImage.SetActive(true);
    }

    public virtual void DoCloseCounterAttackWindow()
    {
        _canBeStunned = false;
        _counterImage.SetActive(false); 
    }

    public virtual bool DoDefineCanBeStunned()
    {
        if (_canBeStunned)
        {
            DoCloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public virtual void DoFreezeEnemy(bool isTimerFrozen)
    {
        if (isTimerFrozen)
        {
            _moveSpeed = 0;
            _animator.speed = 0;
        }
        else
        {
            _moveSpeed = _defaultMoveSpeed;
            _animator.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeEnemyTimer(float seconds)
    {
        DoFreezeEnemy(true);

        yield return new WaitForSeconds(seconds);

        DoFreezeEnemy(false);
    }
}
