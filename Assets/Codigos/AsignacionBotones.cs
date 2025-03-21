using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem.Controls;

public class AsignacionBotones : MonoBehaviour
{
    public TextMeshProUGUI[] botonesTextos; // Textos en UI para mostrar los botones asignados
    private ButtonControl[] botonesAsignados; // Referencia a los botones asignados
    private bool esperandoAsignacion = false;
    private int botonActual = -1; // Índice del botón que se está reasignando

    void Start()
    {
        botonesAsignados = new ButtonControl[botonesTextos.Length];
        RestablecerBotones();
    }

    void Update()
    {
        if (esperandoAsignacion && Gamepad.current != null)
        {
            foreach (var control in Gamepad.current.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    AsignarBoton(button);
                    break;
                }
            }
        }
    }

    public void IniciarAsignacion(int indice)
    {
        botonActual = indice;
        esperandoAsignacion = true;
        Debug.Log("Presiona un botón para asignarlo...");
    }

    private void AsignarBoton(ButtonControl nuevoBoton)
    {
        if (botonActual >= 0 && botonActual < botonesAsignados.Length)
        {
            botonesAsignados[botonActual] = nuevoBoton;
            botonesTextos[botonActual].text = nuevoBoton.name;
            Debug.Log("Botón asignado: " + nuevoBoton.name);
        }
        esperandoAsignacion = false;
    }

    public void RestablecerBotones()
    {
        if (Gamepad.current != null)
        {
            botonesAsignados[0] = Gamepad.current.buttonSouth; // A
            botonesAsignados[1] = Gamepad.current.buttonEast;  // B
            botonesAsignados[2] = Gamepad.current.buttonWest;  // X
            botonesAsignados[3] = Gamepad.current.buttonNorth; // Y
            botonesAsignados[4] = Gamepad.current.leftShoulder;  // LB
            botonesAsignados[5] = Gamepad.current.rightShoulder; // RB
            botonesAsignados[6] = Gamepad.current.leftTrigger;   // LT
            botonesAsignados[7] = Gamepad.current.rightTrigger;  // RT
            botonesAsignados[8] = Gamepad.current.startButton;   // Start
            botonesAsignados[9] = Gamepad.current.selectButton;  // Select

            for (int i = 0; i < botonesAsignados.Length; i++)
            {
                botonesTextos[i].text = botonesAsignados[i].name;
            }
        }
    }
}
