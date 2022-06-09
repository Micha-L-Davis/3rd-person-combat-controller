using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : ActorStateMachine
{
    [field: SerializeField] 
    public InputReader InputReader { get; private set; }
    [field: SerializeField]
    public TargetLocker TargetLocker { get; private set; }
    [field: SerializeField]
    public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField]
    public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField]
    public float RotationSmoothing { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

}
