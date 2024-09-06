using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorReaparicion : MonoBehaviour
{
    public static GestorReaparicion instancia;
    public GameObject personaje;
    public PuntosControl[] puntos;

    private void Awake()
    {
        instancia = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Aparecer(0);
    }

    public void Aparecer(int id)
    {
        GameObject personajeNuevo = Instantiate(personaje);
        personaje = personajeNuevo;
        MoverPersonaje(id);
    }

    public void MoverPersonaje(int id)
    {
        personaje.transform.position = puntos[id].transform.position;
    }

    public void Reaparicion()
    {
        // Buscar el punto de control más cercano y mover el personaje a él
        // Esto podría dar problemas si el punto 0 no está activado y hay otro punto activado un poco más lejos de él, que sea el de distancia menor
        float distanciaMinima = Vector3.Distance(personaje.transform.position, puntos[0].transform.position);
        int idAux = 0;
        for (int i = 1; i < puntos.Length; i++)
        {
            // Si el punto de control a recorrer está activado, se comprueba si es el más cercano
            if (puntos[i].activado)
            {
                float distanciaActual = Vector3.Distance(personaje.transform.position, puntos[i].transform.position);
                if (distanciaActual < distanciaMinima)
                {
                    idAux = i;
                    distanciaMinima = distanciaActual;
                }

            }
        }
        MoverPersonaje(idAux);
    }
}
