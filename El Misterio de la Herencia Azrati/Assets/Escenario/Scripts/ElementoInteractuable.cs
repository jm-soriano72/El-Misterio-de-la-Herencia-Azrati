using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoInteractuable : MonoBehaviour, IAlInteractuar
{
    // Distancia máxima a la que puede estar el jugador respecto de la puerta
    public float distanciaMaxima = 3.5f;
    public float distanciaJugador;

    public bool interactuado = false;

    private void OnMouseDown()
    {
        if(distanciaJugador<=distanciaMaxima)
        {
            AlInteractuar();
        }
    }

    public void CalcularDistancia()
    {
        distanciaJugador = Vector3.Distance(SistemasJugador.movimientoJugador.posicionActual, transform.position);
    }

    public virtual void AlInteractuar() { }
    public bool HaSidoInteractuado() { return interactuado; }

    public void AsignarInteractuado(bool interact)
    {
        interactuado = interact;
    }

}
