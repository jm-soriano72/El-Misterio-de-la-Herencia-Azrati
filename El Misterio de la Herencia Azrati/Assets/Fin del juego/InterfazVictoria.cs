using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfazVictoria : MonoBehaviour
{
    public TMP_Text textoVictoria;
    public string texto1;
    public TMP_Text tituloLogros;
    public string titulo;
    public TMP_Text[] textoLogros;
    public string[] logros;
    public GameObject botones;
    public AudioSource reproductor;
    public AudioClip efectoSonido;
    public TMP_Text textoBoton;
    public string frase;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MostrarInterfaz());
    }

    IEnumerator MostrarInterfaz()
    {
        textoVictoria.text = "";
        tituloLogros.text = "";
        textoBoton.text = "";
        for(int i=0; i<textoLogros.Length; i++)
        {
            textoLogros[i].text = "";
        }
        GestorMusica.PonerMusica(efectoSonido, reproductor, true);
        foreach (char letra in texto1.ToCharArray())
        {
            textoVictoria.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        GestorMusica.QuitarMusica(reproductor);
        yield return new WaitForSeconds(0.2f);
        GestorMusica.PonerMusica(efectoSonido, reproductor, true);
        foreach (char letra in titulo.ToCharArray())
        {
            tituloLogros.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        GestorMusica.QuitarMusica(reproductor);
        yield return new WaitForSeconds(0.2f);

        for(int i=0; i<textoLogros.Length; i++)
        {
            if (GestorGuardado.instancia.guardado.datosPersonaje.logros[i]==2)
            {
                GestorMusica.PonerMusica(efectoSonido, reproductor, true);
                foreach (char letra in logros[i].ToCharArray())
                {
                    textoLogros[i].text += letra;
                    yield return new WaitForSeconds(0.04f);
                }
                GestorMusica.QuitarMusica(reproductor);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        GestorMusica.PonerMusica(efectoSonido, reproductor, true);
        foreach (char letra in frase.ToCharArray())
        {
            textoBoton.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        GestorMusica.QuitarMusica(reproductor);
        botones.SetActive(true);      


    }
}
