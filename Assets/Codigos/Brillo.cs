using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider Slider; // El slider que controla el brillo
    public Image panelBrillo; // El panel cuya transparencia se ajusta
    private float sliderValue; // Guardar el valor actual del slider

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el valor guardado en PlayerPrefs o usar el valor predeterminado de 0.5
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);

        // Establecer el valor del slider en el valor guardado
        Slider.value = sliderValue;

        // Aplicar el valor del slider al panel (ajustando la opacidad)
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }

    // Este método es llamado cuando el slider cambia su valor
    public void ChangeSlider(float valor)
    {
        // Actualizar el valor del slider
        sliderValue = valor;

        // Guardar el nuevo valor de brillo en PlayerPrefs
        PlayerPrefs.SetFloat("brillo", sliderValue);

        // Aplicar el valor al panel (ajustando la opacidad)
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }
}
