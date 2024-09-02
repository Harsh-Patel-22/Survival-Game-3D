using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private GameObject target;
    private Transform targetTransform;
    private Rigidbody rigidBody;

    private Vector3 moveDirection;
    private float forceMultiplier = 70f;
    private float maxSpeed = 10f;
    private float groundDrag = 10f;

    private float stoppingDistance = 3f;


    public event Action OnReachingToThePlayer;
    private bool isWalking = true;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        targetTransform = target.transform;
    }
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        rigidBody.drag = groundDrag;
    }

    private void Update()
    {
        GenerateNormalisedMovementDirection();
        if(isWalking)
        {
            Move();
            LimitSpeed();
        }
    }

    private void LimitSpeed()
    {
        if (rigidBody.velocity.magnitude >= maxSpeed)
        {
            Vector3 tempVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            rigidBody.velocity = tempVelocity.normalized * maxSpeed;
        }
    }
    private void Move()
    {
        transform.forward = moveDirection * Time.deltaTime;
        rigidBody.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
    }
    private void GenerateNormalisedMovementDirection()
    {
        Vector3 differenceVector = targetTransform.position - transform.position;
        if (differenceVector.magnitude > stoppingDistance) {
            isWalking = true;
            moveDirection = new Vector3(differenceVector.x, 0f, differenceVector.z).normalized;
        }
        else
        {
            isWalking = false;
            OnReachingToThePlayer?.Invoke();
            moveDirection = Vector3.zero;
        }
    }
}
