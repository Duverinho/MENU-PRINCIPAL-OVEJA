using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;// Velocidad al caminar
    public float runSpeed = 5f; // Velocidad al correr
    public float rotationSpeed = 5f; // Velocidad de rotacion

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, bool running = false)
    {
        /*float*/ speed = running ? runSpeed : walkSpeed;
        direction = direction.normalized;

        // Obtener la dirección de la cámara sin afectar el eje Y
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular la dirección de movimiento basada en la cámara
        Vector3 moveDirection = (cameraForward * direction.z + cameraRight * direction.x) * speed;

        // Aplicar movimiento
        Vector3 velocityVector = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        rb.linearVelocity = velocityVector;
    }

    public void Rotate(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude < 0.01f) // Evita rotaciones innecesarias si no hay entrada
            return;

        // Convertir la entrada en un vector de dirección en el espacio del mundo
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        // Aplicar la rotación de la cámara para determinar la dirección de rotación
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Asegurar que el personaje solo rote en el plano XZ
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular la dirección final en función de la cámara
        Vector3 moveDirection = cameraForward * direction.z + cameraRight * direction.x;

        // Determinar la rotación objetivo
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Aplicar la rotación suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
