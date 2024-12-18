using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    // EnemyState�� ��� �⺻���� ���� Controller�� EnemyController����
    // ���� Ÿ�� �� Controller�� ���� �����̹Ƿ� ������ ���� ����������
    protected EnemyStateMachine _enemyStateMachine;
    protected EnemyController _enemyBaseController;
    protected Rigidbody2D _rigidbody2D;

    protected bool _triggerCalled;
    protected string _animatorBoolParamName;

    protected float _stateTimer;

    // �����ڿ����� EnemyController, EnemyStateMachine, AnimatorBoolParamName�� ������
    public EnemyState(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName)
    { 
        this._enemyBaseController = enemyBaseController;
        this._enemyStateMachine = enemyStateMachine;
        this._animatorBoolParamName = animatorBoolParamName;
    }

    // EnemyState�� Update������ State���� ����� �ð��� _stateTimer�� ������
    public virtual void Update()
    { 
        _stateTimer -= Time.deltaTime;
    }

    // Enter������ �ش� State�� ���Ƿ�,
    // State�� Ż���ϴ� �뵵�� _triggerCalled�� false�� ���� 
    // EnemyContorller�� RigidBody2D�� ������ 
    // State�� Animator���� Ȱ��ȭ�ϱ� ���� bool ������ true�� ���� 
    public virtual void Enter()
    { 
        _triggerCalled = false;
        _rigidbody2D = _enemyBaseController._rigidbody2D;
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, true);
    }

    // Exit�� ��� �ܼ��ϰ� State�� Animator���� ��Ȱ��ȭ�ϱ� ���� bool ������ false�� ����
    public virtual void Exit()
    {
        _enemyBaseController._animator.SetBool(_animatorBoolParamName, false); 
    }

    // AnimationEvent�� �ߵ��� �Լ�, State�� Ż���ϱ� ���� _triggerCalled�� true�� �ٲ�
    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
