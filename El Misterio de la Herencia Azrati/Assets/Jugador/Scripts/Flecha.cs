using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float velocidad = 15f;
    public float limiteDistancia;
    public Vector3 direccionObjetivo;
    public Rigidbody rb;
    private float contador = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        CalcularDireccionObjetivo();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direccionObjetivo*velocidad;
        contador += Time.deltaTime;

        if(contador>=0.7f)
        {
            Destroy(gameObject);
        }
    }

    public void CalcularDireccionObjetivo()
    {
        direccionObjetivo = SistemasJugador.movimientoCamara.camara.transform.forward;       
    }
}
