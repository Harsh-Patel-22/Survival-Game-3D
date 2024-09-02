using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIMovementController : MonoBehaviour
{
    private Vector3 moveDirection;
    private float movementRadius = 3f;

    private Vector3 spawnPoint;

    private float walkTime;
    private float stopTime;
    private float walkTimeCounter = 0f;
    private float stopTimeCounter = 0f;

    private bool isWalking = true;

    private float moveSpeed = 5f;
    

    private Rigidbody botRigidBody;

    private void Start()
    {
        botRigidBody = GetComponent<Rigidbody>();
        botRigidBody.freezeRotation = true;

        spawnPoint = transform.position;

        walkTime = UnityEngine.Random.Range(0.1f, 0.5f);
        stopTime = UnityEngine.Random.Range(0f, 0.1f);
    }
    private void Update()
    {
        if (isWalking)
        {
            walkTimeCounter += Time.deltaTime;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            if((transform.position - spawnPoint).magnitude >= movementRadius)
            {
                moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
            }

            if(walkTimeCounter >= walkTime)
            {
                ResetWalkLogic();
            }
        }
        else
        {
            stopTimeCounter += Time.deltaTime;
            if(stopTimeCounter >= stopTime)
            {
                ResetStopLogic();
            }
        }
    }

    private void ResetWalkLogic()
    {
        isWalking = false;
        walkTimeCounter = 0;
    }

    private void ResetStopLogic()
    {
        isWalking = true;
        stopTimeCounter = 0;
        GenerateMovementDirection();
    }
    private void GenerateMovementDirection()
    {
        int dirEnum = UnityEngine.Random.Range(0, 7);

        switch (dirEnum) {
            case 0:
                moveDirection = (Vector3)transform.forward; break;
            case 1:
                moveDirection = (Vector3) (transform.forward + transform.right).normalized; break;
            case 2:
                moveDirection = (Vector3) (transform.right); break;
            case 3:
                moveDirection = (Vector3) (-transform.forward + transform.right).normalized; break;
            case 4:
                moveDirection = (Vector3) (-transform.forward); break;
            case 5:
                moveDirection = (Vector3) (-transform.forward - transform.right); break;
            case 6:
                moveDirection = (Vector3) (-transform.right); break;
            case 7:
                moveDirection = (Vector3) (transform.forward - transform.right); break;
        }
    }
}
