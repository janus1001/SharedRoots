using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 360;

    private Vector3 _input;
    private bool _isMoving;

    private void Update()
    {
        GatherInput();
        Look();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        _rb.velocity = _input.ToIso() * _speed * Time.deltaTime;
    }

    private void Animate()
    {
        if (_input.magnitude < 0.1)
        {
            _isMoving = false;
            _audioSource.mute = true;
        }
        else
        {
            _isMoving = true;
            _audioSource.mute = false; 
        }

        _animator.SetBool("isMoving", _isMoving);
    }
}
