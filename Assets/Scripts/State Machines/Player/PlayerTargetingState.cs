using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int freeLookBlendTreeHash = Animator.StringToHash("Free Look Blend Tree");
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
        stateMachine.Animator.Play(freeLookBlendTreeHash);
        stateMachine.TargetLocker.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

}
