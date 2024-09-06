using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaGravedad : MonoBehaviour
{
    public float gravedad = -9.82f;
    public float direccionY; // Alberga la dirección final de la velocidad en el eje Y
    public bool enSuelo; // Comprueba si el objeto está en el suelo
    public float limiteVelocidadCaida;

    // Update is called once per frame
    void Update()
    {
        CalcularGravedad();
    }

    public void CalcularGravedad()
    {
        // Si se está en el suelo y se tiene una velocidad negatica en el eje Y, se establece en 0, para evitar conflictos
        if(enSuelo&&direccionY<0)
        {
            direccionY = 0;
        }
        else
        {
            if(direccionY<=limiteVelocidadCaida)
            {
                return;
            }
            direccionY += gravedad * Time.deltaTime; // Se actualiza la velocidad en el eje Y en función de la aceleración de la gravedad
        }
    }
}
