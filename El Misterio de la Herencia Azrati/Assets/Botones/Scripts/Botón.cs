using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botón : MonoBehaviour
{
    // Array de objetos que se activan con este botón
    public GameObject[] activables;
    // Distancia máxima a la que puede estar el jugador respecto del botón para activarlo
    public float distanciaMaxima = 2f;
    public float distanciaJugador;
    // Indice del botón, para poder comprobar el puzle
    public int indice;

    private void OnMouseDown()
    {
        if (distanciaJugador <= distanciaMaxima && GestorPuzleTotems.instancia.activado)
        {
            // Si la distancia a la que se encuentra el personaje es menor al límite establecido, se activan los objetos conectados con el botón
            Activar();
            // Se añade el índice del botón pulsado al gestor de los tótems
            GestorPuzleTotems.instancia.PulsarBoton(indice);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        CalcularDistancia();
    }

    private void CalcularDistancia()
    {
        distanciaJugador = Vector3.Distance(SistemasJugador.movimientoJugador.posicionActual, transform.position);
    }

    public void Activar()
    {
        // Antes de activar el botón, se produce la animación de que se ha pulsado
        StartCoroutine("BotonPulsado");
        for(int i=0; i<activables.Length; i++)
        {
            IActivable objetoActivar = activables[i].GetComponent<IActivable>();
            if(objetoActivar != null)
            {
                objetoActivar.Activar(indice);
            }
        }
    }

    IEnumerator BotonPulsado()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        yield return new WaitForSeconds(.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
    }
}
