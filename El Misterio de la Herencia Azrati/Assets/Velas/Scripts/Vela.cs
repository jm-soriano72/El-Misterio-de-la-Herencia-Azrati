using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vela : MonoBehaviour
{
    public int indice;
    public bool encendida;
    public float distanciaJugador;
    public GameObject llama;
    public GameObject velaSinLlama;

    public AudioSource reproductor;
    public AudioClip sonido;

    private void OnMouseDown()
    {
        if(distanciaJugador<=3 && GestorPuzleVelas.instancia.activado)
        {
            if(encendida)
            {
                encendida = false;
                llama.SetActive(false);
                velaSinLlama.SetActive(true);
            }
            else
            {
                encendida = true;
                llama.SetActive(true);
                velaSinLlama.SetActive(false);
                GestorMusica.PonerMusica(sonido, reproductor, false);
            }
        }
    }

    private void Update()
    {
        CalcularDistancia();
    }

    private void CalcularDistancia()
    {
        distanciaJugador = Vector3.Distance(SistemasJugador.movimientoJugador.posicionActual, transform.position);
    }
}
