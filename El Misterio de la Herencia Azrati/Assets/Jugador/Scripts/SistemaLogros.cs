using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaLogros : MonoBehaviour
{
    public TMP_Text[] logros;
    public GameObject[] completados;
    public TMP_Text numero;
    public int[] completadosNum;
    public int logrosCompletados;
    public string[] logrosTexto = { "1- Visita el templo por primera vez",
        "2- Alcanza la zona este de la isla",
        "3- Acciona todas las palancas",
        "4- Abre el cofre",
        "5- Visita toda la isla",
        "6- Resuelve el puzle de los tótems",
        "7- Acaba con la horda de esqueletos",
        "8- Resuelve el puzle de las velas",
        "9- Recoge todas las pociones",
        "10- Completa el juego sin perder ninguna continuación",
        "11- Encuentra todos los diamantes",
        "12- Derrota a todos los esqueletos",
        "13- Completa el juego sin caer al agua",
        "14- Completa el juego usando menos de 70 flechas"
    };
    
    public void AparecerLogro(int id)
    {
        logros[id].text = logrosTexto[id];
        completadosNum[id] = 1;
    }

    public void CompletarLogro(int id)
    {
        if (completadosNum[id]!=2)
        {
            logrosCompletados++;
        }
        completados[id].SetActive(true);
        completadosNum[id] = 2;
        numero.text = logrosCompletados.ToString() + "/14";
    }
}
