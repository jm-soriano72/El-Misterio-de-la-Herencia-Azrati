using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GestorBotones : MonoBehaviour
{
    public AudioClip sonido;
    public AudioClip sonido2;
    public AudioSource reproductor;

    public void BotonIniciar()
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        GestorEscenas.instancia.CambiarEscena(1);
    }

    public void BotonCargar()
    {
        string ruta = Directory.GetCurrentDirectory() + "/Save";

        if (!File.Exists(ruta))
        {
            GestorMusica.PonerMusica(sonido2, reproductor, false);
            return;
        }
        GestorMusica.PonerMusica(sonido, reproductor, false);
        GestorGuardado.instancia.CargarDatos();
    }

    public void BotonSalir()
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        GestorEscenas.instancia.Salir();
    }
}
