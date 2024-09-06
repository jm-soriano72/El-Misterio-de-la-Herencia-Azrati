using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuertaTemplo : ElementoInteractuable
{
    public GameObject totem;

    // Update is called once per frame
    void Update()
    {
        CalcularDistancia();
        ComprobarTotems();
    }

    override public void AlInteractuar()
    {
        Debug.Log("Tal y como dice la leyenda, la puerta del templo permanece cerrada y solo aquellos que demuestren su valía podran abrirla...");
        SistemasJugador.sistemaDialogos.MostrarDialogo(3);
        interactuado = true;
        SistemasJugador.sistemaLogros.CompletarLogro(0);
        GestorGuardado.instancia.guardado.activables.puertaInteractuada = true;
        // El objetivo que se muestra al jugador depende de si ya ha interactuado con los tótems o no
        IAlInteractuar objeto = totem.GetComponent<IAlInteractuar>();
        if (objeto != null)
        {
            if(objeto.HaSidoInteractuado())
            {
                StartCoroutine(CambioDialogo(4,0));
                Debug.Log("La única pista que he visto por la isla son aquellos tótems que miran hacia el este, tal vez debería explorar esa zona");
                SistemasJugador.sistemaLogros.AparecerLogro(1);
            }
            else
            {
                StartCoroutine(CambioDialogo(5, 1));
                Debug.Log("Quizás debería echar un vistazo a aquellas estatuas que encontré por el camino.");                
            }
        }

    }

    public void ComprobarTotems()
    {
        if (!GestorPuzleTotems.instancia.resuelto)
        {
            return;
        }
        this.gameObject.SetActive(false);
        GestorGuardado.instancia.guardado.activables.puertaTemplo = false;
    }

    // De esta forma, se produce una conversación entre los pensamientos del personaje
    IEnumerator CambioDialogo(int dialogo, int objetivo)
    {
        yield return new WaitForSeconds(9f);
        SistemasJugador.sistemaDialogos.MostrarDialogo(dialogo);
        yield return new WaitForSeconds(2f);
        SistemasJugador.sistemaObjetivos.CambiarObjetivo(objetivo);
    }
}
