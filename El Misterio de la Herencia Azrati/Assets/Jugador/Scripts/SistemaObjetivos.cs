using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaObjetivos : MonoBehaviour
{
    public TMP_Text texto;
    public AudioClip efectoSonido;
    public AudioSource reproductor;
    public string[] listaObjetivos = { "Objetivo - Observa en la dirección en la que miran los tótems", 
        "Objetivo - Investiga por la isla",
        "Objetivo - Continúa por el este de la isla",
        "Objetivo - Ve a la ubicación que indica el mapa",
        "Objetivo - Resuelve el puzle de los tótems",
        "Objetivo - Observa si se ha producido algún cambio en el templo",
        "Objetivo - Investiga la sala"
    };
    public int idObjetivoActual = -1;


    public void CambiarObjetivo(int id)
    {
        if(id != idObjetivoActual)
        {
            GestorMusica.PonerMusica(efectoSonido, reproductor, false);
            StartCoroutine(MostrarCaracteres(listaObjetivos[id]));
            idObjetivoActual=id;
        }
    }

    // Corrutina con la que se muestran los caracteres del texto uno por uno
    IEnumerator MostrarCaracteres(string frase)
    {
        texto.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            texto.text += letra;
            yield return new WaitForSeconds(0.02f);
        }
    }

}
