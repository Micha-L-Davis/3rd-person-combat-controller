using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float verticalVelocity;

    [SerializeField] private CharacterController controller;

    public Vector3 Movement => Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
