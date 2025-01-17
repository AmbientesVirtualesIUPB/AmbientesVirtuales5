using TMPro;
using UnityEngine;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI  txtTiempo, tiempoFinal; // Elemento de texto desde el canvas
    private float           tiempoTranscurrido = 0f; // Para indicar el tiempo que ha pasado
    public bool             contador = false; // para controlar cuándo empieza a contar


    private void Update()
    {
        // Solo contamos si el contador está activo
        if (contador)
        {
            tiempoTranscurrido += Time.deltaTime; // incrementa el tiempo
            ActualizarContador(tiempoTranscurrido); // actualiza el texto en pantalla
        }
    }

    // Función para mostrar el tiempo en formato mm:ss
    private void ActualizarContador(float time)
    {
        int minutos = Mathf.FloorToInt(time / 60); // calcula los minutos
        int segundos = Mathf.FloorToInt(time % 60); // calcula los segundos  
        int milisegundos = Mathf.FloorToInt((time % 1) * 100); // Calcular centesimas de segundo
        txtTiempo.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos);
    }

    /// <summary>
    /// Para iniciar el contador
    /// </summary>
    public void IniciarContador()
    {
        contador = true;
        tiempoTranscurrido = 0f; // reiniciar el tiempo si queremos
    }

    /// <summary>
    /// Para detener el contador
    /// </summary>
    public void DetenerContador()
    {
        contador = false;
    }

    /// <summary>
    /// Para pausar el contador
    /// </summary>
    public void PausarContador()
    {
        contador = false;
    }

    /// <summary>
    /// Para reanudar el contador
    /// </summary>
    public void ReanudarContador()
    {
        contador = true;
    }
    
    /// <summary>
    /// Para setear el tiempo final
    /// </summary>
    public void TerminarContador()
    {
        tiempoFinal.text = txtTiempo.text;
    }
}
