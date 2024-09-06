using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorActivables : MonoBehaviour
{
    public static GestorActivables instancia;
    public GameObject puertaTemplo;
    public GameObject[] puntosControl;
    public GameObject ruinasBarco;
    public GameObject ruinasBarcoZona;
    public GameObject[] totems;
    public GameObject[] botones;
    public GameObject granRoca;
    public GameObject[] limitesRoca;
    public GameObject[] palancas;

    private void Awake()
    {
        instancia = this;
    }
    public void PuertaTemplo(bool activo)
    {
        puertaTemplo.SetActive(activo);
    }

    public void PuntosDeControl(bool control1, bool control2)
    {
        PuntosControl punto1 = puntosControl[0].GetComponent<PuntosControl>();
        punto1.activado = control1;

        PuntosControl punto2 = puntosControl[1].GetComponent<PuntosControl>();
        punto2.activado = control2;
    }

    public void RuinasBarco(bool ruinas, bool zona)
    {
        ruinasBarco.SetActive(ruinas);
        ruinasBarcoZona.SetActive(zona);
    }

    public void ObjetosParaRuinas(bool puertaInteractuada, bool totemsInteractuados)
    {
        PuertaTemplo puerta = puertaTemplo.GetComponent<PuertaTemplo>();
        puerta.interactuado = puertaInteractuada;

        for(int i=0; i<totems.Length; i++)
        {
            Totems totem = totems[i].GetComponent<Totems>();
            totem.interactuado = totemsInteractuados;
        }
    }

    public void PuzleTotemsActivado(bool puzleActivado)
    {
        GestorPuzleTotems.instancia.activado = puzleActivado;
        if(puzleActivado)
        {
            for (int i = 0; i < botones.Length; i++)
            {
                botones[i].SetActive(true);
            }
            GranRoca roca = granRoca.GetComponent<GranRoca>();
            if (roca != null)
            {
                roca.interactuado = true; // Se indica que ya se ha interactuado con la formación rocosa, para que el mensaje al interactuar cambie
            }
            granRoca.transform.position = new Vector3(granRoca.transform.position.x, -21f, granRoca.transform.position.z);
            // Se desactivan sus límites
            for (int j = 0; j < limitesRoca.Length; j++)
            {
                limitesRoca[j].SetActive(false);
            }
            SistemasJugador.controles.mapaDesbloqueado = true;
        }
    }

    public void PuzleTotemsResuelto(bool resuelto, bool final)
    {
        GestorPuzleTotems.instancia.resuelto = resuelto;
        if (resuelto)
        {
            GestorPuzleTotems.instancia.activado = false;
            GestorPuzleVelas.instancia.activado = true;
            GestorEnemigosFinal.instancia.activado = !final;
            puertaTemplo.SetActive(false);
            for (int i = 0; i < botones.Length; i++)
            {
                botones[i].SetActive(true);
            }
            GranRoca roca = granRoca.GetComponent<GranRoca>();
            if (roca != null)
            {
                roca.interactuado = true; // Se indica que ya se ha interactuado con la formación rocosa, para que el mensaje al interactuar cambie
            }
            granRoca.transform.position = new Vector3(granRoca.transform.position.x, -21f, granRoca.transform.position.z);
            // Se desactivan sus límites
            for (int j = 0; j < limitesRoca.Length; j++)
            {
                limitesRoca[j].SetActive(false);
            }
            // Se colocan como deben estar los tótems
            for(int i=0; i< totems.Length; i++)
            {
                Totems estatua= totems[i].GetComponent<Totems>();
                estatua.DefinirFinal();
                RotacionTótems rotacion = totems[i].GetComponent<RotacionTótems>();
                totems[i].transform.rotation = Quaternion.Euler(totems[i].transform.rotation.x, rotacion.posicionCorrecta, totems[i].transform.rotation.z);
            }
            SistemasJugador.controles.mapaDesbloqueado = true;
        }
    }

    public void ActivarPalancas(bool palanca1, bool palanca2, bool palanca3)
    {
        if(palanca1)
        {
            Palanca palancaA = palancas[0].GetComponent<Palanca>();
            palancaA.PalancaActivada();
        }
        if(palanca2)
        {
            Palanca palancaB = palancas[1].GetComponent<Palanca>();
            palancaB.PalancaActivada();
        }
        if (palanca3)
        {
            Palanca palancaC = palancas[2].GetComponent<Palanca>();
            palancaC.PalancaActivada();
        }
    }

    public void CargarOpciones(float musica, float sonido, float brillo, int calidad)
    {
        MenuOpciones.instancia.musica = musica;
        MenuOpciones.instancia.sonido = sonido;
        MenuOpciones.instancia.brilloValor = brillo;
        MenuOpciones.instancia.calidad = calidad;
        SistemasJugador.controles.interfaz.SetActive(false);
        MenuOpciones.instancia.AjustarSliders();
        SistemasJugador.controles.interfaz.SetActive(true);
    }

}
