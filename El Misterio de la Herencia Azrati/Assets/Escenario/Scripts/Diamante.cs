using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour
{
    public AudioClip clipAudio;
    public AudioSource reproductor;

    public void OnTriggerEnter(Collider other)
    {
        SistemasJugador jugador = other.GetComponent<SistemasJugador>();
        if(jugador != null)
        {
            // Efecto de sonido al recoger el diamante
            GestorMusica.PonerMusica(clipAudio, reproductor, false);
            // Aumento del contador de diamantes
            SistemasJugador.estadisticasPersonaje.ActualizarNumeroDiamantes();
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
