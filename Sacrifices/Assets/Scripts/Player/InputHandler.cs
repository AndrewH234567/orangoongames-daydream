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
        playerActions.Controls.SwapWeapon2.performed += GetWeapon2;
        playerActions.Controls.SwapWeapon3.performed += GetWeapon3;
        playerActions.Controls.SwapWeapon4.performed += GetWeapon4;
        playerActions.Controls.SwapWeapon5.performed += GetWeapon5;
        playerActions.Controls.SwapWeapon6.performed += GetWeapon6;
        playerActions.Controls.SwapWeapon7.performed += GetWeapon7;
        playerActions.Controls.SwapWeapon8.performed += GetWeapon8;


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
        if (context.ReadValue<float>() >= deadzone && Array.Exists(Player.Instance.banned, element => element == 0)) Player.Instance.GetWeaponController().weaponId = 0;
    }

    private void GetWeapon1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone && !Array.Exists(Player.Instance.banned, element => element == 1)) Player.Instance.GetWeaponController().weaponId = 1;
    }
    private void GetWeapon2(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone && !Array.Exists(Player.Instance.banned, element => element == 2)) Player.Instance.GetWeaponController().weaponId = 2;
    }
    private void GetWeapon3(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone && !Array.Exists(Player.Instance.banned, element => element == 3)) Player.Instance.GetWeaponController().weaponId = 3;
    }
    private void GetWeapon4(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 4;
    }
    private void GetWeapon5(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 5;
    }
    private void GetWeapon6(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 6;
    }
    private void GetWeapon7(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 7;
    }
    private void GetWeapon8(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= deadzone) Player.Instance.GetWeaponController().weaponId = 8;
    }
}
