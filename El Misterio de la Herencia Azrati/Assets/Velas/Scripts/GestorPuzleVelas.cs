using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPuzleVelas : MonoBehaviour
{
    public static GestorPuzleVelas instancia;
    // La combinación correcta de velas, comenzando por la izquierda, es la siguiente
    public bool[] combinacionVelas = { false, true, false, true, true, true, false, true };
    // Conjunto de velas
    public GameObject[] velas;
    public bool[] estadoVelas;

    public bool activado = false;
    public bool resuelto = false;

    public bool pocionGenerada = false;

    public AudioSource reproductor;
    public AudioClip efecto;
    private bool reproduciendo;
    private bool finalizando = false;

    public GameObject flechas;

    private void Awake()
    {
        instancia = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (activado && GestorPociones.instancia.totalPociones[9]!=null)
        {
            GestorPociones.instancia.totalPociones[9].SetActive(true);
            pocionGenerada = true;
            if (!GestorGuardado.instancia.guardado.objetos.flechas)
                flechas.SetActive(true);

            GestorGuardado.instancia.guardado.activables.pocionGenerada = true;
        }
        if(activado)
        {
            resuelto = ComprobarEstadoVelas();
            if(resuelto&&!finalizando)
            {
                StartCoroutine(PuzleResuelto());
            }
        }
    }

    public bool ComprobarEstadoVelas()
    {
        for(int i=0; i<velas.Length; i++)
        {
            Vela vela = velas[i].GetComponent<Vela>();
            estadoVelas[i] = vela.encendida;
        }
        for(int j=0; j<velas.Length; j++)
        {
            if (estadoVelas[j] != combinacionVelas[j])
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator PuzleResuelto()
    {
        finalizando = true;
        if(!reproduciendo) GestorMusica.PonerMusica(efecto, reproductor, false);
        reproduciendo = true;
        SistemasJugador.sistemaDialogos.MostrarDialogo(19);
        SistemasJugador.sistemaLogros.AparecerLogro(7);
        SistemasJugador.sistemaLogros.CompletarLogro(7);
        yield return new WaitForSeconds(5f);
        // TRANSICIÓN DE ESCENAS
        if(SistemasJugador.estadisticasPersonaje.numUPs==3)
        {
            SistemasJugador.sistemaLogros.AparecerLogro(9);
            SistemasJugador.sistemaLogros.CompletarLogro(9);
        }
        if(!SistemasJugador.colisiones.caida)
        {
            SistemasJugador.sistemaLogros.AparecerLogro(12);
            SistemasJugador.sistemaLogros.CompletarLogro(12);
        }
        if(SistemasJugador.estadisticasPersonaje.numFlechasUsadas<70)
        {
            SistemasJugador.sistemaLogros.AparecerLogro(13);
            SistemasJugador.sistemaLogros.CompletarLogro(13);
        }
        // FIN DEL JUEGO
        // Se guardan los logros para mostrarlos en la pantalla de victoria
        GestorGuardado.instancia.GuardarLogros();
        // Escena de victoria
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GestorEscenas.instancia.CambiarEscena(2);
    }
}
