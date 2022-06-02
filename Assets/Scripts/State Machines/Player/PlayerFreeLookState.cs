using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int _freeLookBlendTreeHash = Animator.StringToHash("Free Look Blend Tree");
    private const float _animatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Animator.Play(_freeLookBlendTreeHash);
        stateMachine.InputReader.TargetEvent += EngageTarget;
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(_freeLookSpeedHash, 0, _animatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(_freeLookSpeedHash, 1, _animatorDampTime, deltaTime);
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
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

}
