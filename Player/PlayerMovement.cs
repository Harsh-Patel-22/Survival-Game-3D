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
    private Vector3 moveDirection;
    private float moveSpeed = 15f;

    private float groundDrag = 20f, playerHeight = 3f;
    private float airMultiplier = 0f;

    private float stamina = 100f;
    private float staminaDropRate = 5f;
    private float staminaRefillRate = 10f;

    private bool jumped;
    private bool isSprinting;
    private bool isGrounded;


    void Start()
    {
        rb.freezeRotation = true;
    }
    
    private void move()
    {
        if (isGrounded) 
        {
            rb.drag = groundDrag;
            if (jumped)
            {
                airMultiplier = 1.4f;
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
                Debug.Log("Jumped!");
            }
            else
            {
                airMultiplier = 1f;
            }
        }
        else {
            rb.drag = 0;   
        }
        float staminaMultiplier = GetStaminaMultiplier();

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        rb.AddForce(moveDirection.normalized * moveSpeed * staminaMultiplier * 50f * airMultiplier, ForceMode.Force);
        
        if (rb.velocity.magnitude >= moveSpeed * staminaMultiplier)
        {
            Vector3 temp = new Vector3(moveDirection.x, 0f, moveDirection.z);
            temp.Normalize();
            temp.y = rb.velocity.y;
            rb.velocity = temp * moveSpeed * staminaMultiplier;
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
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private float GetStaminaMultiplier()
    {
        float multiplier;
        if(isSprinting && stamina > 0)
        {
            multiplier = stamina;
            stamina -= staminaDropRate * Time.deltaTime;
        }
        else
        {
            multiplier = 1f;
            if(stamina < 100)
            {
                stamina += staminaRefillRate * Time.deltaTime;
            }
        }

        if(stamina <= 0) {
            multiplier = 1f;
        }
        Debug.Log(stamina);
        return multiplier;
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, 0.1f + playerHeight / 2)) isGrounded = true;
        else isGrounded = false;
    }
}
