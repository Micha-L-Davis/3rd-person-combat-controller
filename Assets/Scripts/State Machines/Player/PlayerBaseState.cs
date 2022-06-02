using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion * deltaTime) + (stateMachine.ForceReceiver.Movement));
    }

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
