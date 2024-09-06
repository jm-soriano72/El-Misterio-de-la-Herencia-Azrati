using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour, IActivable
{
    public List <Vector3> posiciones;
    private int tamaño;
    public int indice = 0;
    public float velocidad = 3f;
    public bool activado = false;
    // Tiempo de espera al llegar al objetivo
    public float contador = 0f;
    public float tiempoEspera = 4f;
    // Objetivo alcanzado
    public bool alcanzado = false;
    // Cambio de destino
    public bool nuevaRuta = false;

    void Start()
    {
        tamaño = posiciones.Count;
    }
    // Update is called once per frame
    void Update()
    {
        if(activado&&!alcanzado)
        {
            transform.position = Vector3.MoveTowards(transform.position, posiciones[indice], velocidad * Time.deltaTime);
            alcanzado = ComprobarPosicion();
        }
        if(alcanzado)
        {           
            contador += Time.deltaTime;
        }
        if(alcanzado&&contador >= tiempoEspera)
        {
            contador = 0;
            alcanzado = false;
            CambiarDestino();
        }
    }

    public bool ComprobarPosicion()
    {
        // De esta manera, cuando la plataforma esté muy cerca de su objetivo, toma directamente esta posición para evitar errores
        if(Vector3.Distance(transform.position, posiciones[indice])<=0.01f)
        {
            transform.position = posiciones[indice];
            contador = 0;
            return true;
        }
        return false;
    }

    public void CambiarDestino()
    {
        indice++;
        if(indice>=tamaño)
        {
            indice = 0;
        }
    }

    public void Activar(int indice)
    {
        if(indice==0)
        {
            activado = true;
        }
        else
        {
            if(!nuevaRuta)
            {
                // Función reservada para la palanca que modifica los objetivos de la plataforma inicial
                posiciones.Add(new Vector3(115.7f, 3f, 0));
                posiciones.Add(new Vector3(115.7f, 13f, -22.2f));
                tamaño = posiciones.Count;
                nuevaRuta = true;
                SistemasJugador.sistemaLogros.CompletarLogro(2);
            }
        }
    }
}
