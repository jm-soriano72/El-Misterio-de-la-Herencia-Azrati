using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totems : ElementoInteractuable
{
    public GameObject[] totems;
    public GameObject puertaTemplo;
    public Vector3 posicionFinal;

    private void Start()
    {
        posicionFinal= transform.position;
    }
    void Update()
    {
        CalcularDistancia();
        transform.position = Vector3.MoveTowards(transform.position, posicionFinal, 3 * Time.deltaTime);
    }
    override public void AlInteractuar()
    {
        if(!GestorPuzleTotems.instancia.resuelto)
        {
            Debug.Log("Estas estatuas parecen tótems... Todas están mirando hacia la misma dirección. Igual significa algo");
            GestorGuardado.instancia.guardado.activables.totemsInteractuados = true;
            SistemasJugador.sistemaDialogos.MostrarDialogo(6);
            interactuado = true;
            ActivarOtros();

            PuertaTemplo puerta = puertaTemplo.GetComponent<PuertaTemplo>();
            if (puerta.interactuado)
            {
                SistemasJugador.sistemaObjetivos.CambiarObjetivo(0);
                SistemasJugador.sistemaLogros.AparecerLogro(1);
            }
        }
        else
        {
            Debug.Log("Ahora que he conseguido que todos los tótems crucen sus miradas, algo debe haber sucedido.");
            SistemasJugador.sistemaDialogos.MostrarDialogo(7);
        }
    }

    public void ActivarOtros()
    {
        for (int i = 0; i < totems.Length; i++)
        {
            IAlInteractuar objetoInteractuable = totems[i].GetComponent<IAlInteractuar>();
            objetoInteractuable.AsignarInteractuado(true);
        }
    }

    public void DefinirFinal()
    {
        posicionFinal = new Vector3(transform.position.x, transform.position.y - 4f, transform.position.z);
    }
}
