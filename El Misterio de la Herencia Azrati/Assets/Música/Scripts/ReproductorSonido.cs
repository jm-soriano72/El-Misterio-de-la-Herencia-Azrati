using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReproductorSonido : MonoBehaviour
{
    public AudioClip clipAudio;
    private AudioSource reproductor;
    public bool ponerAlEmpezar;
    public bool debeRepetirse;

    private void Awake()
    {
        reproductor = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(ponerAlEmpezar)
        {
            PonerClip();
        }
    }

    public void PonerClip()
    {
        GestorMusica.PonerMusica(clipAudio, reproductor, debeRepetirse);
    }
}
