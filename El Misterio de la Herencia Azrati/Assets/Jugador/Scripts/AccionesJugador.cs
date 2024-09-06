using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionesJugador : MonoBehaviour
{
    private Ray rayo;
    private RaycastHit hit;
    public GameObject flecha;
    public int numFlechas;
    public AudioSource reproductor;
    public AudioClip sonido;
    public AudioClip sonidoFlecha;
    public bool reproduciendo = false;

    private void Awake()
    {
        reproductor= GetComponent<AudioSource>();
    }

    public void Ataque(Vector2 posicion)
    {
        if (SistemasJugador.estadisticasPersonaje.numActualFlechas == 0) return;
        // Se genera un Raycast que comprueba si se está disparando al esqueleto
        LanzarFlecha();
        rayo.origin = transform.position;
        rayo.direction = SistemasJugador.movimientoCamara.camara.transform.forward;
        rayo = SistemasJugador.movimientoCamara.camara.ScreenPointToRay(posicion);
        if(Physics.Raycast(rayo, out hit, 4f))
        {
            if(hit.collider.CompareTag("Enemigo"))
            {
                StartCoroutine(EsperaMuerte(GameObject.Find(hit.transform.name)));
                Debug.Log("Enemigo");
            }
        }

    }

    public void LanzarFlecha()
    {
        SistemasJugador.estadisticasPersonaje.UsarFlecha();
        GestorMusica.PonerMusica(sonidoFlecha, reproductor, false);
        StartCoroutine(Tirar());
    }

    IEnumerator EsperaMuerte(GameObject enemigo)
    {
        yield return new WaitForSeconds(1f);
        SistemasJugador.estadisticasPersonaje.EsqueletoMuerto();
        if (!reproduciendo) GestorMusica.PonerMusica(sonido, reproductor, false);
        Destroy(enemigo);        
    }

    IEnumerator Tirar()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(flecha, Arco.instancia.posicion, transform.rotation);
    }

  
}
