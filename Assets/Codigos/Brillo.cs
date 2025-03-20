using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider Slider;
    public float sliderValue;
    public Image panelBrillo;
    // Start is called before the first frame update
    void Start()
    {
        Slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, Slider.value);

        Slider.onValueChanged.AddListener(ChangeSlider);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSlider(float valor)
    {
        Slider.value = valor;
        PlayerPrefs.SetFloat("brillo", valor);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, Slider.value);
    }
}
