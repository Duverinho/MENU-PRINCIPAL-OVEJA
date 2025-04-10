using UnityEngine;

public class LogicaDaño : MonoBehaviour
{
    public BarraVida BarraVidaJugador;
    public BarraVida BarraVidaEnemigo;
    public float Daño = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BarraVidaJugador.vidaActual -= Daño;
            BarraVidaEnemigo.vidaActual -= Daño;
        }
    }
}
