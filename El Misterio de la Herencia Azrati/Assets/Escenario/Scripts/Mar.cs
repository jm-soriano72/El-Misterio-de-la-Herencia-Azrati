using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mar : MonoBehaviour
{
    public void Caer()
    {
        GestorReaparicion.instancia.Reaparicion();
        SistemasJugador.estadisticasPersonaje.ModificarVida(20);
    }
}
