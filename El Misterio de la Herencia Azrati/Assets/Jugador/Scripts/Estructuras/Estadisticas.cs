using UnityEngine;

[System.Serializable]

public struct Estadisticas
{
    // Todos los factores necesarios para gestionar la vida del personaje
    public float vidaActual;
    public float vidaPerdida;
    public float vidaMaxima;

    public void ModificarVida(float cantidad)
    {
        vidaPerdida += cantidad;
        CalcularVida();
    }
    public void CalcularVida()
    {
        // De esta manera se consigue que la vida actual no quede nunca mayor que la vida máxima o menor que 0
        vidaPerdida = Mathf.Clamp(vidaPerdida, 0, vidaMaxima);
        vidaActual = vidaMaxima - vidaPerdida;
    }

}


