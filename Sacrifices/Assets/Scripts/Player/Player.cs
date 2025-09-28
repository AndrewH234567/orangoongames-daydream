using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    // Configuration Fields
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 8f;

    public Vector2 movement;
    private bool isJumping = false;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Animator animator;


    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();

        StartCoroutine(slowAnimation());

        /*
        // 2. Instantiate the generated Input Actions class
        playerControls = new PlayerActions();

        // === Input Callbacks for Separate Actions ===

        // Horizontal Movement
        playerControls.Movement.Left.performed += ctx => { leftHeld = true; UpdateHorizontalInput(); };
        playerControls.Movement.Left.canceled += ctx => { leftHeld = false; UpdateHorizontalInput(); };

        playerControls.Movement.Right.performed += ctx => { rightHeld = true; UpdateHorizontalInput(); };
        playerControls.Movement.Right.canceled += ctx => { rightHeld = false; UpdateHorizontalInput(); };

        // Vertical Aiming
        playerControls.Movement.AimUp.performed += ctx => verticalAimInput = 1f;
        playerControls.Movement.AimUp.canceled += ctx => verticalAimInput = 0f;

        playerControls.Movement.AimDown.performed += ctx => verticalAimInput = -1f;
        playerControls.Movement.AimDown.canceled += ctx => verticalAimInput = 0f;
        */
    }

    void Update()
    {

        // === 2. Hollow Knight-style Aim/Look Input ===
        /*
        if (verticalAimInput > 0)
        {
            Debug.Log("Looking Up");
        }
        else if (verticalAimInput < 0)
        {
            Debug.Log("Looking Down");
        }
        */
        handleHorizontalMovement();

    }

    IEnumerator slowAnimation()
    {
        while (true)
        {
            animator.speed = 1;
            yield return new WaitForSeconds(0.155f);
            animator.speed = 0;
            yield return new WaitForSeconds(0.155f);
        }
    }
    
    private void handleHorizontalMovement()
    {
        movement = inputHandler.movement;
        rb.linearVelocityX = movement.x * moveSpeed;
    }
}