using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform _rightDoor;
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private float _openAngle = 90f; // How much the doors open in degrees
    [SerializeField] private float _doorSpeed = 2f; // How fast the doors open/close

    private Quaternion _rightDoorClosedRotation;
    private Quaternion _leftDoorClosedRotation;
    private Quaternion _rightDoorOpenRotation;
    private Quaternion _leftDoorOpenRotation;

    private bool _isOpening = false;

    private void Start()
    {
        // Save the original rotation
        _rightDoorClosedRotation = _rightDoor.rotation;
        _leftDoorClosedRotation = _leftDoor.rotation;

        // Calculate the open rotation
        _rightDoorOpenRotation = _rightDoorClosedRotation * Quaternion.Euler(0f, -_openAngle, 0f);
        _leftDoorOpenRotation = _leftDoorClosedRotation * Quaternion.Euler(0f, _openAngle, 0f);
    }

    private void Update()
    {
        if (_isOpening)
        {
            // Smoothly rotate the doors to the open position
            _rightDoor.rotation = Quaternion.Slerp(_rightDoor.rotation, _rightDoorOpenRotation, Time.deltaTime * _doorSpeed);
            _leftDoor.rotation = Quaternion.Slerp(_leftDoor.rotation, _leftDoorOpenRotation, Time.deltaTime * _doorSpeed);
        }
        else
        {
            // Smoothly rotate the doors to the closed position
            _rightDoor.rotation = Quaternion.Slerp(_rightDoor.rotation, _rightDoorClosedRotation, Time.deltaTime * _doorSpeed);
            _leftDoor.rotation = Quaternion.Slerp(_leftDoor.rotation, _leftDoorClosedRotation, Time.deltaTime * _doorSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isOpening = false;
        }
    }
}
