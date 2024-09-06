using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruirAlCargar : MonoBehaviour
{
    private void Awake()
    {
        // Mediante esta funci�n, se evita que al cambiar de escena el objeto que lleve este script se elimina
        DontDestroyOnLoad(gameObject);
    }
}
