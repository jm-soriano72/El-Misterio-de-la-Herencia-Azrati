using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorGuardado : MonoBehaviour
{
    public EstructuraGuardado guardado;
    public static GestorGuardado instancia;
    public int escenaActual;

    private void Awake()
    {
        // Si el singleton tiene riesgo de repetirse, se debe:
        if(instancia!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
        }
    }

    private void Start()
    {
        // Inicialización de los activables
        guardado.objetos.flechas = false;
        guardado.activables.puertaTemplo = true;
        guardado.activables.puntoControl2 = false;
        guardado.activables.puntoControl3 = false;
        guardado.activables.ruinasBarco = true;
        guardado.activables.zonaRuinas = false;
        guardado.activables.totemsInteractuados = false;
        guardado.activables.puertaInteractuada = false;
        guardado.activables.puzleTotems = false;
        guardado.activables.puzleTotemsResuelto = false;
        guardado.activables.puzleVelas = false;
        guardado.opciones.nivelCalidad = 5;
        guardado.opciones.brillo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        escenaActual = SceneManager.GetActiveScene().buildIndex;
    }

    public void Guardar()
    {
        // Se guardan los datos de la estructura sistemas
        guardado.sistema.posicionActual = SistemasJugador.movimientoJugador.transform.position;
        guardado.sistema.rotacion = SistemasJugador.movimientoJugador.transform.rotation;
        // Se guardan los datos de la estructura del personaje
        guardado.datosPersonaje.vidaPerdida = SistemasJugador.estadisticasPersonaje.estadisticasPersonaje.vidaPerdida;
        guardado.datosPersonaje.numDiamantes = SistemasJugador.estadisticasPersonaje.numDiamantes;
        guardado.datosPersonaje.numEsqueletosMuertos = SistemasJugador.estadisticasPersonaje.numEsqueletos;
        guardado.datosPersonaje.numVidas = SistemasJugador.estadisticasPersonaje.numUPs;
        guardado.datosPersonaje.numPociones = SistemasJugador.estadisticasPersonaje.numPociones;
        guardado.datosPersonaje.numFlechas = SistemasJugador.estadisticasPersonaje.numActualFlechas;
        guardado.datosPersonaje.numFlechasUsadas = SistemasJugador.estadisticasPersonaje.numFlechasUsadas;
        guardado.datosPersonaje.objetivo = SistemasJugador.sistemaObjetivos.idObjetivoActual;
        guardado.datosPersonaje.llegadaA = SistemasJugador.colisiones.llegada1;
        guardado.datosPersonaje.diamanteA = SistemasJugador.colisiones.diamante;
        guardado.datosPersonaje.pocionA = SistemasJugador.colisiones.pocion;
        guardado.datosPersonaje.esqueletoA = SistemasJugador.colisiones.esqueleto;
        guardado.datosPersonaje.caida = SistemasJugador.colisiones.caida;
        // Se guardan los objetos que aún quedan en la escena
        GuardarDiamantes();
        GuardarEsqueletos();
        GuardarPociones();
        GuardarLogros();
        // Crear ruta de guardado
        string ruta = Directory.GetCurrentDirectory() + "/Save";

        // Escribimos todo el texto en un archivo externo
        File.WriteAllText(ruta, JsonUtility.ToJson(guardado, true));
    }

    public void GuardarDiamantes()
    {
        for(int i=0; i<GestorDiamantes.instancia.totalDiamantes.Length;i++)
        {
            guardado.objetos.diamantes[i] = 0;
        }

        for(int i=0; i<GestorDiamantes.instancia.totalDiamantes.Length; i++)
        {
            if (GestorDiamantes.instancia.totalDiamantes[i]!=null)
            {
                guardado.objetos.diamantes[i] = 1;
            }
        }
    }

    public void GuardarEsqueletos()
    {
        for(int i = 0; i < GestorEsqueletos.instancia.esqueletosTotales.Length; i++)
        {
            guardado.objetos.esqueletos[i] = 0;
        }

        for (int i=0; i<GestorEsqueletos.instancia.esqueletosTotales.Length; i++)
        {
            if (GestorEsqueletos.instancia.esqueletosTotales[i]!=null)
            {
                guardado.objetos.esqueletos[i] = 1;
            }
        }
    }

    public void GuardarPociones()
    {
        for(int i = 0; i < GestorPociones.instancia.totalPociones.Length; i++)
        {
            guardado.objetos.pociones[i] = 0;
        }

        for (int i=0; i<GestorPociones.instancia.totalPociones.Length; i++)
        {
            if (GestorPociones.instancia.totalPociones[i]!=null)
            {
                guardado.objetos.pociones[i] = 1;
            }
        }
    }

    public void GuardarLogros()
    {
        for(int i=0; i<SistemasJugador.sistemaLogros.logros.Length; i++)
        {
            guardado.datosPersonaje.logros[i] = 0;
        }
        for(int i=0; i< SistemasJugador.sistemaLogros.logros.Length; i++)
        {
            guardado.datosPersonaje.logros[i] = SistemasJugador.sistemaLogros.completadosNum[i];
        }
    }

    public void CargarDatos()
    {
        StartCoroutine(Cargar());
        Debug.Log("Cargando");
    }

    public IEnumerator Cargar()
    {
        string ruta = Directory.GetCurrentDirectory() + "/Save";

        if(!File.Exists(ruta))
        {
            Debug.Log("No hay datos de guardado");
            yield break; // Esta instrucción rompe la corrutina, ya que no existen datos para cargar
        }
        // De esta forma, se cargan los datos del archivo externo
        guardado = JsonUtility.FromJson<EstructuraGuardado>(File.ReadAllText(ruta));

            // La operación asíncrona se ejecuta de manera paralela en otros hilos del procesador
            AsyncOperation operacionAsincrona; // En este caso, se hace de forma asíncrona el cargar la escena en la que se ejecuta el juego principal
            operacionAsincrona = SceneManager.LoadSceneAsync(1);

            while (!operacionAsincrona.isDone)
            {
                // Si la operación asíncrona no se ha llevado a cabo, es decir, la carga de la escena, no se continua con la carga
                yield return null;
            }
       
        // Se cargan los datos de la estructura sistemas
        SistemasJugador.movimientoJugador.transform.position = guardado.sistema.posicionActual;
        SistemasJugador.movimientoJugador.transform.rotation = guardado.sistema.rotacion;
        // Se cargan los datos de la estructura del personaje
        SistemasJugador.estadisticasPersonaje.estadisticasPersonaje.vidaPerdida = guardado.datosPersonaje.vidaPerdida;
        SistemasJugador.estadisticasPersonaje.numDiamantes = guardado.datosPersonaje.numDiamantes;
        SistemasJugador.estadisticasPersonaje.numEsqueletos = guardado.datosPersonaje.numEsqueletosMuertos;
        SistemasJugador.estadisticasPersonaje.numUPs = guardado.datosPersonaje.numVidas;
        SistemasJugador.estadisticasPersonaje.numPociones = guardado.datosPersonaje.numPociones;
        SistemasJugador.estadisticasPersonaje.numFlechasUsadas = guardado.datosPersonaje.numFlechasUsadas;
        SistemasJugador.estadisticasPersonaje.numActualFlechas = guardado.datosPersonaje.numFlechas;
        SistemasJugador.colisiones.llegada1 = guardado.datosPersonaje.llegadaA;
        SistemasJugador.colisiones.diamante = guardado.datosPersonaje.diamanteA;
        SistemasJugador.colisiones.pocion = guardado.datosPersonaje.pocionA;
        SistemasJugador.colisiones.esqueleto = guardado.datosPersonaje.esqueletoA;
        SistemasJugador.colisiones.caida = guardado.datosPersonaje.caida;
        // Se actualiza la interfaz del jugador
        SistemasJugador.estadisticasPersonaje.iniciado = false;
        SistemasJugador.estadisticasPersonaje.ModificarVida(0);
        SistemasJugador.estadisticasPersonaje.ActualizarTextoEsqueletos();
        SistemasJugador.estadisticasPersonaje.ActualizarTextoDiamantes();
        SistemasJugador.estadisticasPersonaje.ActualizarNumeroVidas();
        SistemasJugador.estadisticasPersonaje.ActualizarTextoFlechas();
        SistemasJugador.controles.carta.SetActive(false);
        SistemasJugador.controles.interfaz.SetActive(true);
        SistemasJugador.controles.juegoIniciado = true;
        // Se eliminan los objetos que ya se recogieron o eliminaron anteriormente
        GestorDiamantes.instancia.diamantesActuales = guardado.objetos.diamantes;
        GestorDiamantes.instancia.EliminarDiamantesExtra();
        GestorEsqueletos.instancia.esqueletosActuales = guardado.objetos.esqueletos;
        GestorEsqueletos.instancia.EliminarEsqueletosActuales();
        GestorPociones.instancia.pocionesActuales = guardado.objetos.pociones;
        GestorPociones.instancia.EliminarPocionesExtra();
        // Se coloca el objetivo adecuado
        SistemasJugador.sistemaObjetivos.CambiarObjetivo(guardado.datosPersonaje.objetivo);
        // Se reestablecen las características de las opciones
        CargarOpciones();

        // Se activan los objetos pertinentes
        GestorActivables.instancia.PuertaTemplo(guardado.activables.puertaTemplo);
        GestorActivables.instancia.PuntosDeControl(guardado.activables.puntoControl2,guardado.activables.puntoControl3);
        GestorActivables.instancia.RuinasBarco(guardado.activables.ruinasBarco, guardado.activables.zonaRuinas);
        GestorActivables.instancia.ObjetosParaRuinas(guardado.activables.puertaInteractuada, guardado.activables.totemsInteractuados);
        GestorActivables.instancia.PuzleTotemsActivado(guardado.activables.puzleTotems);
        GestorActivables.instancia.ActivarPalancas(guardado.activables.palanca1, guardado.activables.palanca2, guardado.activables.palanca3);
        GestorActivables.instancia.PuzleTotemsResuelto(guardado.activables.puzleTotemsResuelto, guardado.activables.finalDerrotado);
        GestorActivables.instancia.CargarOpciones(guardado.opciones.volumenMusica, guardado.opciones.volumenSonido, guardado.opciones.brillo, guardado.opciones.nivelCalidad);
        GestorPuzleVelas.instancia.pocionGenerada = guardado.activables.pocionGenerada;
        CargarLogros();
    }

    public void CargarLogros()
    {
        int numLogros = 0;

        for(int i=0; i<guardado.datosPersonaje.logros.Length; i++)
        {
            if (guardado.datosPersonaje.logros[i] == 0)
            {
                SistemasJugador.sistemaLogros.logros[i].text = (i+1).ToString()+"- ???";
            }
            if(guardado.datosPersonaje.logros[i] == 1)
            {
                SistemasJugador.sistemaLogros.logros[i].text = SistemasJugador.sistemaLogros.logrosTexto[i];
                SistemasJugador.sistemaLogros.completadosNum[i] = 1;
            }
            if(guardado.datosPersonaje.logros[i] == 2)
            {
                SistemasJugador.sistemaLogros.logros[i].text = SistemasJugador.sistemaLogros.logrosTexto[i];
                SistemasJugador.sistemaLogros.completados[i].SetActive(true);
                SistemasJugador.sistemaLogros.completadosNum[i] = 2;
                numLogros++;
            }
        }
        SistemasJugador.sistemaLogros.logrosCompletados = numLogros;
        SistemasJugador.sistemaLogros.numero.text = numLogros.ToString() + "/14";
    }

    public void CargarOpciones()
    {
        MenuOpciones.instancia.CambiarVolumenMusica(guardado.opciones.volumenMusica);
        MenuOpciones.instancia.CambiarVolumenSonido(guardado.opciones.volumenSonido);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            MenuOpciones.instancia.CambiarBrillo(guardado.opciones.brillo);
            MenuOpciones.instancia.CambiarCalidad(guardado.opciones.nivelCalidad);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SistemasJugador.controles.opciones.SetActive(false);
    }

}

[System.Serializable]
public class EstructuraGuardado
{
    public GuardadoDatosPersonaje datosPersonaje;
    public Sistemas sistema;
    public Objetos objetos;
    public Activables activables;
    public Opciones opciones;
}
[System.Serializable]
public struct GuardadoDatosPersonaje
{
    public float vidaPerdida;
    public int numDiamantes;
    public int numEsqueletosMuertos;
    public int numVidas;
    public int numPociones;
    public int numFlechas;
    public int numFlechasUsadas;
    public int objetivo;
    public bool llegadaA;
    public bool diamanteA;
    public bool pocionA;
    public bool esqueletoA;
    public bool caida;
    public int[] logros;
}
[System.Serializable]
public struct Sistemas
{
    public Vector3 posicionActual;
    public Quaternion rotacion;
}

[System.Serializable]
public struct Objetos
{
    public int[] diamantes;
    public int[] esqueletos;
    public int[] pociones;
    public bool flechas;
}

[System.Serializable]
public struct Activables
{
    // Todos los elementos activables a lo largo del juego
    public bool puertaTemplo;
    public bool puntoControl2;
    public bool puntoControl3;
    public bool ruinasBarco;
    public bool zonaRuinas;
    public bool totemsInteractuados;
    public bool puertaInteractuada;
    public bool puzleTotems;
    public bool puzleTotemsResuelto;
    public bool cofre;
    public bool palanca1;
    public bool palanca2;
    public bool palanca3;
    public bool puzleVelas;
    public bool finalDerrotado;
    public bool pocionGenerada;
}

[System.Serializable]
public struct Opciones
{
    public float volumenSonido;
    public float volumenMusica;
    public int nivelCalidad;
    public float brillo;
}
