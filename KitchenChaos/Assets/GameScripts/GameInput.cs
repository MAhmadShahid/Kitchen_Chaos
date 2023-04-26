using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;  
    private PlayerInputActions m_playerInputActions;

    private void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        m_playerInputActions.Player.Enable();

        m_playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //if (OnInteractAction != null)
        //    OnInteractAction(this, EventArgs.Empty);

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        // our input comprises of only two axis, thus the use of Vector2
        Vector2 inputVector = m_playerInputActions.Player.Move.ReadValue<Vector2>();

        // normalizing vector to have a constant maginitude (1 in this case) along all directions
        Vector2 inputVectorNormalized = inputVector.normalized;

        return inputVectorNormalized;
    }
}
