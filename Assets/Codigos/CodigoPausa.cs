using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoPausa : MonoBehaviour
{
    [Header("Configuración de Menú")]
    public GameObject objetoMenuPausa;
    public GameObject MenuSalir;

    [Header("Estado del Juego")]
    public bool enPausa = false;

    // Almacena los sonidos pausados para reanudarlos correctamente
    private AudioSource[] fuentesDeAudio;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!enPausa)
            {
                ActivarPausa();
            }
            else
            {
                Reanudar();
            }
        }
    }

    void ActivarPausa()
    {
        // Activar menú de pausa
        objetoMenuPausa.SetActive(true);
        enPausa = true;

        // Pausar el juego
        Time.timeScale = 0;

        // Configurar cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pausar todos los sonidos
        PausarSonidos();
    }

    void PausarSonidos()
    {
        // Obtener todos los AudioSource activos (FindObjectsByType es el nuevo método en Unity 6)
        fuentesDeAudio = FindObjectsByType<AudioSource>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        // Pausar cada sonido
        foreach (AudioSource fuente in fuentesDeAudio)
        {
            if (fuente != null && fuente.isPlaying)
            {
                fuente.Pause();
            }
        }
    }

    public void Reanudar()
    {
        // Desactivar menús
        objetoMenuPausa.SetActive(false);
        MenuSalir.SetActive(false);
        enPausa = false;

        // Reanudar el juego
        Time.timeScale = 1;

        // Configurar cursor (ajustar según tu juego)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Reanudar sonidos
        ReanudarSonidos();
    }

    void ReanudarSonidos()
    {
        // Si no tenemos referencias, buscamos nuevamente
        if (fuentesDeAudio == null || fuentesDeAudio.Length == 0)
        {
            fuentesDeAudio = FindObjectsByType<AudioSource>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        // Reanudar cada sonido
        foreach (AudioSource fuente in fuentesDeAudio)
        {
            if (fuente != null && !fuente.isPlaying)
            {
                fuente.Play();
            }
        }
    }

    public void IrAlMenu(string nombreMenu)
    {
        // Asegurarse de reanudar el tiempo antes de cambiar de escena
        Time.timeScale = 1;
        SceneManager.LoadScene(nombreMenu);
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
        Application.Quit();

    }
}