using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    public GameObject[] activables;
    public float distanciaMaxima = 2f;
    public float distanciaJugador;
    public int funcionActivar;

    public GameObject palanca;
    public Vector3 rotacion;

    public bool activada = false;
    public int indice;

    public AudioClip sonido;
    public AudioSource reproductor;

    private void OnMouseDown()
    {
        if(distanciaJugador<distanciaMaxima && !activada)
        {
            Activar();
            activada = true;
        }
    }

    public void PalancaActivada()
    {
        for (int i = 0; i < activables.Length; i++)
        {
            IActivable objetoActivar = activables[i].GetComponent<IActivable>();
            if (objetoActivar != null)
            {
                objetoActivar.Activar(funcionActivar);
            }
        }
        // Giro de la palanca al activarla
        palanca.transform.rotation = Quaternion.Euler(rotacion.x + (70 / Mathf.Rad2Deg) * Mathf.Rad2Deg, rotacion.y * Mathf.Rad2Deg, rotacion.z * Mathf.Rad2Deg);
        activada = true;
    }

    void Update()
    {
        CalcularDistancia();
        rotacion = Quaternion.ToEulerAngles(palanca.transform.rotation);
    }

    private void CalcularDistancia()
    {
        distanciaJugador = Vector3.Distance(SistemasJugador.movimientoJugador.posicionActual, transform.position);
    }

    public void Activar()
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        SistemasJugador.sistemaLogros.AparecerLogro(2);
        switch(indice)
        {
            case 1:
                GestorGuardado.instancia.guardado.activables.palanca1 = true;
                break;
            case 2:
                GestorGuardado.instancia.guardado.activables.palanca2 = true;
                break;
            case 3:
                GestorGuardado.instancia.guardado.activables.palanca3 = true;
                break;
        }

        for(int i=0; i<activables.Length; i++)
        {
            IActivable objetoActivar = activables[i].GetComponent<IActivable>();
            if(objetoActivar != null)
            {
                objetoActivar.Activar(funcionActivar);
            }
        }
        // Giro de la palanca al activarla
        palanca.transform.rotation = Quaternion.Euler(rotacion.x + (70/ Mathf.Rad2Deg)* Mathf.Rad2Deg, rotacion.y * Mathf.Rad2Deg, rotacion.z * Mathf.Rad2Deg);
        SistemasJugador.sistemaDialogos.MostrarDialogo(14);
    }
}
