using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorEscenas : MonoBehaviour
{
    public static GestorEscenas instancia;
    public GameObject panelTransicion;
    public Image transicion;

    private void Awake()
    {
        instancia = this;
    }
    
    void Update()
    {
        
    }

    public void CambiarEscena(int id)
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            panelTransicion = SistemasJugador.movimientoCamara.panelTransicion;
        }
        StartCoroutine(Transicion(id));
    }

    public void Salir()
    {
        // Salir del ejecutable del juego
        Application.Quit();
    }

    IEnumerator Transicion(int id)
    {
        panelTransicion.SetActive(true);
        transicion = panelTransicion.GetComponent<Image>();
        for(float i=0f; i<=0.8; i+=0.01f)
        {
            transicion.color = new Color(transicion.color.r, transicion.color.g, transicion.color.b, i);
            yield return new WaitForSeconds(0.003f);
        }
        SceneManager.LoadScene(id);
    }
}
