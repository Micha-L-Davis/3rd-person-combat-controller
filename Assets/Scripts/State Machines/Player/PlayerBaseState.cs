using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    protected float animationCrossfadeTime = 0.1f;

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        Target target = stateMachine.TargetLocker.CurrentTarget;
        if ( target == null) return;
        Vector3 currentPosition = stateMachine.transform.position;
        Vector3 facingVector = target.transform.position - currentPosition;
        facingVector.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
