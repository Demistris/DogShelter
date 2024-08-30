using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    private bool _isGrounded;

    [Header("Camera")]
    [SerializeField] Transform _cameraTransform;

    [Header("Model")]
    [SerializeField] private Transform _modelTransform;

    private void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        // Check if grounded
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        // Input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on the camera's direction
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        // Ignore vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;
        _rigidBody.MovePosition(_rigidBody.position + moveDirection * _speed * Time.deltaTime);

        if(moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, targetRotation, 0.1f);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
