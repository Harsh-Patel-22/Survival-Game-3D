using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;
    private AIMovementController movementController;

    private Vector3 differenceVector;
    private float scanDistance = 10f;

    private void Start()
    {
        movementController = GetComponent<AIMovementController>();
    }

    private void Update()
    {
        differenceVector = playerTransform.position - transform.position;
        if (differenceVector.magnitude <= scanDistance)
        {
            movementController.enabled = false;
            transform.forward = new Vector3 (differenceVector.x, 0f, differenceVector.z);
        }
        else
        {
            movementController.enabled = true;
        }
    }
}
