using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandleInput : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; } // Entrada de movimiento (eje X y Y)

    public bool IsMoving => MovementInput.magnitude > 0.1f; // // Devuelve true si el jugador se está moviendo

    public Vector2 LookInput { get; private set; } // Entrada para la rotación de la cámara
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }

    private bool isSprinting;

    // Método que captura las entradas de movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    // Método que captura la entrada de rotación (mirar)
    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    // El botón fue presionado
        //    isSprinting = context.ReadValueAsButton();
        //}
        //else if (context.canceled)
        //{
        //    // El botón fue soltado
        //    isSprinting = context.ReadValueAsButton();
        //}

        isSprinting = context.ReadValueAsButton();
    }
}
