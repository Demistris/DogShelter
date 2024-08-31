using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    [Header("Rotation")]
    [SerializeField] private float _mouseSensitivity = 250f;
    private float _pitch = 0f; // Vertical rotation
    private float _yaw = 0f; // Horizontal rotation

    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField] private float _minDistance = 2.5f; // Minimum distance between the camera and the player
    [SerializeField] private float _maxDistance = 10f; // Maximum distance between the camera and the player
    [SerializeField] private float _smoothSpeed = 5f;
    private float _currentDistance; // Current distance between the camera and the player
    private float _targetDistance; // Target distance after scrolling

    [Header("Collision")]
    [SerializeField] private float _collisionOffset = 0.2f; // Offset to prevent camera from clipping into walls
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private float _minHeightAboveTarget = 1f; // Minimum height above the target's feet

    void Start()
    {
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;

        _currentDistance = Vector3.Distance(transform.position, _target.position);
        _targetDistance = _currentDistance;

        // Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        CameraMovement();
        CameraZoom();
    }

    private void CameraMovement()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        // Adjust the yaw and pitch based on mouse movement
        _yaw += mouseX;
        _pitch -= mouseY;  // Inverting Y so it feels more natural
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);  // Limit vertical rotation

        // Update camera rotation
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);

        // Calculate the desired position
        Vector3 desiredPosition = _target.position + rotation * _offset;

        // Ensure the camera doesn't go below the minimum height
        desiredPosition.y = Mathf.Max(desiredPosition.y, _target.position.y + _minHeightAboveTarget);

        // Check for collision
        Vector3 directionToTarget = desiredPosition - _target.position;
        float distanceToTarget = directionToTarget.magnitude;

        RaycastHit hit;
        if (Physics.SphereCast(_target.position, 0.2f, directionToTarget, out hit, distanceToTarget, _collisionLayers))
        {
            // If there's a collision, move the camera to the hit point
            transform.position = hit.point + hit.normal * _collisionOffset;
        }
        else
        {
            // If no collision, move to the desired position
            transform.position = desiredPosition;
        }

        // Always look at the target
        transform.LookAt(_target);
    }

    private void CameraZoom()
    {
        // Get the scroll input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new distance based on scroll input
        _targetDistance -= scrollInput * _zoomSpeed;
        _targetDistance = Mathf.Clamp(_targetDistance, _minDistance, _maxDistance);

        // Smoothly move towards the target distance
        _currentDistance = Mathf.Lerp(_currentDistance, _targetDistance, Time.deltaTime * _smoothSpeed);

        // Update the camera's position to maintain the new distance
        Vector3 direction = (transform.position - _target.position).normalized;
        Vector3 desiredPosition = _target.position + direction * _currentDistance;

        // Ensure the camera doesn't go below the minimum height
        desiredPosition.y = Mathf.Max(desiredPosition.y, _target.position.y + _minHeightAboveTarget);

        // Check for collision when zooming
        RaycastHit hit;
        if (Physics.SphereCast(_target.position, 0.2f, direction, out hit, _currentDistance, _collisionLayers))
        {
            transform.position = hit.point + hit.normal * _collisionOffset;
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
