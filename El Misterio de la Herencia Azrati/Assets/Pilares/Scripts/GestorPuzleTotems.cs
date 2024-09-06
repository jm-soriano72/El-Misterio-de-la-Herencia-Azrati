using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPuzleTotems : MonoBehaviour
{
    // Singleton para ver acceder al gestor desde cualquier script
    public static GestorPuzleTotems instancia;
    // Gestiona si se ha llegado al punto del juego necesario para poder resolver el puzle
    public bool activado;
    // Almacena los botones que se han ido pulsando
    public int[] botonesPulsados = { 0, 0, 0, 0, 0 };
    public int numBotonesPulsados;
    // Número de botones que tiene la clave
    public int maxBotones = 5;
    public int[] combinacionBotones = { 1, 3, 4, 1, 6 };
    public int numBoton1 = 2;
    public int numBoton3 = 1;
    public int numBoton4 = 1;
    public int numBoton6 = 1;
    public int boton1, boton2, boton3, boton4, boton5, boton6;
    public bool resuelto = false;
    // Tótems
    public GameObject[] totems;

    private void Awake()
    {
        instancia = this;
    }

    // Update is called once per frame
    void Update()
    {
        PuzleResuelto();
    }

    public void PuzleResuelto()
    {
        if(resuelto&&activado)
        {
            GestorGuardado.instancia.guardado.activables.puzleTotemsResuelto = true;
            GestorGuardado.instancia.guardado.activables.puzleVelas = true;
            GestorGuardado.instancia.guardado.activables.puzleTotems = false;
            SistemasJugador.sistemaLogros.CompletarLogro(5);
            // RESULTADO DEL PUZLE
            activado = false;
            // Mensaje de que se ha resuelto el puzle
            SistemasJugador.sistemaDialogos.MostrarDialogo(18);
            // Cambio del objetivo al resolver el puzle
            SistemasJugador.sistemaObjetivos.CambiarObjetivo(5);
            // Descenso de los tótems
            for(int i=0; i<totems.Length; i++)
            {
                Totems totem = totems[i].GetComponent<Totems>();
                totem.DefinirFinal();
            }
            GestorPuzleVelas.instancia.activado = true;
            GestorEnemigosFinal.instancia.activado = true;
        }
        if(activado&&!resuelto)
        resuelto = ComprobarBotones();
    }
    public bool ComprobarBotones()
    {
        // Se comprueba si se han pulsado todos los botones necesarios
        if(numBotonesPulsados == combinacionBotones.Length)
        {
            // Se comparan los botones pulsados y si coinciden con la clave
            for(int i=0; i< combinacionBotones.Length; i++)
            {
                switch(botonesPulsados[i])
                {
                    case 1: boton1++; break;
                    case 2: boton2++; break;
                    case 3: boton3++; break;
                    case 4: boton4++; break;
                    case 5: boton5++; break;
                    case 6: boton6++; break;
                }
            }
            // Si se han verificado todos los botones y coincide, se devuelve true
            if(boton1 == numBoton1 && boton3 == numBoton3 && boton4 == numBoton4 && boton6 == numBoton6)
            {
                return true;
            }
            if(!resuelto)
            {
                ReiniciarBotones();
                ReiniciarTotems();
            }
        }
        return false;
    }

    public void ReiniciarBotones()
    {
        // Se reinicia el contador de botones a 0
        for(int j=0; j<botonesPulsados.Length; j++)
        {
            botonesPulsados[j] = 0;
        }
        numBotonesPulsados = 0;
        boton1 = 0;
        boton2 = 0;
        boton3 = 0;
        boton4 = 0;
        boton5 = 0;
        boton6 = 0;
        SistemasJugador.sistemaDialogos.MostrarDialogo(17);
    }

    public void PulsarBoton(int indice)
    {
        // Cuando se pulsa un botón, se añade el índice del botón al array
        botonesPulsados[numBotonesPulsados] = indice;
        numBotonesPulsados++;
        // Se comprueba si se ha resuelto
        PuzleResuelto();
    }

    public void ReiniciarTotems()
    {
        activado = false;
        for(int i=0; i<totems.Length; i++)
        {
            RotacionTótems totem = totems[i].GetComponent<RotacionTótems>();
            if(totem!=null)
            {
                totem.rotacionDestino = -90f;
            }
        }
        activado = true;
        // Se reduce la vida del personaje en 20 puntos al fallar en el puzle
        SistemasJugador.estadisticasPersonaje.ModificarVida(20);
    }
}
