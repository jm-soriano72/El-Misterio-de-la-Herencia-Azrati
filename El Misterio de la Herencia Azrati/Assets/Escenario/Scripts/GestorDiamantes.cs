using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDiamantes : MonoBehaviour
{
    public static GestorDiamantes instancia;

    // Conjunto total de diamantes
    public GameObject[] totalDiamantes;
    // Diamantes actuales
    public int[] diamantesActuales;

    private void Awake()
    {
        instancia = this;
    }

    public void EliminarDiamantesExtra()
    {
        for(int i=0; i<totalDiamantes.Length; i++)
        {
            // Si hay un 0 en el array, significa que ya se recogió anteriormente, por lo que se debe destruir
            if (diamantesActuales[i]==0)
            {
                Destroy(totalDiamantes[i]);
            }
        }
    }
}
