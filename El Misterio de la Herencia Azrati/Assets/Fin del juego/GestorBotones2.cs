using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorBotones2 : MonoBehaviour
{
    public AudioClip sonido;
    public AudioSource reproductor;

    public void BotonVolver()
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        GestorEscenas.instancia.CambiarEscena(0);
    }
    public void BotonSalir()
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        GestorEscenas.instancia.Salir();
    }
}
