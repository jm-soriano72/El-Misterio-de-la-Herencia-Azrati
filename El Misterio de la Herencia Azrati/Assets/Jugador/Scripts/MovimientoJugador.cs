using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Variables del jugador
    // Velocidad y correr
    public float velocidadFinal = 2f;
    public float velocidadBase;
    private float modificadorVelocidad = 1;
    public float modificadorCorrer; // Factor que indica en cuánto se incrementa el modificador de la velocidad si se corre
    // Saltos
    public float distanciaSalto;
    public float saltosMaximos;
    public float saltosActuales;
    // Movimiento
    public Vector3 direccionXZ;
    public Vector3 direccionFinal;
    private Rigidbody personaje;
    // Posición actual del personaje, para poder calcular la distancia a la que se encuentra de distintos elementos
    public Vector3 posicionActual;
    public Vector3 frente;

    private void Awake()
    {
       personaje = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        CalcularVelocidad();
        posicionActual = transform.position;
    }

    private void FixedUpdate()
    {
        Movimiento();
        posicionActual = transform.position;
        frente = Vector3.forward;
    }

    public void CalcularVelocidad()
    {
        velocidadFinal = velocidadBase * modificadorVelocidad;
    }

    public void Movimiento()
    {
        // Primero se calcula el vector normalizado con la dirección de la velocidad, obtenida a través de los controles
        // Después se multiplica por la velocidad
        direccionXZ = new Vector3(SistemasJugador.controles.ejeX, 0, SistemasJugador.controles.ejeZ).normalized * velocidadFinal;
        // Por último, se suma la componente Y al vector anterior, influido por la gravedad
        if(SistemasJugador.controles.juegoIniciado)
        {
            direccionFinal = direccionXZ + Vector3.up * SistemasJugador.sistemaGravedad.direccionY;
        }
        else
        {
            direccionFinal = Vector3.up * SistemasJugador.sistemaGravedad.direccionY;
        }
        personaje.velocity = transform.TransformDirection(direccionFinal);
        
    }

    public void Saltar()
    {
        // Si el personaje toca el suelo, se reinician los saltos a 0
        if(SistemasJugador.sistemaGravedad.enSuelo)
        {
            saltosActuales = 0;
        }
        // Si no se está en el suelo y se supera el máximo de saltos, no se salta más hasta que no se vuelve al suelo
        if(!SistemasJugador.sistemaGravedad.enSuelo&&saltosActuales>=saltosMaximos)
        {
            return;
        }
        SistemasJugador.sistemaGravedad.direccionY = Mathf.Sqrt(distanciaSalto * -2 * SistemasJugador.sistemaGravedad.gravedad);
        saltosActuales++;
    }

    public void Correr(bool corriendo)
    {
        modificadorVelocidad = corriendo ? modificadorCorrer : 1;
        // Si se corre, el modificador de la velocidad es 2, si no, es 1
        CalcularVelocidad();
    }
}
