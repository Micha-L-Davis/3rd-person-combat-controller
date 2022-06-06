using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _previousFrameTime;
    private Attack _attack;
    private bool _forceIsApplied;

    public EnemyAttackState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.WeaponDamage.SetAttack(_attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        stateMachine.AnimationEventListener.OnHit += TryApplyForce;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FacePlayer();

        float normalizedTime = GetNormalizedTime();
        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            if (IsInAttackRange())
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }

        _previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) return;
        if (normalizedTime < _attack.ComboAttackTime) return;

        stateMachine.SwitchState
        (
            new EnemyAttackState
            (
                stateMachine,
                _attack.ComboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if (_forceIsApplied) return;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _attack.Force);

        _forceIsApplied = true;
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
