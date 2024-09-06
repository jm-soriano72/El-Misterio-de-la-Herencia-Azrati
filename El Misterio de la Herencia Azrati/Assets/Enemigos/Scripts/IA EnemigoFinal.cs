using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigoFinal : MonoBehaviour
{
    // VERSIÓN SIMPLIFICADA DEL ENEMIGO ORIGINAL, ESTE SOLO TE PERSIGUE
    NavMeshAgent agente;
    public Vector3 direccionActual;

    public SphereCollider colisionAtaque, colisionDeteccion;
    public float rangoAtaque, rangoDeteccion;

    // Animador
    public Animator animador;
    public float velocidadAndar;
    public float velocidadPerseguir;


    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }



    // Start is called before the first frame update
    void Start()
    {
        colisionAtaque.radius = rangoAtaque;
        colisionDeteccion.radius = rangoDeteccion;
        agente.speed = velocidadPerseguir; // Hacemos que la velocidad base sea de andar
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
    }

    public void Perseguir()
    {
        animador.SetFloat("Velocidad", agente.velocity.magnitude);
        // Si hay un objetivo, el enemigo busca perseguirlo
        direccionActual = SistemasJugador.movimientoJugador.posicionActual;
        agente.destination= direccionActual;
        
    }

    public void Atacar()
    {
        SistemasJugador.estadisticasPersonaje.ModificarVida(10);
    }
}
