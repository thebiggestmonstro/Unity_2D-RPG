using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState _currentState { get; private set; }

    // EnemyStateMachine�� ���� ���� State�� �����ϰ� State�� Enter
    public void Init(EnemyState StartState)
    { 
        _currentState = StartState;
        _currentState.Enter();
    }

    // EnemtStateMachine�� ���� ���� State�� Ż���ϰ� ���� State�� ���� ����, ���� �� Enter
    public void ChangeState(EnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
