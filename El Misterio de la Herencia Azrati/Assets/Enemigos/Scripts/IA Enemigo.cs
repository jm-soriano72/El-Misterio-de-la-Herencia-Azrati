using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
    // Objetivo al que perseguirán los enemigos
    public GameObject objetivo;
    // Posiciones que el enemigo tratará de alcanzar
    public Vector3[] destinos;
    NavMeshAgent agente;
    public Vector3 direccionActual;
    private int indice = 0;
    public float temporizador;
    public float maximoTiempoQuieto;
    public bool rutaAleatoria;
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
        direccionActual = destinos[indice]; 
        // Se asigna el radio del collider según el rango
        colisionAtaque.radius = rangoAtaque;
        colisionDeteccion.radius = rangoDeteccion;
        agente.speed = velocidadAndar; // Hacemos que la velocidad base sea de andar
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
        if(objetivo!= null)
        {
            direccionActual = objetivo.transform.position;
        }
        // Para asignarle el destino al enemigo, se utiliza su agente
        agente.destination = direccionActual;
        temporizador += Time.deltaTime;
        ComprobarDistancia();
        ComprobarTiempoQuieto();
    }

    public void ComprobarDistancia()
    {
        if(Vector3.Distance(transform.position, direccionActual)<=0.3 && objetivo==null)
        {
            CambiarDestino();
        }
    }

    public void ComprobarTiempoQuieto()
    {
        if(temporizador>=maximoTiempoQuieto)
        {
            CambiarDestino();
        }
    }

    public void CambiarDestino()
    {
        agente.speed = velocidadAndar;
        if(rutaAleatoria)
        {
            indice = Random.Range(0, destinos.Length);
        }
        else
        {
            indice++;
            if(indice >= destinos.Length)
            {
                indice = 0;
            }
        }
        direccionActual = destinos[indice];
        temporizador = 0;
    }

    public void DefinirObjetivo(GameObject objetivoEntrada)
    {
        agente.speed = velocidadPerseguir;
        objetivo = objetivoEntrada;
    }

    public void Atacar(GameObject objetivoEntrada)
    {
        // Se obtiene la componente dañable del objetivo, para comprobar si lo que colisiona con el enemigo es el propio personaje. Si es así, se le reduce la vida en 10
        IDañable objetoDañable = objetivoEntrada.GetComponent<IDañable>();
        if (objetoDañable != null)
        {
            objetoDañable.ModificarVida(10);
            objetivo = null;
            CambiarDestino();
        }

    }
}
