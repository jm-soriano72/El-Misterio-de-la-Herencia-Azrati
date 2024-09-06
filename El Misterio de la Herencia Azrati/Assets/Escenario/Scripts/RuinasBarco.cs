using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuinasBarco : ElementoInteractuable
{
    // En este array se van a guardar los objetos con los que es necesario interactuar, para poder avanzar en el juego
    public GameObject[] objetosRequisito;
    public GameObject nuevaZona;

    void Update()
    {
        CalcularDistancia();
        ComprobarRequisitos();
    }
    override public void AlInteractuar()
    {
        Debug.Log("Parece que son los restos de una casa o un barco naufragado... no debo ser el primero, ni mucho menos, que intenta descubrir el secreto de esta isla.");
        SistemasJugador.sistemaDialogos.MostrarDialogo(0);
    }

    public void ComprobarRequisitos()
    {
        // Se comprueba si se ha interactuado con los objetos necesarios
        bool interactuado = false;
        for(int i=0; i<objetosRequisito.Length; i++)
        {
            IAlInteractuar objetoInteractuable = objetosRequisito[i].GetComponent<IAlInteractuar>();
            interactuado = objetoInteractuable.HaSidoInteractuado();
            if(!interactuado)
            {
                return;
            }
        }
        this.gameObject.SetActive(false);
        GestorGuardado.instancia.guardado.activables.ruinasBarco = false;
        nuevaZona.SetActive(true);
        GestorGuardado.instancia.guardado.activables.zonaRuinas = true;
    }
}
