using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosControl : MonoBehaviour
{
    public bool activado;
    public int indice;
    public float rango;
    public SphereCollider colision;

    private void Awake()
    {
        colision = GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        colision.radius = rango;
    }

    public void Activar()
    {
        activado = true;
        if(indice==1)
        {
            GestorGuardado.instancia.guardado.activables.puntoControl2 = true;
        }
        if(indice==2)
        {
            GestorGuardado.instancia.guardado.activables.puntoControl3 = true;
        }
    }
}
