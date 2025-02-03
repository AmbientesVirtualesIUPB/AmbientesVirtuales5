using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class Carrera : MonoBehaviour
{
    // Variables de manejo de camaras, al momento de finalizar la carrera
    public GameObject[]     camaras; // Camaras entre las que queremos cambiar vistas
    public GameObject       camaraPrincipal; // Camara de conduccion
    public GameObject       camaraOrbital; // Camara de conduccion
    public float            tiempoEspera = 3.1f;   // Tiempo de espera entre activaciones
    public bool             puedoCambiarCamara; // Para validar si puedo seguir cambiando de camara al finalizar la carrera
      
    // Referencias necesarias para el control de la carrera
    public GameObject[]     luces = new GameObject[4];  // Un array para almacenar las 3 luces del semaforo, Amarilla, Verde y Roja
    public GameObject       panelFinalCarrera;
    public TextMeshProUGUI  txtInicioSombra, txtInicio, txtFinalCarreraSombra, txtFinalCarrera; // Elemento de texto desde el canvas
    public AudioSource      sonidoArranque; // Sonido para indicar el inicio de la carrera
    public Conducir         conducir;
    public Bateria          bateria;

    // Variables de manejo de flujo
    public bool             carreraIniciada;
    private bool            pausa;

    public ContadorTiempo   contadorTiempo;

    public IniciarIntefazVehiculo iniciarIntefazVehiculo;
    public GameObject numero1, numero2, numero3;

    public void InicializarComponentesCarrera(GameObject obj)
    {
        bateria = obj.GetComponent<Bateria>();
        conducir = obj.GetComponent<Conducir>();
    }
    public void InicializarComponentesCarreraInterfaz(IniciarIntefazVehiculo obj)
    {
        iniciarIntefazVehiculo = obj.GetComponent<IniciarIntefazVehiculo>();
    }

    void Update()
    {
        // Verifica si se ha presionado la tecla Escape para pausar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausa)
            {
                panelFinalCarrera.SetActive(true); // Activamos el panel de pausa
                Time.timeScale = 0; // Detenemos el tiempo de juego
                pausa = true;
            }
            else
            {
                panelFinalCarrera.SetActive(false); // Desactivamos el panel de pausa
                Time.timeScale = 1; // Reanudamos el tiempo de juego
                pausa = false;
            }
        }
    }

    /// <summary>
    /// Metodo invocado desde el scrip IniciarIntefazVehiculo, al momento de iniciar la interfaz
    /// </summary>
    public void Iniciar()
    {
        iniciarIntefazVehiculo.Iniciar();
        StartCoroutine(IniciarCarrera());
        print("iniciada");
    }

    /// <summary>
    /// Currutina encargada de iniciar la carrera
    /// </summary>
    public IEnumerator IniciarCarrera()
    {
        if (!carreraIniciada)
        {
            luces[0].gameObject.SetActive(true);
            numero3.SetActive(true);
            sonidoArranque.Play();

            yield return new WaitForSeconds(1.5f);

            luces[1].gameObject.SetActive(true);
            numero3.SetActive(false);
            numero2.SetActive(true);

            yield return new WaitForSeconds(1.3f);

            luces[2].gameObject.SetActive(true);
            numero2.SetActive(false);
            numero1.SetActive(true);

            yield return new WaitForSeconds(1.4f);
            numero1.SetActive(false);

            luces[3].gameObject.SetActive(true);
            txtInicioSombra.gameObject.SetActive(true);
            txtInicio.color = Color.green;
            txtInicio.text = "GO";
            txtInicioSombra.text = "GO";

            //Accion temporal momentanea
            contadorTiempo.contador = true;
            bateria.encendida = true; // Indicamos que la bateria esta encendida
            conducir.descargado = false; // Indicamos que el vehiculo se puede conducir, siempre y cuando tenga carga
            
            yield return new WaitForSeconds(1f);
            txtInicioSombra.gameObject.SetActive(false);
        }
        carreraIniciada = true;
    }

    /// <summary>
    /// Metodo invocado desde el scrip LapsManager si se gana la carrera
    /// </summary>
    public void ColisionFinalGanador()
    {
        txtFinalCarrera.text = "GANASTE";
        txtFinalCarreraSombra.text = "GANASTE";

        // Convertir colores hexadecimales a Color
        Color color1, color2, color3, color4;
        ColorUtility.TryParseHtmlString("#00F8FF", out color1); // Inferior izquierda
        ColorUtility.TryParseHtmlString("#00FFE3", out color2); // Inferior derecha
        ColorUtility.TryParseHtmlString("#00FF35", out color3); // Superior izquierda
        ColorUtility.TryParseHtmlString("#00FF2A", out color4); // Superior derecha

        // Crear un nuevo gradiente
        VertexGradient newGradient = new VertexGradient(
            color1,   // Color de esquina inferior izquierda
            color2,   // Color de esquina inferior derecha
            color3,   // Color de esquina superior izquierda
            color4    // Color de esquina superior derecha
        );

        // Asignar el gradiente al texto
        txtFinalCarrera.colorGradient = newGradient;
        panelFinalCarrera.gameObject.SetActive(true); // Activamos el texto
        StartCoroutine(DetenerScriptCarrera());
        pausa = true;
    }

    /// <summary>
    /// Metodo invocado desde el scrip LapsManager si se pierde la carrera
    /// </summary>
    public void ColisionFinalPerdedor()
    {
        txtFinalCarrera.text = "PERDISTE";
        txtFinalCarreraSombra.text = "PERDISTE";
        panelFinalCarrera.gameObject.SetActive(true);
        StartCoroutine(DetenerScriptCarrera());
        pausa = true;
    }

    /// <summary>
    /// Metodo invocado desde el scrip PistaServidor al terminar la carrera
    /// </summary>
    public void ColisionFinal()
    {
        txtFinalCarrera.text = "TERMINÓ";
        txtFinalCarreraSombra.text = "TERMINÓ";
        contadorTiempo.TerminarContador();
        panelFinalCarrera.gameObject.SetActive(true);
        StartCoroutine(DetenerScriptCarrera());
        pausa = true;
    }

    /// <summary>
    /// Metodo invocado desde ButtonTaller en nuestro canvas para salir de la escena 
    /// </summary>
    public void SalirScena()
    {   
        ManagerScene.instance.CargarEscenaAsincronamente("C_Taller"); // Recargamos escena
        Time.timeScale = 1; // Reanudamos el tiempo de juego
    }

    /// <summary>
    /// Metodo invocado desde ButtonReiniciar en nuestro canvas para salir de la escena 
    /// </summary>
    public void RecargarScena()
    {
        // Obtiene el nombre o índice de la escena actual y la carga nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Currutina encargada de detener la carrera
    /// </summary>
    public IEnumerator DetenerScriptCarrera()
    {
        conducir.descargado = true; // Indicamos que el vehiculo no se puede conducir
        yield return new WaitForSeconds(1f);
        conducir.enabled = false;
    }
}
