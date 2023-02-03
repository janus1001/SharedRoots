using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("PickupSettings")]
    [SerializeField] Transform _holdArea;
    [SerializeField] private float _pickupRange = 1.5f;
    private GameObject _heldObject;
    private Rigidbody _heldObjRB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_heldObject == null )
            {
                RaycastHit hit;
                if (Physics.Raycast(_holdArea.transform.position, transform.TransformDirection(Vector3.forward), out hit, _pickupRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (_heldObject != null)
        {
            MoveObject();
        }
    }

    private void PickUpObject(GameObject pickedObject)
    {
        if (pickedObject.GetComponent<Rigidbody>())
        {
            _heldObjRB = pickedObject.GetComponent<Rigidbody>();
            _heldObjRB.useGravity = false;
            _heldObjRB.drag = 10;
            _heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            _heldObjRB.transform.parent = _holdArea;
            _heldObject = pickedObject;
            _heldObject.transform.position = _holdArea.position;
        }
    }

    private void DropObject()
    {
        _heldObjRB.useGravity = true;
        _heldObjRB.drag = 10;
        _heldObjRB.constraints = RigidbodyConstraints.None;

        _heldObjRB.transform.parent = null;
        _heldObject = null;
    }

    private void MoveObject()
    {
        if (Vector3.Distance(_heldObject.transform.position, _holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (_holdArea.position - _heldObject.transform.position);
        }
    }

}
