using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
        Debug.Log("cerraste el juego");
    }
}
