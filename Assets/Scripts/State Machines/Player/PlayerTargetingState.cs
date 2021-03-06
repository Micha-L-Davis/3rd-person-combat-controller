using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int _targetingBlendTreeHash = Animator.StringToHash("Targeting Blend Tree");
    private readonly int _targetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int _targetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_targetingBlendTreeHash, animationCrossfadeTime);
        stateMachine.InputReader.TargetEvent += DisengageTarget;
    }


    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }
        if(stateMachine.TargetLocker.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= DisengageTarget;
    }

    private void DisengageTarget()
    {
        stateMachine.TargetLocker.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        Vector2 movement = stateMachine.InputReader.MovementValue;

        stateMachine.Animator.SetFloat(_targetingForwardSpeedHash, movement.x, 0.1f, deltaTime);
        stateMachine.Animator.SetFloat(_targetingRightSpeedHash, movement.y, 0.1f, deltaTime);
    }
}
