using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class EnvioDatosBD : MonoBehaviour
{
    //URL's
    [SerializeField]
    private string  url_personalizacion_furtivo = "CRUD/Create/insertar_datos_personalizacion_furtivo.php"; // URL que guardla la informacion de  personalizacion al momento de ser guardada
    [SerializeField]
    private string  url_usuario = "CRUD/Create/insertar_datos_usuario.php"; // URL para guardar la informacion de los usuarios

    // Datos de usuario, extraidos del script ConsumirApi
    public int      id_usuario; // id del usuario
    public int      tipo_usuario; // Si es docente o estudiante

    public string   datosPersonalizacion;
    public bool     debugEnConsola; // Gestionador de mensajes
    public string   datosFurtivo;

    public static EnvioDatosBD instancia;
    public string escenaACargar = "C_Taller";

    void Awake()
    {
        // Si la instancia ya existe y no es esta, destruir la nueva
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Asignar la instancia a esta y asegurarse de que no se destruya
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
    /// <summary>
    /// Metodo invocado desde el script de personalizacion y furtivo, este recibe los datos de la personalizacion
    /// </summary>
    public void EnviarDatosFurtivo(string f)
    {
        StartCoroutine(EnviarDatosPersonalizacionFurtivo(f));
    }

    /// <summary>
    /// Metodo invocado desde el script de consumirAPi, este recibe los datos del usuari y lo crea en la BD
    /// </summary>
    public void EnviarDatosU()
    {
        StartCoroutine(EnviarDatosUsuario());
    }

    /// <summary>
    /// Currutina que crea el formulario y lo envia en solicitud POST para guardar la infomracion de la personalizacion en la BD
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnviarDatosPersonalizacionFurtivo(string f)
    {
        // Creación del formulario
        WWWForm form = new WWWForm();
        form.AddField("furtivos", f);

        string url_base = ConfiguracionGeneral.configuracionDefault.url;
        // Enviar la solicitud POST
        using (UnityWebRequest www = UnityWebRequest.Post(url_base + url_personalizacion_furtivo, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Almacenamos la respuesta del servidor
                string responseText = www.downloadHandler.text;
                if (debugEnConsola) print("Respuesta del servidor: " + responseText);

                // Verificar si la respuesta contiene un mensaje de error
                if (responseText.Contains("Error"))
                {
                    if (debugEnConsola) print("Respuesta Unity: " + "Error en la solicitud: " + responseText);
                    // Acciones a realizar
                }
                else
                {
                    if (debugEnConsola) print("Respuesta Unity: " + "Datos enviados con éxito");
                }
            }
            else
            {
                if (debugEnConsola) print("Respuesta Unity: " + "Error al enviar los datos: " + www.error);
            }
            //tengo una tabla en sql con un campo llamado furtivo, necesito enviarle un string, separarlo como una cadena separada con este caracter | y que lo inserte en ese campo ya tengo un codigo php, modifica el codigo para este proceso 
        }
    }

    /// <summary>
    /// Currutina que crea el formulario y lo envia en solicitud POST para guardar la infomracion del usuario en la BD
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnviarDatosUsuario()
    {
        // Creación del formulario
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", id_usuario);
        form.AddField($"personalizacion", "{\"genero\":0,\"color1\":17,\"color2\":17,\"cuerpo\":0,\"colorPiel\":1,\"cabeza\":5,\"cabello\":2,\"colorCabello\":1,\"cejas\":2,\"zapatos\":10,\"sombrero\":0,\"accesorios\":0}");
        form.AddField($"tiempo_uso", 0);
        form.AddField($"num_conexiones", 0);
        form.AddField($"nombre", "nombre");
        form.AddField($"tipo_usuario", tipo_usuario);
        form.AddField($"programa", "programa");
        form.AddField($"facultad", "facultad");

        string url_base = ConfiguracionGeneral.configuracionDefault.url;
        // Enviar la solicitud POST
        using (UnityWebRequest www = UnityWebRequest.Post(url_base + url_usuario, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Almacenamos la respuesta del servidor
                string responseText = www.downloadHandler.text;
                if (debugEnConsola) print("Respuesta del servidor: " + responseText);

                // Verificar si la respuesta contiene un mensaje de error
                if (responseText.Contains("Error"))
                {
                    if (debugEnConsola) print("Respuesta Unity: " + "Error en la solicitud: " + responseText);
                    // Acciones a realizar
                }
                else if (responseText.Contains("El usuario ya existe en el sistema"))
                {
                    if (debugEnConsola) print("Respuesta Unity: " + "El usuario ya esta creado");
                    // Acciones a realizar
                }
                else
                {
                    if (debugEnConsola) print("Respuesta Unity: " + "Datos enviados con éxito");
                    print(responseText);
                    PersonajeBD.instance.usuario = JsonUtility.FromJson<DatosUsuario>(responseText);
                }
            }
            else
            {
                if (debugEnConsola) print("Respuesta Unity: " + "Error al enviar los datos: " + www.error);
            }
        }
    }

    /// <summary>
    /// Metodo invocado desde el script de personalizacion para traer la informacion almacenada en la base de datos
    /// </summary>
    /// <returns> Regresa la cedula del usuario logueado </returns>
    public int AsignarIdUsuario()
    {
        return id_usuario;
    }

    /// <summary>
    /// metodo y currutina para pruebas
    /// </summary>
    public void CambioScena()
    {
        StartCoroutine(CambiarEscena(escenaACargar));
    }

    public IEnumerator CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
        yield return new WaitForSeconds(1f);
    }
}
