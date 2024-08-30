using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // The player
    [SerializeField] private Vector3 _offset; // Offset between the camera and the player
    [SerializeField] private float _smoothSpeed;

    private void LateUpdate()
    {
        
    }
}
