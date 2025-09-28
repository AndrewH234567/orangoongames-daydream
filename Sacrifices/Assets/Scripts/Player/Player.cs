using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Entity
{

    private InputHandler inputHandler;
    private Animator animator;

    private GroundDetector groundDetector;

    public int Score = 0;

    [Header("Assigns")]
    [SerializeField] private WeaponController weaponController;

    public static Player Instance;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
        groundDetector = GetComponentInChildren<GroundDetector>();

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
        handleJump();
        float horizontalMove = Mathf.Abs(rb.linearVelocity.x);
        if (horizontalMove > 0.01f)
        {
            currentFacingDirection = 1f;
        }
        else if (horizontalMove < -0.01f)
        {
            currentFacingDirection = -1f;
        }
        handleAttack();
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

    private void handleJump()
    {
        if (inputHandler.isUpPressed && groundDetector.isGrounded && !isJumping)
        {
            rb.linearVelocityY += jumpForce;
            isJumping = true;
        }
        if (!groundDetector.isGrounded)
        {
            isJumping = false;
        }
    }

    public void handleAttack()
    {
        if (inputHandler.isFirePressed)
        {
            float verticalInput = inputHandler.verticalAimInput; // (You need to add this property in InputHandler)

            Vector2 aimDirection = new Vector2(currentFacingDirection, 0f);

            if (verticalInput > 0.5f)
            {
                // Aim diagonally up
                aimDirection = new Vector2(currentFacingDirection, 1f);
            }
            else if (verticalInput < -0.5f)
            {
                aimDirection = new Vector2(currentFacingDirection, -1f);
            }

            // The key is to normalize it so the resulting vector has a length of 1
            aimDirection = aimDirection.normalized;

            if (weaponController != null)
            {
                weaponController.TryAttack(aimDirection);
            }
            else
            {
                Debug.LogWarning("Weapon Controller is null");
            }
        }
    }

    public void handleSwitch(int key)
    {
        if (key == 1)
            weaponController.weaponId = 0;
        else if (key == 2)
            weaponController.weaponId = 1;
    }

    public WeaponController GetWeaponController()
    {
        return weaponController;
    }
}