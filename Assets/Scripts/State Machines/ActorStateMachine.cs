using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateMachine : StateMachine
{
    [field: SerializeField]
    public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField]
    public CharacterController Controller { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public AnimationEventListener AnimationEventListener { get; private set; }
    [field: SerializeField]
    public Attack[] Attacks { get; private set; }
    [field: SerializeField]
    public WeaponDamage WeaponDamage { get; private set; }
}
