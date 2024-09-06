using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Camera camara;
    public GameObject panelTransicion;
    public float sensibilidad;
    public float rotacionX, rotacionY;
    // Start is called before the first frame update
    void Start()
    {
        // De esta manera no se ve el cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    // Update is called once per frame
    void Update()
    {
        RotacionCamara();
    }

    public void RotacionCamara()
    {
            rotacionX -= SistemasJugador.controles.ratonY * sensibilidad; // Se multiplica por la sensibilidad de la cámara
            // Clamp crea un límite al que se ajusta la variable que se indica, en este caso, para que no pueda girar la cabeza hacia arriba y dar una vuelta completa
            rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);
            camara.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);

            rotacionY += SistemasJugador.controles.ratonX;
            transform.localRotation = Quaternion.Euler(0, rotacionY, 0);
        
    }
}
