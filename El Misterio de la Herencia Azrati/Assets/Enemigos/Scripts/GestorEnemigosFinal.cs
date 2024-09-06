using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorEnemigosFinal : MonoBehaviour
{
    public static GestorEnemigosFinal instancia;

    public bool activado = false;
    public bool generar = false;
    public GameObject enemigo;
    public int enemigosGenerados = 0;
    public int maxEnemigos;
    public float contador = 2f;
    public float tiempoEspera = 2f;
    public bool dialogoMostrado = false;
    public Vector3 direccionActual;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        direccionActual = new Vector3(transform.position.x - 11f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(activado&&generar&&enemigosGenerados<maxEnemigos)
        {
            transform.position = Vector3.MoveTowards(transform.position, direccionActual, 6 * Time.deltaTime);
            ComprobarPosicion();
            contador += Time.deltaTime;
            if(contador>=tiempoEspera)
            {
                Instantiate(enemigo, transform.position, Quaternion.identity);
                enemigosGenerados++;
                contador = 0f;
            }
        }
        else
        {
            if(enemigosGenerados==maxEnemigos)
            {
                contador += Time.deltaTime;
                if(contador>=tiempoEspera+2&&!dialogoMostrado)
                {
                    GestorGuardado.instancia.guardado.activables.finalDerrotado = true;
                    SistemasJugador.sistemaDialogos.MostrarDialogo(23);
                    SistemasJugador.sistemaLogros.AparecerLogro(6);
                    SistemasJugador.sistemaLogros.CompletarLogro(6);
                    dialogoMostrado = true;
                    contador = 0f;
                }
            }
        }

    }

    public void NuevaDireccion()
    {
        if(direccionActual.x > 5f)
        {
            direccionActual = new Vector3(transform.position.x - 22f, transform.position.y, transform.position.z);
        }
        else
        {
            direccionActual = new Vector3(transform.position.x + 22f, transform.position.y, transform.position.z);
        }
    }

    public void ComprobarPosicion()
    {
        if(Vector3.Distance(transform.position, direccionActual)<0.1f)
        {
            NuevaDireccion();
        }
    }
}
