using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider Slider; // El slider que controla el brillo
    public Image panelBrillo; // El panel cuya transparencia se ajusta
    private float sliderValue; // Guardar el valor actual del slider
    private float valorPredeterminado = 0.49f; // Valor predeterminado de brillo

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el valor guardado en PlayerPrefs o usar el valor predeterminado de 0.5
        sliderValue = PlayerPrefs.GetFloat("brillo", valorPredeterminado);

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
        PlayerPrefs.Save();  // Asegúrate de que los cambios se guarden inmediatamente

        // Aplicar el valor al panel (ajustando la opacidad)
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }

    // Método para restablecer el brillo al valor predeterminado
    public void RestablecerBrillo()
    {
        // Restablecer el valor del slider al valor predeterminado
        sliderValue = valorPredeterminado;

        // Actualizar el slider
        Slider.value = sliderValue;

        // Restablecer el valor de brillo en PlayerPrefs
        PlayerPrefs.SetFloat("brillo", sliderValue);
        PlayerPrefs.Save();  // Guardar inmediatamente

        // Aplicar el valor predeterminado al panel (ajustando la opacidad)
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }
}
