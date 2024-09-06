using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPociones : MonoBehaviour
{
    public static GestorPociones instancia;

    // Conjunto total de pociones
    public GameObject[] totalPociones;
    // Pociones actuales
    public int[] pocionesActuales;

    private void Awake()
    {
        instancia = this;
    }

    public void EliminarPocionesExtra()
    {
        for(int i=0; i<totalPociones.Length; i++)
        {
            if (pocionesActuales[i] == 0)
            {
                Destroy(totalPociones[i]);
            }
        }
    }
}
