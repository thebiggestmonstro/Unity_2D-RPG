using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState _currentState { get; private set; }

    // ���� ó�� �ƹ��͵� �ƴ� ���¿��� Ư�� State�� �����ϴ� ��� ȣ��
    public void Init(PlayerState startState)
    { 
        _currentState = startState;
        _currentState.Enter();
    }

    // Ư�� State�� ���¿��� �ٸ� State�� �����ϴ� ��� ȣ��
    public void ChangeState(PlayerState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
