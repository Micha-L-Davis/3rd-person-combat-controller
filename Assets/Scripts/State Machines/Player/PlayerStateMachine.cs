using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] 
    public InputReader InputReader { get; private set; }
    [field: SerializeField]
    public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField]
    public CharacterController Controller { get; private set; }
    [field: SerializeField]
    public TargetLocker TargetLocker { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField]
    public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField]
    public float RotationSmoothing { get; private set; }
    public Transform MainCameraTransform { get; private set; }
    private bool isTargeting;

    void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }


}
