using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("DEBUG")]
    public Vector2 movement;
    public float deadzone = 0.2f;
    public PlayerActions playerActions;

    void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Movement.MoveHorizontal.performed += GetMovementInput; //Subscribing the events
        playerActions.Movement.MoveHorizontal.canceled += GetMovementInput;

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
        playerActions.Movement.Enable();
    }

    private void OnDisable()
    {
        //inputActions.UIActionMap.Disable();
        playerActions.Movement.Disable();
    }

    private void GetMovementInput(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
