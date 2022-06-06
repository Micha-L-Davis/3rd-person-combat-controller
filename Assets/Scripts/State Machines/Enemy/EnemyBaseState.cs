using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    protected float animationCrossfadeTime = 0.1f;
    protected readonly int locomotionBlendTreeHash = Animator.StringToHash("Locomotion Blend Tree");
    protected readonly int speedHash = Animator.StringToHash("Speed");
    protected float animationDampTime = 0.1f;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected bool IsInChaseRange()
    {
        float distance = Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position);

        return distance <= stateMachine.ChaseRange;
    }

    protected bool IsInAttackRange()
    {
        float distance = Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position);

        return distance <= stateMachine.AttackRange;
    }

    protected void FacePlayer()
    {
        Vector3 currentPosition = stateMachine.transform.position;
        Vector3 facingVector = stateMachine.Player.transform.position - currentPosition;
        facingVector.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }
}
