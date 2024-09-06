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
        float tama�o = GetComponent<Renderer>().bounds.size.y;
        // Posici�n inicial, radio de la pelota, direcci�n, datos y distancia
        if (Physics.SphereCast(transform.position, tama�o/4, Vector3.down, out datos, tama�o/4 + 0.1f))
        {
            SistemasJugador.sistemaGravedad.enSuelo = true;
        }
        else
        {
            SistemasJugador.sistemaGravedad.enSuelo = false;
        }
    }
}
