using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RestablecerVolumen : MonoBehaviour
{
    // Referencia al Slider de volumen
    public Slider volumeSlider;

    // Valor predeterminado del volumen
    private float defaultVolume = 0.5f;  // El valor máximo de volumen (ajústalo si necesitas otro valor predeterminado)

    void Start()
    {
        // Si el botón está conectado, asignamos la acción de restablecer volumen
        Button resetButton = GetComponent<Button>();
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetVolumeSetting);
        }
    }

    // Método para restablecer el volumen al valor predeterminado
    public void ResetVolumeSetting()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = defaultVolume;  // Restablece el volumen al valor predeterminado
        }
    }
}