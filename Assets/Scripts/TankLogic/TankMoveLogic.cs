using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMoveLogic : MonoBehaviour
{
    public float speed = 1;
    public float turnSpeed = 180;

    private float movementInput;
    private float turnInput;
    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            _rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput = InputController.Instance.VerticalAxe;
        turnInput = InputController.Instance.HorizontalAxe;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _transform.forward * movementInput * speed * Time.deltaTime);

        int isForward = 1;
        if (movementInput < 0)
            isForward = -1;

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, isForward * turnInput * turnSpeed * Time.deltaTime, 0));
    }
}
