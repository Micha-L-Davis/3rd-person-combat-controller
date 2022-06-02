using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Attack _attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}

