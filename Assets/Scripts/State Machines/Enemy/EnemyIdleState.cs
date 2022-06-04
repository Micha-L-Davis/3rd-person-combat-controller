using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int _speedHash = Animator.StringToHash("Speed");
    private readonly int _idleBlendTreeHash = Animator.StringToHash("Locomotion Blend Tree");
    private float _animationDampTime = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_idleBlendTreeHash, animationCrossfadeTime);

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(_speedHash, 0f, _animationDampTime, deltaTime);
    }

    public override void Exit()
    {

    }
}
