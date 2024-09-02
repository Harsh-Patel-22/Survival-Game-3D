using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform orientation;

    private float horizontalMovement, verticalMovement, jumpForce = 550f;
    private bool jumped;
    private Vector3 moveDirection;
    private float moveSpeed = 15f;

    private bool isGrounded;
    private float groundDrag = 20f, playerHeight = 3f;

    private float airMultiplier = 0f;

    void Start()
    {
        rb.freezeRotation = true;
    }
    
    private void move()
    {

        if (isGrounded && jumped) 
        {
            airMultiplier = 1.4f;
            rb.drag = groundDrag;
            rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            Debug.Log("Jumped!");
        }
        else {
            rb.drag = 0;
            airMultiplier = 1;    
        }
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        rb.AddForce(moveDirection.normalized * moveSpeed * 50f * airMultiplier, ForceMode.Force);
        
        if (rb.velocity.magnitude >= moveSpeed)
        {
            Vector3 temp = new Vector3(moveDirection.x, 0f, moveDirection.z);
            temp.Normalize();
            temp.y = rb.velocity.y;
            rb.velocity = temp * moveSpeed;
        }
        
    }
    void Update()
    {
        transform.rotation = orientation.rotation;
        GetInput();
        CheckGround();
    }

    private void FixedUpdate()
    {
        move();
    }

    void GetInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        jumped = Input.GetKeyDown(KeyCode.Space);
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, 0.1f + playerHeight / 2)) isGrounded = true;
        else isGrounded = false;
    }
}
