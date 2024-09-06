using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    public AudioClip sonido;
    public AudioSource reproductor;

    public void OnTriggerEnter(Collider other)
    {
        SistemasJugador jugador = other.GetComponent<SistemasJugador>();
        if (jugador != null)
        {
            // Efecto de sonido al recoger el diamante
            GestorMusica.PonerMusica(sonido, reproductor, false);
            // Curación 
            SistemasJugador.estadisticasPersonaje.Curar();
            // Destrucción del diamante
            StartCoroutine(Destruir());
        }
    }

    IEnumerator Destruir()
    {
        transform.position = Vector3.up * 1000;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
