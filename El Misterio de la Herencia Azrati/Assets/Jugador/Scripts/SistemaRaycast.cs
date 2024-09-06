using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaRaycast : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        DetectarSuelo();
        
    }
    private void DetectarSuelo()
    {
        RaycastHit datos;

        // Altura del personaje
        float tamaño = GetComponent<Renderer>().bounds.size.y;
        // Posición inicial, radio de la pelota, dirección, datos y distancia
        if (Physics.SphereCast(transform.position, tamaño/4, Vector3.down, out datos, tamaño/4 + 0.1f))
        {
            SistemasJugador.sistemaGravedad.enSuelo = true;
        }
        else
        {
            SistemasJugador.sistemaGravedad.enSuelo = false;
        }
    }
}
