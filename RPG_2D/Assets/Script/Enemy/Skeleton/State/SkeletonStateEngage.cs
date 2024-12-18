using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateEngage : EnemyState
{
    // ���� ���¸� �ǹ��ϴ� SkeletonStateEngage
    SkeletonController _skeletonController;
    Transform _player;
    float _moveDir = 1;

    public SkeletonStateEngage(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    // ó�� EngageState�� ������ Player�� Ž����, ���������� ��ȿ������ �����̹Ƿ� ������ ����
    public override void Enter()
    {
        base.Enter();

        _player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // EngageState�� �� �����ӿ����� ������ ���� ó����
    public override void Update()
    {
        base.Update();

        // ���� �÷��̾ Ž���ߴٸ�
        if (_skeletonController.DoDetectPlayer())
        {
            // _stateTimer�� _engageTimer���� ����
            _stateTimer = _skeletonController._engageTime;

            // ����, Ž���Ÿ��� _attackDistance���� �۴ٸ�
            if (_skeletonController.DoDetectPlayer().distance < _skeletonController._attackDistance)
            {
                // ������ �� �ִ����� �Ǵ��ϰ� State�� AttackState�� ����
                if (DoDefineCanAttack())
                    _enemyStateMachine.ChangeState(_skeletonController._attackState);
            }
        }
        // Ž������ ���ߴٸ�
        else
        {
            // _stateTimer�� 0���� �۰ų�, �÷��̾�� Skeleton�� �Ÿ��� 7���� ũ�ٸ� EngageState�� Ǯ�� IdleState�� ��ȯ
            if (_stateTimer < 0 || Vector2.Distance(_player.transform.position, _skeletonController.transform.position) > 7)
                _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }

        // ���� �÷��̾ Skeleton�� �����ʿ� ��ġ���ִٸ�, ����������
        if (_player.position.x > _skeletonController.transform.position.x)
            _moveDir = 1;
        // ���� �÷��̾ Skeleton�� ���ʿ� ��ġ���ִٸ�, ��������
        else if (_player.position.x < _skeletonController.transform.position.x)
            _moveDir = -1;

        // Skeleton�� �ӷ��� ����
        _skeletonController.SetVelocity(_skeletonController._moveSpeed * _moveDir, _rigidbody2D.velocity.y);
    }

    // ���ݰ��������� �Ǵ��ϴ� �Լ�
    private bool DoDefineCanAttack()
    {
        // ���� �ð��� ���������� ������ �ð��� _lastTimeAttacked�� ���� �������� _attackCooldown�� ���� ������ ũ�ٸ�
        if (Time.time >= _skeletonController._lastTimeAttacked + _skeletonController._attackCooldown)
        {
            // ���������� ������ �ð��� ���� �ð����� �����ϰ� true�� ����
            _skeletonController._lastTimeAttacked = Time.time;
            return true;
        }

        // �ƴ϶�� false�� ����
        return false;
    }
}
