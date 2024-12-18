using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateEngage : EnemyState
{
    // 교전 상태를 의미하는 SkeletonStateEngage
    SkeletonController _skeletonController;
    Transform _player;
    float _moveDir = 1;

    public SkeletonStateEngage(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    // 처음 EngageState에 들어오면 Player를 탐색함, 마찬가지로 비효율적인 동작이므로 수정할 예정
    public override void Enter()
    {
        base.Enter();

        _player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // EngageState의 매 프레임에서는 다음과 같이 처리함
    public override void Update()
    {
        base.Update();

        // 만약 플레이어를 탐지했다면
        if (_skeletonController.DoDetectPlayer())
        {
            // _stateTimer를 _engageTimer으로 설정
            _stateTimer = _skeletonController._engageTime;

            // 만약, 탐지거리가 _attackDistance보다 작다면
            if (_skeletonController.DoDetectPlayer().distance < _skeletonController._attackDistance)
            {
                // 공격할 수 있는지를 판단하고 State를 AttackState로 변경
                if (DoDefineCanAttack())
                    _enemyStateMachine.ChangeState(_skeletonController._attackState);
            }
        }
        // 탐지하지 못했다면
        else
        {
            // _stateTimer가 0보다 작거나, 플레이어와 Skeleton의 거리가 7보다 크다면 EngageState를 풀고 IdleState로 전환
            if (_stateTimer < 0 || Vector2.Distance(_player.transform.position, _skeletonController.transform.position) > 7)
                _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }

        // 만약 플레이어가 Skeleton의 오른쪽에 위치해있다면, 오른쪽으로
        if (_player.position.x > _skeletonController.transform.position.x)
            _moveDir = 1;
        // 만약 플레이어가 Skeleton의 왼쪽에 위치해있다면, 왼쪽으로
        else if (_player.position.x < _skeletonController.transform.position.x)
            _moveDir = -1;

        // Skeleton의 속력을 설정
        _skeletonController.SetVelocity(_skeletonController._moveSpeed * _moveDir, _rigidbody2D.velocity.y);
    }

    // 공격가능한지를 판단하는 함수
    private bool DoDefineCanAttack()
    {
        // 현재 시간이 마지막으로 공격한 시간인 _lastTimeAttacked에 공격 딜레이인 _attackCooldown을 합한 값보다 크다면
        if (Time.time >= _skeletonController._lastTimeAttacked + _skeletonController._attackCooldown)
        {
            // 마지막으로 공격한 시간을 현재 시간으로 설정하고 true를 리턴
            _skeletonController._lastTimeAttacked = Time.time;
            return true;
        }

        // 아니라면 false를 리턴
        return false;
    }
}
