using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefenderBaseState : State
{
    protected DefenderStateMachine stateMachine;

    public DefenderBaseState(DefenderStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInAttackRange(EnemyStateMachine enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, stateMachine.transform.position);

        return distance <= stateMachine.AttackRange;
    }

}
