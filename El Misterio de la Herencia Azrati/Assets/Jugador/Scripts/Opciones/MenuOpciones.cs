using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    public static MenuOpciones instancia;
    public AudioMixer mezclador;
    public Image brillo;

    // Sliders
    public Slider sliderMusica;
    public Slider sliderSonido;
    public Slider sliderBrillo;

    // Valores
    public float musica;
    public float sonido;
    public float brilloValor;
    public int calidad;

    private void Awake()
    {
        instancia = this;
    }

    public void AjustarSliders()
    {
        sliderMusica.value = musica;
        mezclador.SetFloat("volumenMusica", musica);
        sliderSonido.value = sonido;
        mezclador.SetFloat("volumenSonido", sonido);
        sliderBrillo.value = brilloValor;
        brillo.color = new Color(brillo.color.r, brillo.color.g, brillo.color.b, 1 - brilloValor);
        QualitySettings.SetQualityLevel(calidad);
    }

    public void CambiarVolumenMusica(float volumenMusica)
    {
        musica = volumenMusica;
        mezclador.SetFloat("volumenMusica", volumenMusica);
        GestorGuardado.instancia.guardado.opciones.volumenMusica= volumenMusica;
    }

    public void CambiarVolumenSonido(float volumenSonido)
    {
        sonido = volumenSonido;
        mezclador.SetFloat("volumenSonido", volumenSonido);
        GestorGuardado.instancia.guardado.opciones.volumenSonido = volumenSonido;
    }

    public void CambiarBrillo(float intensidad)
    {
        brilloValor = intensidad;
        brillo.gameObject.SetActive(false);
        brillo.color = new Color(brillo.color.r, brillo.color.g, brillo.color.b, 1-intensidad);
        GestorGuardado.instancia.guardado.opciones.brillo = intensidad;
        brillo.gameObject.SetActive(true);
    }

    public void CambiarCalidad(int calidad)
    {
        QualitySettings.SetQualityLevel(calidad);
        GestorGuardado.instancia.guardado.opciones.nivelCalidad = calidad;
    }
}
