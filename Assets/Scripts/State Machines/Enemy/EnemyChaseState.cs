using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(locomotionBlendTreeHash, animationCrossfadeTime);
    }

    public override void Tick(float deltaTime)
    {
        FacePlayer();
        MoveToPlayer(deltaTime);

        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(speedHash, 1f, animationDampTime, deltaTime);
    }


    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
    private void FacePlayer()
    {
        Vector3 currentPosition = stateMachine.transform.position;
        Vector3 facingVector = stateMachine.Player.transform.position - currentPosition;
        facingVector.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination = stateMachine.Player.transform.position;

        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
