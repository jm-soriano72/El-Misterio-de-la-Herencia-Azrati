using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemasJugador : MonoBehaviour
{
    // Se añaden como atributos todos los scripts que manejan distintos aspectos del jugador. Se coloca el modificador static para que sean accesibles desde fuera
    public static Controles controles;
    public static MovimientoJugador movimientoJugador;
    public static MovimientoCamara movimientoCamara;
    public static SistemaGravedad sistemaGravedad;
    public static SistemaRaycast sistemaRaycast;
    public static EstadisticasPersonaje estadisticasPersonaje;
    public static Colisiones colisiones;
    public static AccionesJugador accionesJugador;
    public static SistemaObjetivos sistemaObjetivos;
    public static SistemaDialogos sistemaDialogos;
    public static SistemaLogros sistemaLogros;

    public int fps;
    private double framesAcumulados;
    private float temporizador;
    public int actualizacionesPorSegundo;
    public TMP_Text textoFPS;
    public bool debug;


    private void Awake()
    {
        controles = GetComponent<Controles>();
        movimientoJugador = GetComponent<MovimientoJugador>();
        movimientoCamara = GetComponent<MovimientoCamara>();
        sistemaGravedad = GetComponent<SistemaGravedad>();
        sistemaRaycast = GetComponent<SistemaRaycast>();
        estadisticasPersonaje = GetComponent<EstadisticasPersonaje>();
        colisiones = GetComponent<Colisiones>();
        accionesJugador = GetComponent<AccionesJugador>();
        sistemaObjetivos = GetComponent<SistemaObjetivos>();
        sistemaDialogos= GetComponent<SistemaDialogos>();
        sistemaLogros= GetComponent<SistemaLogros>();

        textoFPS.gameObject.SetActive(debug);
    }


    private void Update()
    {
        CalcularFPS();
    }

    public void CalcularFPS()
    {
        framesAcumulados += 1;
        temporizador += Time.deltaTime;
        if(temporizador>=1f/actualizacionesPorSegundo)
        {
            fps = (int)(framesAcumulados*actualizacionesPorSegundo);
            textoFPS.text = fps.ToString();
            framesAcumulados = 0;
            temporizador = 0;
        }
    }
}
