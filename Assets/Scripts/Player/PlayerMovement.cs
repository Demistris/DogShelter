using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;

    [Header("Ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    private bool _isGrounded;

    [Header("Camera")]
    [SerializeField] Transform _cameraTransform;

    [Header("Model")]
    [SerializeField] private Transform _modelTransform;

    private void Start()
    {
        _cameraTransform.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Check if grounded
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

        // Move the player
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        controller.Move(moveDirection * _speed * Time.deltaTime);

        // Rotate the model to face the movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, targetRotation, 0.1f);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
