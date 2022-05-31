using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
