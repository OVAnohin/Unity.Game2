using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    private PlayerControls _inputActions;
    private Rigidbody _playerRigidbody;
    private bool _isJumped = false;

    private void Awake()
    {
        _inputActions = new PlayerControls();
        _inputActions.Player.Jump.performed += OnJump;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void FixedUpdate()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        Vector3 movement = Vector3.right * _movementSpeed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + movement);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isJumped == false)
        {
            _playerRigidbody.velocity = Vector3.up * _jumpForce;
            _isJumped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumped = false;
    }
}
