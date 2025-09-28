using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("DEBUG")]
    public Vector2 movement;
    public bool isUpPressed;
    public bool isFirePressed;
    public float deadzone = 0.2f;
    public float verticalAimInput;
    public PlayerActions playerActions;

    void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Controls.Move.performed += GetMovementInput; //Subscribing the events
        playerActions.Controls.Move.canceled += GetMovementInput;
        playerActions.Controls.Attack.performed += GetAttackInput;
        playerActions.Controls.Attack.canceled += GetAttackInput;
        playerActions.Controls.AimUp.performed += GetUpAttackDirection;
        playerActions.Controls.AimUp.canceled += GetUpAttackDirection;
        playerActions.Controls.AimDown.performed += GetDownAttackDirection;
        playerActions.Controls.AimDown.canceled += GetDownAttackDirection;
        playerActions.Controls.SwapWeapon0.performed += GetWeapon0;
        playerActions.Controls.SwapWeapon1.performed += GetWeapon1;

        /*
        inputActions.PlayerActionMap.Shoot.performed += GetShootInput;
        inputActions.PlayerActionMap.Shoot.canceled += GetShootInput;
        inputActions.PlayerActionMap.Pause.performed += _ => PauseGame();
        inputActions.UIActionMap.Start.performed += _ => StartGame();
        if (noMenu)
        {
            StartGame();
        }           
        */
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        playerActions.Controls.Enable();
    }

    private void OnDisable()
    {
        //inputActions.UIActionMap.Disable();
        playerActions.Controls.Disable();
    }

    private void GetMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y != 0)
        {
            isUpPressed = true;
        }
        else
        {
            isUpPressed = false;
        }
        movement = new Vector2(input.x, 0);
    }

    private void GetAttackInput(InputAction.CallbackContext context)
    {
        isFirePressed = context.ReadValue<float>() >= deadzone;
    }

    private void GetUpAttackDirection(InputAction.CallbackContext context)
    {
        verticalAimInput = context.ReadValue<float>() >= deadzone ? 1 : 0;
    }

    private void GetDownAttackDirection(InputAction.CallbackContext context)
    {
        verticalAimInput = context.ReadValue<float>() >= deadzone ? -1 : 0;
    }

    private void GetWeapon0(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 0;
    }

    private void GetWeapon1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 1;
    }
}
