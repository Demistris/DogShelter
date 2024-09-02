using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpObjects : MonoBehaviour
{
    [SerializeField] private Transform _pickupParent; // The position where the picked-up object will be held

    private GameObject _currentObject;
    private GameObject _currentObjectParent;
    private List<GameObject> _pickupableObjectsInRange = new List<GameObject>();
    private bool _isCarrying = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(_isCarrying)
            {
                DropObject();
            }
            else if(_pickupableObjectsInRange.Count > 0)
            {
                PickupObject(_pickupableObjectsInRange[0]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) return;

        if (other.CompareTag("Pickupable"))
        {
            if (!_pickupableObjectsInRange.Contains(other.gameObject))
            {
                if(other.gameObject.GetComponent<Rigidbody>() != null)
                {
                    _pickupableObjectsInRange.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameObject) return;

        if (_pickupableObjectsInRange.Contains(other.gameObject))
        {
            _pickupableObjectsInRange.Remove(other.gameObject);
        }
    }

    private void PickupObject(GameObject obj)
    {
        if(obj.transform.parent != null)
        {
            _currentObjectParent = obj.transform.parent.gameObject;
        }

        obj.transform.SetParent(_pickupParent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        
        if (rigidbody != null) 
        {
            rigidbody.isKinematic = true;
        }

        _currentObject = obj;
        _isCarrying = true;
    }

    private void DropObject()
    {
        if (_currentObject != null)
        {
            if(_currentObjectParent != null)
            {
                _currentObject.transform.SetParent(_currentObjectParent.transform);
            }
            else
            {
                _currentObject.transform.SetParent(null);
            }

            Rigidbody rigidbody = _currentObject.GetComponent<Rigidbody>();

            if(rigidbody != null)
            {
                rigidbody.isKinematic = false;
            }

            _currentObjectParent = null;
            _currentObject = null;
            _isCarrying = false;
        }
    }
}
