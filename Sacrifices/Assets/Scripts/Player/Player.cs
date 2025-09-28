using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // 1. IMPORTANT: Add this namespace
using UnityEngine.SocialPlatforms.Impl;

public class Player : Entity
{
    private InputHandler inputHandler;
    private Animator animator;
    private GroundDetector groundDetector;

    public int[] banned = { };

    // 2. Change 'Score' to a property with a setter to update the UI
    private int scoreValue = 0;
    public int Score
    {
        get { return scoreValue; }
        set
        {
            scoreValue = value;
            // Call the method to update the UI whenever the score changes
            UpdateScoreUI();
        }
    }

    [Header("Assigns")]
    [SerializeField] private WeaponController weaponController;

    // 3. New: Reference to the TextMeshPro component
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText; // Use TextMeshProUGUI for UI elements

    public static Player Instance;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
        groundDetector = GetComponentInChildren<GroundDetector>();

        StartCoroutine(slowAnimation());

        InvokeRepeating("Ban", 30f, 30f);

        /*
        // 2. Instantiate the generated Input Actions class
        playerControls = new PlayerActions();

        /* ... (Input setup commented out) ... */
    }

    void Update()
    {
        /* ... (Hollow Knight-style Aim/Look Input commented out) ... */
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

    // 5. Method to update the text box
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            // Set the text property using an interpolated string
            scoreText.text = $"Score: {scoreValue}";
        }
        else
        {
            Debug.LogWarning("Score TextMeshPro component is not assigned in the Inspector.");
        }
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

    // ... (Other methods remain the same) ...
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
            float verticalInput = inputHandler.verticalAimInput;

            Vector2 aimDirection = new Vector2(currentFacingDirection, 0f);

            if (verticalInput > 0.5f)
            {
                aimDirection = new Vector2(currentFacingDirection, 1f);
            }
            else if (verticalInput < -0.5f)
            {
                aimDirection = new Vector2(currentFacingDirection, -1f);
            }

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

    private void Ban()
    {
        int rng = UnityEngine.Random.Range(0, 2 - banned.Length);
        banned.Append(rng);
        if (rng == 0)
        {
            Alert.Instance.Update("You cant use pistol anymore");
        }
        else if (rng == 1)
        {
            Alert.Instance.Update("You cant use sword anymore");
        }
        else
        {
            Alert.Instance.Update("You cant use minigun anymore");     
        }
    }
}