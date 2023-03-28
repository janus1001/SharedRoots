using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("PickupSettings")]
    [SerializeField] Transform _holdArea;
    private GameObject _heldObject;
    private Rigidbody _heldObjRB;
    [SerializeField] CapsuleCollider pickupCollider;

    [SerializeField] MeshFilter outlineObject;
    GameObject highlightedObject;
    [SerializeField] ParticleSystem pickupSFXParticle;

    private void Start()
    {
        if(pickupSFXParticle)
            pickupSFXParticle.Stop();
    }

    private void Update()
    {
        bool pickedUpThisFrame = false;
        var closestObject = FindClosestPickupableCollider();
        if (closestObject != null && !_heldObject)
        {
            if (closestObject.gameObject != highlightedObject)
            {
                outlineObject.mesh = closestObject.GetComponent<MeshFilter>().mesh;
                highlightedObject = closestObject.gameObject;
                outlineObject.transform.parent = highlightedObject.transform;
                outlineObject.transform.localPosition = Vector3.zero;
                outlineObject.transform.localRotation = Quaternion.identity;
                outlineObject.transform.localScale = Vector3.one;
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
            {
                if (_heldObject == null)
                {
                    PickUpObject(closestObject.gameObject);
                    pickedUpThisFrame = true;
                    highlightedObject = null;
                    outlineObject.mesh = null;
                }
            }
        }
        else
        {
            highlightedObject = null;
            outlineObject.mesh = null;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            if (_heldObject != null && !pickedUpThisFrame)
            {
                DropObject();
                highlightedObject = null;
                outlineObject.mesh = null;
            }
        }

        if (_heldObject != null)
        {
            MoveObject();
        }
    }

    Collider FindClosestPickupableCollider()
    {
        Debug.DrawLine(pickupCollider.transform.position + Vector3.down, pickupCollider.transform.position + Vector3.up);
        var colliders = Physics.OverlapCapsule(pickupCollider.transform.position + Vector3.down, pickupCollider.transform.position + Vector3.up, pickupCollider.radius);
        Vector3 targetPosition = pickupCollider.center; // the position you want to find the closest collider to

        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("PuzzlePiece"))
                continue;

            float distance = Vector3.Distance(collider.transform.position, targetPosition);
            if (distance < closestDistance)
            {
                closestCollider = collider;
                closestDistance = distance;
            }
        }

        return closestCollider;
    }

    RigidbodyConstraints previousConstraints;
    private void PickUpObject(GameObject pickedObject)
    {
        _heldObjRB = pickedObject.GetComponent<Rigidbody>();
        if (_heldObjRB)
        {
            _heldObjRB.useGravity = false;
            _heldObjRB.drag = 0;
            previousConstraints = _heldObjRB.constraints;
            _heldObjRB.constraints = RigidbodyConstraints.FreezeAll;

            _heldObjRB.transform.parent = _holdArea;
            _heldObject = pickedObject;
            _heldObject.transform.position = _holdArea.position;
            _heldObject.GetComponent<Collider>().enabled = false;

            _heldObject.transform.rotation = transform.rotation;

            var source = _heldObject.GetComponent<AudioSource>();
            if (source)
            {
                source.Play();

                // Play ParticleSystem if exists
                if (pickupSFXParticle != null)
                {
                    pickupSFXParticle.transform.parent = _heldObject.transform;
                    pickupSFXParticle.transform.position = _heldObject.transform.position;
                    pickupSFXParticle.Play();
                }
            }
        }
    }

    private void DropObject()
    {
        _heldObjRB.useGravity = true;
        _heldObjRB.drag = 0;
        _heldObjRB.constraints = previousConstraints;
        _heldObjRB.isKinematic = false;

        _heldObject.GetComponent<Collider>().enabled = true;
        _heldObjRB.transform.parent = null;
        _heldObject = null;


        // Stop ParticleSystem if exists
        if (pickupSFXParticle != null)
            pickupSFXParticle.Stop();
    }

    private void MoveObject()
    {
        if (Vector3.Distance(_heldObject.transform.position, _holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (_holdArea.position - _heldObject.transform.position);
        }
    }
}
