using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;

    private void Update()
    {
        _currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }
}
