using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateEngage : EnemyState
{
    SkeletonController _skeletonController;
    Transform _player;
    float _moveDir = 1;

    public SkeletonStateEngage(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController) 
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    public override void Enter()
    {
        base.Enter();

        _player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_skeletonController.DoDetectPlayer())
        {
            _stateTimer = _skeletonController._battleTime;

            if (_skeletonController.DoDetectPlayer().distance < _skeletonController._attackDistance)
            {
                if (DoDefineCanAttack())
                    _enemyStateMachine.ChangeState(_skeletonController._attackState);
            }
        }
        else
        {
            if (_stateTimer < 0 || Vector2.Distance(_player.transform.position, _skeletonController.transform.position) > 7)
                _enemyStateMachine.ChangeState(_skeletonController._idleState);
        }

        if (_player.position.x > _skeletonController.transform.position.x)
            _moveDir = 1;
        else if(_player.position.x < _skeletonController.transform.position.x)
            _moveDir = -1;

        _skeletonController.SetVelocity(_skeletonController._moveSpeed * _moveDir, _rigidbody2D.velocity.y);
    }

    private bool DoDefineCanAttack()
    {
        if (Time.time >= _skeletonController._lastTimeAttacked + _skeletonController._attackCooldown)
        {
            _skeletonController._lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
