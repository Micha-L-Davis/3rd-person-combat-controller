using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderStateMachine : StateMachine
{
    [field: SerializeField]
    public float AttackRange { get; private set; }
    [field: SerializeField]
    public Attack[] Attacks { get; private set; }
    [field: SerializeField]
    public WeaponDamage WeaponDamage { get; private set; }

}
