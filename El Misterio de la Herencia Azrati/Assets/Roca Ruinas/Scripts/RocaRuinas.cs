using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaRuinas : ElementoInteractuable
{
    void Update()
    {
        CalcularDistancia();
    }
    override public void AlInteractuar()
    {
        Debug.Log("Parece que esta roca tiene unos s�mbolos desgastados sobre su superficie. Posiblemente est� relacionado con �l mecanismo de los t�tems.");
        SistemasJugador.sistemaObjetivos.CambiarObjetivo(4);
        SistemasJugador.sistemaDialogos.MostrarDialogo(11);
        SistemasJugador.sistemaLogros.AparecerLogro(5); 
        SistemasJugador.sistemaLogros.CompletarLogro(4);
    }
}
