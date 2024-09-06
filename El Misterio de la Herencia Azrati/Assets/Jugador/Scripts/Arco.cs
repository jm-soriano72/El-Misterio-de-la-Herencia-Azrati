using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public static Arco instancia;
    public Vector3 objetivo;
    public Vector3 posicion;

    private void Awake()
    {
        instancia = this;
    }

    // Update is called once per frame
    void Update()
    {
        objetivo = transform.forward;
        posicion = transform.position;
    }
}
