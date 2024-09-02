using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Can be null if there's only one door
    [SerializeField] private Transform _rightDoor;
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private float _openAngle = 90f; // How much the doors open in degrees
    [SerializeField] private float _doorSpeed = 2f; // How fast the doors open/close
    [SerializeField] private Vector3 _leftDoorAxis = Vector3.up;
    [SerializeField] private Vector3 _rightDoorAxis = Vector3.up;

    private Quaternion _rightDoorClosedRotation;
    private Quaternion _leftDoorClosedRotation;
    private bool _isOpening = false;

    private void Start()
    {
        if (_rightDoor != null)
        {
            // Save the original rotation
            _rightDoorClosedRotation = _rightDoor.rotation;
            // Calculate the open rotation
            //_rightDoorOpenRotation = _rightDoorClosedRotation * Quaternion.Euler(0f, -_openAngle, 0f);
        }

        if (_leftDoor != null)
        {
            _leftDoorClosedRotation = _leftDoor.rotation;
            //_leftDoorOpenRotation = _leftDoorClosedRotation * Quaternion.Euler(0f, _openAngle, 0f);
        }
    }

    private void Update()
    {
        if (_isOpening)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
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

    private void OpenDoor()
    {
        // Smoothly rotate the doors to the open position
        if (_rightDoor != null)
        {
            //_rightDoor.rotation = Quaternion.Slerp(_rightDoor.rotation, _rightDoorOpenRotation, Time.deltaTime * _doorSpeed);

            Quaternion targetRotation = _rightDoorClosedRotation * Quaternion.AngleAxis(_openAngle, _rightDoorAxis);
            _rightDoor.rotation = Quaternion.Lerp(_rightDoor.rotation, targetRotation, Time.deltaTime * _doorSpeed);
        }

        if (_leftDoor != null)
        {
            //_leftDoor.rotation = Quaternion.Slerp(_leftDoor.rotation, _leftDoorOpenRotation, Time.deltaTime * _doorSpeed);k

            Quaternion targetRotation = _leftDoorClosedRotation * Quaternion.AngleAxis(_openAngle, _leftDoorAxis);
            _leftDoor.rotation = Quaternion.Lerp(_leftDoor.rotation, targetRotation, Time.deltaTime * _doorSpeed);
        }
    }

    private void CloseDoor()
    {
        // Smoothly rotate the doors to the closed position
        if (_rightDoor != null)
        {
            //_rightDoor.rotation = Quaternion.Slerp(_rightDoor.rotation, _rightDoorClosedRotation, Time.deltaTime * _doorSpeed);
            _rightDoor.rotation = Quaternion.Lerp(_rightDoor.rotation, _rightDoorClosedRotation, Time.deltaTime * _doorSpeed);
        }

        if (_leftDoor != null)
        {
            //_leftDoor.rotation = Quaternion.Slerp(_leftDoor.rotation, _leftDoorClosedRotation, Time.deltaTime * _doorSpeed);
            _leftDoor.rotation = Quaternion.Lerp(_leftDoor.rotation, _leftDoorClosedRotation, Time.deltaTime * _doorSpeed);
        }
    }
}
