using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasPersonaje : MonoBehaviour, IDañable, IPuedoMorir
{
    // Slider que manejará la vida del personaje
    public Slider sliderVida;
    // Estadisticas del personaje
    public Estadisticas estadisticasPersonaje;
    // Número de diamantes conseguidos
    public int numDiamantes;
    public TMP_Text textoDiamantes;
    // Número de "UPs"
    public int numUPs = 3;
    public TMP_Text textoVidas;
    // Número de esqueletos derrotados
    public int numEsqueletos;
    public TMP_Text textoEsqueletos;
    // Número de pociones recogidas
    public int numPociones;
    // Número de flechas
    public int numActualFlechas = 50;
    // Número de flechas usadas
    public int numFlechasUsadas;
    public TMP_Text textoFlechas;

    public AudioSource reproductor;
    public AudioClip sonido;
    public bool iniciado = false;

    private void Awake()
    {
        reproductor = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ModificarVida(0);
        ActualizarInterfaz();
    }

    void Update()
    {
        ComprobarVida();
        if(numDiamantes==100)
        {
            SistemasJugador.sistemaLogros.CompletarLogro(10);
        }
        if(numEsqueletos==58)
        {
            SistemasJugador.sistemaLogros.CompletarLogro(11);
        }
        if(numPociones == 10)
        {
            SistemasJugador.sistemaLogros.CompletarLogro(8);
        }
    }

    public void ModificarVida(float cantidad)
    {
        if(iniciado) GestorMusica.PonerMusica(sonido, reproductor, false);
        iniciado = true;
        estadisticasPersonaje.ModificarVida(cantidad);
        ActualizarSliderVida();
    }

    public void ActualizarInterfaz()
    {
        ActualizarSliderVida();
    }

    public void ActualizarSliderVida()
    {
        // Se establecen los valores del slider para que este se vaya actualizando
        sliderVida.maxValue = estadisticasPersonaje.vidaMaxima;
        sliderVida.value = estadisticasPersonaje.vidaActual;
    }

    public void ActualizarNumeroDiamantes()
    {
        numDiamantes++;
        ActualizarTextoDiamantes();
    }

    public void ActualizarTextoDiamantes()
    {
        if(numDiamantes<10)
        {
            textoDiamantes.text = "00"+numDiamantes.ToString();
        }
        else if(numDiamantes<100)
        {
            textoDiamantes.text = "0" + numDiamantes.ToString();
        }
        else
        {
            textoDiamantes.text = numDiamantes.ToString();
        }

    }

    // Continuamente se comprueba la vida del personaje
    public void ComprobarVida()
    {
        if(estadisticasPersonaje.vidaActual == 0)
        {
            AlMorir();
        }
    }
    // Si el personaje pierde toda su vida, se comprueba si le quedan continuaciones o no
    public void AlMorir()
    {
        numUPs--;
        if(numUPs>0)
        {
            // Si le quedan continuaciones, se reinicia el estado de la vida
            ReiniciarVida();
            GestorReaparicion.instancia.Reaparicion();
            ActualizarNumeroVidas();
        }
        else
        {
            // Escena de derrota. Se puede volver al inicio desde ella
            GestorEscenas.instancia.CambiarEscena(3);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ActualizarNumeroVidas()
    {
        textoVidas.text = numUPs.ToString();
    }

    public void ReiniciarVida()
    {
        estadisticasPersonaje.vidaActual = 100;
        estadisticasPersonaje.vidaPerdida = 0;
        ActualizarSliderVida();
    }

    public void EsqueletoMuerto()
    {
        numEsqueletos++;
        ActualizarTextoEsqueletos();
    }

    public void ActualizarTextoEsqueletos()
    {
        if(numEsqueletos<10)
        {
            textoEsqueletos.text = "0"+numEsqueletos.ToString();
        }
        else
        {
            textoEsqueletos.text = numEsqueletos.ToString();
        }

    }

    public void Curar()
    {
        if(estadisticasPersonaje.vidaActual<=80)
        {
            estadisticasPersonaje.ModificarVida(-20);
            ActualizarSliderVida();
        }
        else
        {
            estadisticasPersonaje.vidaActual = 100;
            ActualizarSliderVida();
        }
        numPociones++;
    }

    public void UsarFlecha()
    {
        numActualFlechas--;
        numFlechasUsadas++;
        ActualizarTextoFlechas();
    }

    public void ActualizarTextoFlechas()
    {
        if(numActualFlechas<10)
        {
            textoFlechas.text = "0" + numActualFlechas.ToString();
        }
        else
        {
            textoFlechas.text = numActualFlechas.ToString();
        }
    }
}
