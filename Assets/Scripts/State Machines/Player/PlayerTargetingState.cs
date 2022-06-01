using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += DisengageTarget;
    }


    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= DisengageTarget;
    }

    private void DisengageTarget()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

}
