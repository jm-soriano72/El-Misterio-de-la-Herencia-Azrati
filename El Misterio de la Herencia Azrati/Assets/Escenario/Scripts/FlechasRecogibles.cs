using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechasRecogibles : MonoBehaviour
{
    public static FlechasRecogibles instancia;
    public AudioClip clipAudio;
    public AudioSource reproductor;

    private void Awake()
    {
        instancia = this;
    }

    public void OnTriggerEnter(Collider other)
    {
        SistemasJugador jugador = other.GetComponent<SistemasJugador>();
        if (jugador != null)
        {
            // Efecto de sonido al recoger las flechas
            GestorMusica.PonerMusica(clipAudio, reproductor, false);
            // Aumento del número de flechas
            SistemasJugador.estadisticasPersonaje.numActualFlechas += 20;
            SistemasJugador.estadisticasPersonaje.ActualizarTextoFlechas();
            SistemasJugador.sistemaDialogos.MostrarDialogo(28);
            // Destrucción 
            StartCoroutine(Destruir());
        }
    }

    IEnumerator Destruir()
    {
        GestorGuardado.instancia.guardado.objetos.flechas = true;
        transform.position = Vector3.up * 1000;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
