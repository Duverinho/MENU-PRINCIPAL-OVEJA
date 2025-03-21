using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PantallaCompleta : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolucionDropdown;
    Resolution[] resoluciones;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        RevisarResolucion();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }
        resolucionDropdown.AddOptions(opciones);
        resolucionDropdown.value = resolucionActual;
        resolucionDropdown.RefreshShownValue();
        resolucionDropdown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionDropdown.value);
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
    public void RestablecerConfiguracionPantalla()
    {
        // Restablecer la pantalla completa a falso
        toggle.isOn = false; // Si prefieres que inicie en pantalla completa, cambia esto a 'true'
        Screen.fullScreen = false;

        // Restablecer la resolución a la resolución actual del sistema
        int resolucionActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            if (resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
                break;
            }
        }

        resolucionDropdown.value = resolucionActual; // Seleccionar la resolución actual
        Screen.SetResolution(resoluciones[resolucionActual].width, resoluciones[resolucionActual].height, Screen.fullScreen);

        // Guardar la configuración restablecida
        PlayerPrefs.SetInt("numeroResolucion", resolucionActual);
        PlayerPrefs.Save();  // Guardar inmediatamente en PlayerPrefs
    }
}
