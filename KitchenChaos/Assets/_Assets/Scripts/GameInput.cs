using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
        // onInteractAction?.Invoke(this, EventArgs.Empty); // ? - if not null 
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        InputVector = InputVector.normalized;
        return InputVector;
    }
}
