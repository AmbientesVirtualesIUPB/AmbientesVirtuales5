using UnityEngine;

public class LapsManager : MonoBehaviour
{
    public int              totalCheckpoints = 24;  // Número total de checkpoints en la pista
    private int             actualCheckpoint = 0; // Contador que nos indica el valor del checkpoint actual
    private int             vueltasCompletadas = 0; // Contador de vueltas, para programar que sea mas de una

    public Carrera          carrera;  // Referencia al componente del objeto padre
    public ContadorTiempo   contadorTiempo;  // Referencia al componente del objeto padre
    public Collider         colliderFinal; // Objeto collider que deseamos desactivar una vez cruce la linea de meta, el Checkpoint25
    public PistaServidor    servidor;

    public bool debugEnConsola; // Para validar si queremos imprimir en consola

    private void Start()
    {   
        if (carrera == null || contadorTiempo == null) // Si alguno de los componentes es nulo
        {
            if (debugEnConsola) print("Falta asignar componentes en el script");
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider es un Checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Creamos una variable y le asignamos el numero de checkpoint, este numero lo extraemos del nombre del objeto, utilizando replace, para quitar el nombre checkpoint y solo dejar el numero convertido a entero
            int checkpointNumero = int.Parse(other.name.Replace("Checkpoint", ""));
            
            // Solo avanza si se pasa al siguiente checkpoint en secuencia, esto para evitar que retroceda y haga trampa
            if (checkpointNumero == actualCheckpoint + 1)
            {
                actualCheckpoint = checkpointNumero; // Actualizamos el checkpoint actual
            }
        }

        // Verifica si el collider es un LineaMeta y si ya pasó por todos los checkpoint, de ser así, se da como finalizada la vuelta
        else if (other.CompareTag("LineaMeta") && actualCheckpoint == totalCheckpoints)
        {
            contadorTiempo.DetenerContador(); // Detenemos el contador de tiempo
            vueltasCompletadas++; // Aumentamos las vueltas
            actualCheckpoint = 0;  // Reiniciamos los checkpoints
            servidor.LlamadoATerminarCarrera();
            colliderFinal.enabled = false; // Desactivamos el ultimo collider
        }
    }
}
