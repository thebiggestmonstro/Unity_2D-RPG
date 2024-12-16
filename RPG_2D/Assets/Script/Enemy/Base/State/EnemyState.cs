using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine _enemyStateMachine;
    protected EnemyController _enemyBaseController;

    protected bool _triggerCalled;
    protected string _animatorBoolParamName;

    protected float _stateTimer;

    public EnemyState(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName)
    { 
        this._enemyBaseController = enemyBaseController;
        this._enemyStateMachine = enemyStateMachine;
        this._animatorBoolParamName = animatorBoolParamName;
    }

    public virtual void Update()
    { 
        _stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    { 
        _triggerCalled = true;
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, true);
    }

    public virtual void Exit()
    {
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, false); 
    }
}
