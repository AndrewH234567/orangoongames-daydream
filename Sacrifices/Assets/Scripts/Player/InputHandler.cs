using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("DEBUG")]
    public Vector2 movement;
    public Boolean isUpPressed;
    public float deadzone = 0.2f;
    public PlayerActions playerActions;

    void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Controls.Move.performed += GetMovementInput; //Subscribing the events
        playerActions.Controls.Move.canceled += GetMovementInput;

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
}
