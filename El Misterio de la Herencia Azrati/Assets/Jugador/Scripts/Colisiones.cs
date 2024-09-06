using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    // Informaci�n al interactuar por primera vez con alg�n objeto del entorno
    public bool llegada1 = false;
    public bool diamante = false;
    public bool pocion = false;
    public bool esqueleto = false;
    public bool final = false;
    public bool caida = false;
    public bool dialogoVelas = false;
    private bool granRoca = false;

    private float contadorArboles = 0f;
    public float maxTiempoArboles = 2.5f;
    private bool enArbol = false;
    private bool subida = false;
    private float ultimoTiempo;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            SistemasJugador.sistemaGravedad.enSuelo = true;
            transform.parent = collision.transform;
            transform.parent = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Colisi�n con las plataformas
        // Al chocar con ellas, el personaje se mueve junto a ellas
        if(collision.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = collision.transform;
            // Nos aseguramos de que el personaje est� en el suelo
            SistemasJugador.sistemaGravedad.enSuelo = true;
        }
        // Colisi�n con el terreno de la isla, para asegurar que se pueda saltar siempre
        if (collision.gameObject.CompareTag("Terreno"))
        {
            SistemasJugador.sistemaGravedad.enSuelo = true;
            transform.parent = collision.transform;
            transform.parent = null;
        }
        // Colisi�n con las nubes, asegur�ndose de que el personaje se encuentre en el suelo
        if (collision.gameObject.CompareTag("Nube"))
        {
            SistemasJugador.sistemaGravedad.enSuelo = true;
        }

        // Colisi�n con los pinchos
        // Al chocar con ellos, el personaje pierde 10 puntos de vida
        if (collision.gameObject.CompareTag("Pincho"))
        {
            SistemasJugador.estadisticasPersonaje.ModificarVida(10);
            SistemasJugador.sistemaGravedad.enSuelo = true;
        }

        // Colisi�n con el mar
        // Al caer en �l, el personaje pierde vida y reaparece en el punto de control m�s cercano
        Mar oceano = collision.gameObject.GetComponent<Mar>();
        if(oceano != null)
        {
            oceano.Caer();
            caida = true;
        }

        if (collision.gameObject.CompareTag("Arbol"))
        {
            if (!subida)
            {
                SistemasJugador.sistemaDialogos.MostrarDialogo(26);
            }
            enArbol = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Salir de la plataforma
        // Al chocar con ellas, el personaje se mueve junto a ellas
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Arbol"))
        {
            enArbol = false;
            contadorArboles = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Colisi�n con un un enemigo est�ndar, que causa da�o al personaje
        // Si el objeto con el que se colisiona contiene el componente IA, que es caracter�stico de los enemigos, el personaje se define como el objetivo de los enemigos
        IAEnemigo enemigo = other.gameObject.GetComponent<IAEnemigo>();
        if (enemigo != null)
        {
            // Si la colisi�n detectada coincide con la definida como colisi�n detecci�n, se define al personaje como el objetivo a perseguir del enemigo
            if (enemigo.colisionDeteccion == other)
            {
                enemigo.DefinirObjetivo(gameObject);
            }
            // Si la colisi�n detectada coincide con la definida como colisi�n ataque, se ataca al personaje
            if (enemigo.colisionAtaque == other)
            {
                enemigo.Atacar(gameObject);
                if(!esqueleto)
                {
                    esqueleto = true;
                    SistemasJugador.sistemaDialogos.MostrarDialogo(16);
                    SistemasJugador.sistemaLogros.AparecerLogro(11);
                }
            }
        }

        // Colisi�n con los enemigos finales
        IAEnemigoFinal enemigoFinal = other.gameObject.GetComponent<IAEnemigoFinal>();
        if (enemigoFinal != null)
        {
            if (enemigoFinal.colisionAtaque == other)
            {
                enemigoFinal.Atacar();
            }
        }

        // Colisi�n con los puntos de control, para activarlos
        PuntosControl control = other.gameObject.GetComponent<PuntosControl>();
        if(control != null)
        {
            control.Activar();
        }

        // Colisi�n al intentar ir a la nueva zona este
        if(other.gameObject.CompareTag("Ruinas"))
        {
            other.gameObject.SetActive(false);
            GestorGuardado.instancia.guardado.activables.zonaRuinas = false;
            Debug.Log("Parece que la marea se ha llevado los restos que obstaculizaban el paso. Ahora s� puedo continuar por aqu�.");
            SistemasJugador.sistemaDialogos.MostrarDialogo(8);
            StartCoroutine(EsperaObjetivo(2));
        }

        // Colisi�n al acercarse a la gran roca
        if (other.gameObject.CompareTag("Gran Roca"))
        {
            ElementoInteractuable roca = other.gameObject.GetComponent<GranRoca>();
            if(!roca.interactuado)
            {
                Debug.Log("Qu� extra�a es esta formaci�n rocosa. Impide el paso completamente hacia el oeste de la isla.");
                SistemasJugador.sistemaDialogos.MostrarDialogo(1);
            }
            else
            {
                if(!granRoca)
                {
                    Debug.Log("Seguramente alg�n mecanismo ha hecho que la formaci�n rocosa descendiera permitiendo el paso... El nivel de tecnolog�a de la civilizaci�n que habit� esta isla es incre�ble");
                    SistemasJugador.sistemaDialogos.MostrarDialogo(2);
                    granRoca = true;
                }
            }
        }

        // Colisi�n al llegar al callej�n sin salida
        if(other.gameObject.CompareTag("Sin Salida") && SistemasJugador.sistemaObjetivos.idObjetivoActual == 0)
        {
            SistemasJugador.sistemaDialogos.MostrarDialogo(12);
        }

        // Colisi�n al llegar a la zona de plataformas
        if(!llegada1 && other.gameObject.CompareTag("Zona Plataformas"))
        {
            SistemasJugador.sistemaDialogos.MostrarDialogo(13);
            llegada1 = true;
            SistemasJugador.sistemaLogros.CompletarLogro(1);
            StartCoroutine(EsperaObjetivo(7));
        }

        // Colisi�n con el primer diamante
        Diamante diamant = other.gameObject.GetComponent<Diamante>();
        if(!diamante && diamant!=null)
        {
            SistemasJugador.sistemaDialogos.MostrarDialogo(15);
            SistemasJugador.sistemaLogros.AparecerLogro(10);
            diamante = true;
        }

        // Colisi�n con la primera poci�n
        Pocion curar = other.gameObject.GetComponent<Pocion>();
        if(!pocion && curar!=null)
        {
            SistemasJugador.sistemaDialogos.MostrarDialogo(24);
            SistemasJugador.sistemaLogros.AparecerLogro(8);
            pocion = true;
        }

        // Colisi�n al llegar a la zona final de generaci�n de enemigos
        GestorEnemigosFinal gestor = other.gameObject.GetComponent<GestorEnemigosFinal>();
        if(gestor!=null)
        {
            if(GestorEnemigosFinal.instancia.activado)
            {
                GestorEnemigosFinal.instancia.generar = true;
                if (!final)
                {
                    SistemasJugador.sistemaDialogos.MostrarDialogo(22);
                    final = true;
                }
            }
        }

        if(other.gameObject.CompareTag("Puzle")&&!dialogoVelas)
        {
            dialogoVelas = true;
            SistemasJugador.sistemaDialogos.MostrarDialogo(25);
        }

        if (other.gameObject.CompareTag("Arbol"))
        {
            if (!subida)
            {
                SistemasJugador.sistemaDialogos.MostrarDialogo(26);
            }
            contadorArboles = ultimoTiempo;
            enArbol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Colisi�n al salir de la zona final de generaci�n de enemigos
        GestorEnemigosFinal gestor = other.gameObject.GetComponent<GestorEnemigosFinal>();
        if (gestor != null)
        {
            if (GestorEnemigosFinal.instancia.activado)
            {
                GestorEnemigosFinal.instancia.generar = false;
            }
        }
        if (other.gameObject.CompareTag("Arbol"))
        {
            enArbol = false;
            ultimoTiempo = contadorArboles;
            contadorArboles = 0f;
        }
    }

    IEnumerator EsperaObjetivo (int id)
    {
        yield return new WaitForSeconds(3f);
        SistemasJugador.sistemaObjetivos.CambiarObjetivo(id);
    }

    private void Update()
    {
        if(enArbol)
        {
            contadorArboles += Time.deltaTime;
        }
        if(contadorArboles >= maxTiempoArboles)
        {
            contadorArboles = 0f;
            ultimoTiempo = 0f;
            GestorReaparicion.instancia.Reaparicion();
            SistemasJugador.estadisticasPersonaje.ModificarVida(20);
            if(!subida)
            {
                StartCoroutine(EsperaMuerte());
                maxTiempoArboles = 1f;
                subida = true;
            }
            else
            {
                SistemasJugador.sistemaDialogos.MostrarDialogo(27);
            }
        }
    }

    IEnumerator EsperaMuerte()
    {
        yield return new WaitForSeconds(3f);
        SistemasJugador.sistemaDialogos.MostrarDialogo(27);
    }


}
