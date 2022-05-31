using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] 
    public InputReader InputReader { get; private set; }

    void Start()
    {
        SwitchState(new PlayerMoveState(this));
    }

}
