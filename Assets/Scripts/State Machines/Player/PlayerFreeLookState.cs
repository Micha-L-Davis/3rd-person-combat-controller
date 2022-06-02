using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int fargetingBlendTreeHash = Animator.StringToHash("Targeting Blend Tree");
    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += EngageTarget;
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(freeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= EngageTarget;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (forward * stateMachine.InputReader.MovementValue.y) + (right * stateMachine.InputReader.MovementValue.x);
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationSmoothing
            );

    }

    private void EngageTarget()
    {
        if (stateMachine.TargetLocker.SelectTarget())
        {
            stateMachine.Animator.Play(fargetingBlendTreeHash);
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

}
