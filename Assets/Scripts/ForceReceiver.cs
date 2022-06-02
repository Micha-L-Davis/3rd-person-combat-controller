using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float _verticalVelocity;

    [SerializeField] private CharacterController _controller;

    public Vector3 Movement => Vector3.up * _verticalVelocity;

    private void Update()
    {
        if (_verticalVelocity < 0f && _controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
