using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controles : MonoBehaviour
{
    public float ejeX, ejeZ;
    public float ratonX, ratonY;

    public GameObject interfaz;
    public GameObject carta;
    public GameObject arco;
    public GameObject mapa;
    public GameObject logros;
    public GameObject opciones;
    public GameObject iconoMapa;
    public bool juegoIniciado = false;
    public bool mapaDesbloqueado = false;

    private void Start()
    {
        opciones.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(juegoIniciado)
        {
            // Movimiento del personaje
            ejeX = Input.GetAxisRaw("Horizontal");
            ejeZ = Input.GetAxisRaw("Vertical");

            // Saltos, controlados con la barra espaciadora
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SistemasJugador.movimientoJugador.Saltar();
            }

            // Coordenadas del movimiento del ratón, para el movimiento de la cámara
            ratonX = Input.GetAxis("Mouse X");
            ratonY = Input.GetAxis("Mouse Y");

            // Correr, manejado con la tecla Shift
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                SistemasJugador.movimientoJugador.Correr(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SistemasJugador.movimientoJugador.Correr(false);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                SistemasJugador.accionesJugador.Ataque(Input.mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                carta.SetActive(true);
                
                juegoIniciado = false;
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                logros.SetActive(true);
                juegoIniciado = false;
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                opciones.SetActive(true);
                juegoIniciado = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(mapaDesbloqueado)
            {
                iconoMapa.SetActive(true);
                if(Input.GetKeyDown(KeyCode.R))
                {
                    mapa.SetActive(true);
                    juegoIniciado = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                MovimientoPlataforma plataforma = gameObject.GetComponentInParent<MovimientoPlataforma>();
                if(plataforma == null)
                {
                    GestorGuardado.instancia.Guardar();
                    SistemasJugador.sistemaDialogos.MostrarDialogo(20);
                }
                else
                {
                    SistemasJugador.sistemaDialogos.MostrarDialogo(21);
                }
            }

            if(Input.GetKeyDown(KeyCode.F1))
            {
                // De esta forma, vuelve a aparecer el cursor al cambiar de escena
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GestorEscenas.instancia.CambiarEscena(0);

            }

        }
        else
        {
            DetenerCamara();
            DetenerJugador();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                carta.SetActive(false);
                mapa.SetActive(false);
                logros.SetActive(false);
                interfaz.SetActive(true);
                arco.SetActive(true);
                opciones.SetActive(false);
                juegoIniciado = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ActivarMapa()
    {
        mapaDesbloqueado = true;
        mapa.SetActive(true);
        juegoIniciado = false;
        StartCoroutine(EsperaMapa());
    }

    IEnumerator EsperaMapa()
    {
        yield return new WaitForSeconds(5f);
        mapa.SetActive(false);
        interfaz.SetActive(true);
        arco.SetActive(true);
        juegoIniciado = true;
    }

    public void DetenerCamara()
    {
        ratonX = 0;
        ratonY = 0;
        SistemasJugador.movimientoCamara.camara.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void DetenerJugador()
    {
        SistemasJugador.movimientoJugador.direccionFinal = SistemasJugador.movimientoJugador.posicionActual;
    }
}
