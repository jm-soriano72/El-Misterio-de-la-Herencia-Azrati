using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorEsqueletos : MonoBehaviour
{
    public static GestorEsqueletos instancia;

    // Conjunto total de esqueletos
    public GameObject[] esqueletosTotales;
    // Diamantes actuales
    public int[] esqueletosActuales;

    private void Awake()
    {
        instancia = this;
    }

    public void EliminarEsqueletosActuales()
    {
        for(int i=0; i<esqueletosTotales.Length; i++)
        {
            // Si hay un 0 en el array, quiere decir que ese esqueleto ya ha sido eliminado anteriormente
            if (esqueletosActuales[i]==0)
            {
                Destroy(esqueletosTotales[i]);
            }
        }
    }
}
