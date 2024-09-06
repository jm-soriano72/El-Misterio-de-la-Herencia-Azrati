using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionTótems : MonoBehaviour, IActivable
{
    public float velocidad = 45f;
    public float rotacionDestino;
    public float posicionCorrecta;
    public float posicionActual;
    public bool mirandoCentro = false;
    public Vector3 rotacion;
    public Light luz1;
    public Light luz2;
    public AudioClip sonido;
    public AudioSource reproductor;

    // Start is called before the first frame update
    void Start()
    {
        luz1.intensity = 0f;
        luz2.intensity = 0f;
        rotacionDestino = -90f;
        CalcularRotacion();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GestorPuzleTotems.instancia.resuelto)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, rotacionDestino, transform.rotation.z), velocidad*Time.deltaTime);
            CalcularRotacion();
        }
        else
        {
            rotacionDestino = posicionCorrecta;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, rotacionDestino, transform.rotation.z), velocidad * Time.deltaTime);
            mirandoCentro =true;
        }
        EncenderLuces();
    }

    public void Activar(int indice)
    {
        GestorMusica.PonerMusica(sonido, reproductor, false);
        // Dependiendo del botón pulsado, la rotación se produce en sentido horario o antihorario
        if(indice == 1 || indice == 3 || indice == 5 || indice == 6)
        {
            rotacionDestino -= 90;
        }
        else
        {
            rotacionDestino += 90;
        }
    }

    public void CalcularRotacion()
    {
        rotacion = Quaternion.ToEulerAngles(transform.rotation);
        posicionActual = Mathf.Rad2Deg * rotacion.y;
        // Resolución del problema de conversión de grados a radianes
        if(posicionActual<0 && posicionActual > -10)
        {
            posicionActual = 0;
        }
        ComprobarPosicion();
    }

    public void ComprobarPosicion()
    {
        if(posicionActual==posicionCorrecta)
        {
            mirandoCentro = true;
        }
        else
        {
            mirandoCentro = false;
        }
    }

    public void EncenderLuces()
    {
        if(mirandoCentro)
        {
            luz1.intensity = 3;
            luz2.intensity = 3;
        }
        else
        {
            luz1.intensity = 0;
            luz2.intensity = 0;
        }
    }

}
