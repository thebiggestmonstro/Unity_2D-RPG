using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    // EnemyState의 경우 기본적인 적의 Controller인 EnemyController말고도
    // 적의 타입 별 Controller를 가질 예정이므로 다음과 같이 구분지었음
    protected EnemyStateMachine _enemyStateMachine;
    protected EnemyController _enemyBaseController;
    protected Rigidbody2D _rigidbody2D;

    protected bool _triggerCalled;
    protected string _animatorBoolParamName;

    protected float _stateTimer;

    // 생성자에서는 EnemyController, EnemyStateMachine, AnimatorBoolParamName을 설정함
    public EnemyState(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName)
    { 
        this._enemyBaseController = enemyBaseController;
        this._enemyStateMachine = enemyStateMachine;
        this._animatorBoolParamName = animatorBoolParamName;
    }

    // EnemyState의 Update에서는 State별로 사용할 시간인 _stateTimer를 조정함
    public virtual void Update()
    { 
        _stateTimer -= Time.deltaTime;
    }

    // Enter에서는 해당 State에 들어가므로,
    // State를 탈출하는 용도인 _triggerCalled를 false로 설정 
    // EnemyContorller의 RigidBody2D를 가져옴 
    // State를 Animator에서 활성화하기 위해 bool 변수를 true로 설정 
    public virtual void Enter()
    { 
        _triggerCalled = false;
        _rigidbody2D = _enemyBaseController._rigidbody2D;
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, true);
    }

    // Exit의 경우 단순하게 State를 Animator에서 비활성화하기 위해 bool 변수를 false로 설정
    public virtual void Exit()
    {
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, false); 
    }

    // AnimationEvent로 발동될 함수, State를 탈출하기 위해 _triggerCalled를 true로 바꿈
    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
