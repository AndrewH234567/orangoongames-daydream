using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardActivator : MonoBehaviour
{
    public InputAction activationAction = new InputAction(binding: "<Keyboard>/e");


    public GameObject mainInventory;
    public GameObject showMainButton;

    void Awake()
    {
        activationAction.performed += ctx => ToggleState();
    }

    void OnEnable()
    {
        activationAction.Enable();
    }

    void OnDisable()
    {
        activationAction.Disable();
    }


    private void ToggleState()
    {
        if (mainInventory == null || showMainButton == null)
        {
            Debug.LogError("MainInventory or ShowMainButton is not assigned!");
            return;
        }

        if (mainInventory.activeSelf)
        {

            mainInventory.SetActive(false); 
            showMainButton.SetActive(true);  
            Debug.Log("Inventory Hidden.");
        }
        else
        {
            mainInventory.SetActive(true); 
            showMainButton.SetActive(false);
            Debug.Log("Inventory Shown.");
        }
    }
}
