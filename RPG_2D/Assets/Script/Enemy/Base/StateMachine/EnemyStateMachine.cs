using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState _currentState { get; private set; }

    // EnemyStateMachine을 통해 현재 State를 설정하고 State에 Enter
    public void Init(EnemyState StartState)
    { 
        _currentState = StartState;
        _currentState.Enter();
    }

    // EnemtStateMachine을 통해 현재 State를 탈출하고 현재 State를 새로 설정, 설정 후 Enter
    public void ChangeState(EnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
