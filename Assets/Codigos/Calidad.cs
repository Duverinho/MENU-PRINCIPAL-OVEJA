using TMPro;
using UnityEngine;

public class Calidad : MonoBehaviour
{
    public TMP_Dropdown dropdown; // El Dropdown que controla la calidad
    public int calidad; // Variable para almacenar el valor de calidad
    private int calidadPredeterminada = 3; // Valor predeterminado de calidad (ajustado según tus necesidades)

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el valor guardado en PlayerPrefs o usar el valor predeterminado de 3
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", calidadPredeterminada);

        // Establecer el valor del dropdown en el valor guardado o predeterminado
        dropdown.value = calidad;

        // Ajustar la calidad
        AjustarCalidad();
    }

    // Este método es llamado cuando el dropdown cambia su valor
    public void AjustarCalidad()
    {
        // Establecer la calidad seleccionada
        QualitySettings.SetQualityLevel(dropdown.value);

        // Guardar el nuevo valor de calidad en PlayerPrefs
        PlayerPrefs.SetInt("numeroDeCalidad", dropdown.value);
        PlayerPrefs.Save();  // Guardar inmediatamente

        // Actualizar el valor de la variable calidad
        calidad = dropdown.value;
    }

    // Método para restablecer la calidad al valor predeterminado
    public void RestablecerCalidad()
    {
        // Establecer la calidad al valor predeterminado
        dropdown.value = calidadPredeterminada;

        // Ajustar la calidad según el valor predeterminado
        QualitySettings.SetQualityLevel(calidadPredeterminada);

        // Guardar el valor predeterminado en PlayerPrefs
        PlayerPrefs.SetInt("numeroDeCalidad", calidadPredeterminada);
        PlayerPrefs.Save();  // Guardar inmediatamente

        // Actualizar la variable de calidad
        calidad = calidadPredeterminada;
    }
}
