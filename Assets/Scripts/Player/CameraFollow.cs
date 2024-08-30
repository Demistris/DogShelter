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
    [SerializeField] private float _mouseSensitivity = 100f;
    private float _pitch = 0f;  // Vertical rotation
    private float _yaw = 0f;    // Horizontal rotation

    void Start()
    {
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        CameraMovement();
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
        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);

        // Keep the camera at the correct position relative to the player
        transform.position = _target.position + transform.rotation * _offset;
    }
}
