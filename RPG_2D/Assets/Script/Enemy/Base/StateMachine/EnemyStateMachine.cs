using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState _currentState { get; private set; }

    public void Initialize(EnemyState StartState)
    { 
        _currentState = StartState;
        _currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
