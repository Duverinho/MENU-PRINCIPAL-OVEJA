using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandleInput : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; } // Entrada de movimiento (eje X y Y)

    public bool IsMoving => MovementInput.magnitude > 0.1f; // // Devuelve true si el jugador se est� moviendo

    public Vector2 LookInput { get; private set; } // Entrada para la rotaci�n de la c�mara
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }

    private bool isSprinting;

    // M�todo que captura las entradas de movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    // M�todo que captura la entrada de rotaci�n (mirar)
    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    // El bot�n fue presionado
        //    isSprinting = context.ReadValueAsButton();
        //}
        //else if (context.canceled)
        //{
        //    // El bot�n fue soltado
        //    isSprinting = context.ReadValueAsButton();
        //}

        isSprinting = context.ReadValueAsButton();
    }
}
