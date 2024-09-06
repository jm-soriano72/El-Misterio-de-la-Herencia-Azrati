using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Derrota : MonoBehaviour
{
    public TMP_Text textoDerrota;
    public TMP_Text textoDerrota2;
    public string texto1;
    public string texto2;
    public GameObject botones;
    public AudioSource reproductor;
    public AudioClip efectoSonido;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MostrarInterfaz());
    }

    IEnumerator MostrarInterfaz()
    {
        textoDerrota.text = "";
        textoDerrota2.text = "";   
        GestorMusica.PonerMusica(efectoSonido, reproductor, true);
        foreach (char letra in texto1.ToCharArray())
        {
            textoDerrota.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        GestorMusica.QuitarMusica(reproductor);
        GestorMusica.PonerMusica(efectoSonido, reproductor, true);
        foreach (char letra in texto2.ToCharArray())
        {
            textoDerrota2.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        GestorMusica.QuitarMusica(reproductor);
        yield return new WaitForSeconds(0.5f);
        botones.SetActive(true);
    }
}
