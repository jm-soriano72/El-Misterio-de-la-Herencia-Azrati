using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    // Distancia m�xima a la que puede estar el jugador respecto del cofre para poder abrirlo
    public float distanciaMaxima = 2f;
    public float distanciaJugador;
    // Botones
    public GameObject[] botones;
    // Formaci�n rocosa
    public GameObject granRoca;
    // L�mites
    public GameObject[] limites;

    public AudioSource reproductor;
    public AudioClip clipAudio;


    private void OnMouseDown()
    {
        if(distanciaJugador<=distanciaMaxima)
        {
            SistemasJugador.sistemaLogros.AparecerLogro(3);
            SistemasJugador.sistemaLogros.CompletarLogro(3);
            GestorMusica.PonerMusica(clipAudio, reproductor, false);
            // Se mostrar� en pantalla esta frase, junto con un mapa. Adem�s, aparecer�n los botones que mueven a los t�tems y se activar� el puzle
            Debug.Log("<< Se iluminan sus ojos cuando se cruzan sus miradas, abriendo el camino hacia la sabidur�a >>");
            SistemasJugador.sistemaDialogos.MostrarDialogo(9);

            ////////////////////////
            // MOSTRAR EL MAPA
            ////////////////////////
            StartCoroutine(MostrarMapa());
            
            for (int i=0; i<botones.Length;i++)
            {
                botones[i].SetActive(true);
            }
            GestorPuzleTotems.instancia.activado = true;
            GestorGuardado.instancia.guardado.activables.puzleTotems = true;
            // Tambi�n se desplaza la formaci�n rocosa hacia abajo, para permitir el paso
            GranRoca roca = granRoca.GetComponent<GranRoca>();
            if(roca != null )
            {
                roca.interactuado = true; // Se indica que ya se ha interactuado con la formaci�n rocosa, para que el mensaje al interactuar cambie
            }
            granRoca.transform.position = new Vector3(granRoca.transform.position.x, -21f, granRoca.transform.position.z);
            // Se desactivan sus l�mites
            for(int j=0; j<limites.Length; j++)
            {
                limites[j].SetActive(false);
            }

            Debug.Log("Estoy seguro de que el mensaje y el mapa son claves para poder abrir la puerta del templo. Tendr� que ir donde se indica.");
            
            StartCoroutine(CambioDialogo());

        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcularDistancia();
    }

    private void CalcularDistancia()
    {
        distanciaJugador = Vector3.Distance(SistemasJugador.movimientoJugador.posicionActual, transform.position);
    }

    IEnumerator MostrarMapa()
    {
        yield return new WaitForSeconds(3.5f);
        SistemasJugador.controles.ActivarMapa();

    }

    IEnumerator CambioDialogo()
    {
        // Espera a que desaparezca el mapa
        yield return new WaitForSeconds(10f);
        SistemasJugador.sistemaDialogos.MostrarDialogo(10);
        SistemasJugador.estadisticasPersonaje.numActualFlechas += 30;
        SistemasJugador.estadisticasPersonaje.ActualizarTextoFlechas();
        yield return new WaitForSeconds(2f);
        SistemasJugador.sistemaObjetivos.CambiarObjetivo(3);
    }
}
