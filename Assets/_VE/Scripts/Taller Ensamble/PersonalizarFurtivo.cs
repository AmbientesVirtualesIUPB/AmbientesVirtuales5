using System.Linq;
using UnityEngine;

public class PersonalizarFurtivo : MonoBehaviour
{
    [Header("Baterias")]
    [Space(10)]
    public GameObject   baterias; // Referencia a las baterias para su personalizacion

    // Referencias de objetos hijos
    [Header("Mesh Filters Base")]
    [Space(10)]
    public MeshFilter   carroceriaMeshFilter;
    public MeshFilter   aleronMeshFilter;
    public MeshFilter   sillaMeshFilter;
    public MeshFilter   volanteMeshFilter;
    public MeshFilter   llantaMeshFilterDIzq;
    public MeshFilter   llantaMeshFilterDDer;
    public MeshFilter   llantaMeshFilterTrasera;

    [Header("Mesh Renderer Base")]
    [Space(10)]
    public MeshRenderer carroceriaRenderer;
    public MeshRenderer aleronRenderer;
    public MeshRenderer sillaRenderer;
    public MeshRenderer volanteRenderer;
    public MeshRenderer llantaRendererDIzq;
    public MeshRenderer llantaRendererDDer;
    public MeshRenderer llantaRendererTrasera;

    // Arrays para las opciones de personalización
    [Header("Mesh Filters Para Personalizar")]
    [Space(10)]
    public MeshFilter[] carroceriaMeshes;
    public MeshFilter[] aleronMeshes;
    public MeshFilter[] sillaMeshes;
    public MeshFilter[] volanteMeshes;
    public MeshFilter[] llantaMeshesDelanteraIzquierda;
    public MeshFilter[] llantaMeshesDelanteraDerecha;
    public MeshFilter[] llantaMeshesTrasera;

    [Header("Materiales Para Personalizar")]
    [Space(10)]
    public Material[]   carroceriaMaterials;
    public Material[]   cabinaMaterials;
    public Material[]   llantaMaterials;

    // Índices actuales para cada parte
    [HideInInspector]
    public int          carroceriaIndex = 0;
    [HideInInspector]
    public int          aleronIndex = 0;
    private int         sillaIndex = 0;
    private int         volanteIndex = 0;
    [HideInInspector]
    public int          llantaIndex = 0;
    [HideInInspector]
    public int          bateriaIndex = 0;
    [HideInInspector]
    public int[]        indiceGlobal = new int[6]; // indice global para almacenar todas los indices y tener un mejor control para el guardado

    [Header("Variables de utilidad")]
    public SaveManager  saveManager; // Objeto de guardado
    public EnvioDatosBD managerBD; //Referencia al administrador de bases de datos
    private bool        cambioCarroceria = false; // Para indicar que ya se cambio la carroceria y se deje el material por defecto en los alerones

    [Header("Gestionar Mensajes")]
    public bool         debugEnConsola; // Gestionador de mensajes

    public int idJugador;

    private void Start()
    {
        // Busca el objeto por nombre para buscar la referencia al objeto que administra la base de datos, ya que este pasara entre escenas
        GameObject obj = GameObject.Find("EnvioBD");
        
        if (obj != null) // Si es diferente de nulo
        {
            managerBD = obj.GetComponent<EnvioDatosBD>(); // Si encuentra el objeto lo almacenamos en la variable
        }
        else
        {
            managerBD = null; // Sino lo dejamos vacio
        }    

        
        if (saveManager != null) // Si es diferente de nulo
        {
            saveManager.CargarDatos(); // Cargamos los datos que se puedan tener guardados
        }
        else
        {
            if (debugEnConsola) print("Falta asignar referencia del saveData al script personalizacionFurtivo");
        }   
    }


    /// <summary>
    /// Metodo invodado desde BtnCarroceria en el CanvasPersonalizacion
    /// </summary>
    public void CambiarMeshCarroceria()
    {
        cambioCarroceria = true; // indicamos que ya se cambio la carroceria, para cambiar los alerones sin errores
        
        carroceriaMeshFilter.mesh = carroceriaMeshes[carroceriaIndex].sharedMesh; // Asignamos a la carroceria el proximo mesh en el array
        carroceriaRenderer.material = carroceriaMaterials[carroceriaIndex]; // Asignamos a la carroceria el proximo material en el array
        aleronRenderer.material = carroceriaMaterials[carroceriaIndex]; // Asignamos a los alerones el proximo material en el array
        
        carroceriaIndex++; // Aumentamos el valor del indice
        carroceriaIndex = carroceriaIndex % carroceriaMeshes.Length; // Asignamos al indice el proximo valor segun el tamaño del array
        indiceGlobal[0] = carroceriaIndex; // Asignamos a la primer posicion del indice global   

        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local
    }

    /// <summary>
    /// Metodo invodado desde BtnAlerones en el CanvasPersonalizacion
    /// </summary>
    public void CambiarMeshAleron()
    {
        if (cambioCarroceria) // Validamos si ya se ha cambiado la carroceria
        {
            aleronMeshFilter.mesh = aleronMeshes[aleronIndex].sharedMesh; // Asignamos al aleron el proximo mesh en el array

            aleronIndex++; // Aumentamos el valor del indice
            aleronIndex = aleronIndex % aleronMeshes.Length; // Asignamos al indice el proximo valor segun el tamaño del array
            indiceGlobal[1] = aleronIndex; // Asignamos a la segunda posicion del indice global
        }
        else
        {
            aleronMeshFilter.mesh = aleronMeshes[aleronIndex].sharedMesh; // Asignamos al aleron el proximo mesh en el array
            aleronRenderer.material = carroceriaMaterials.LastOrDefault(); // Asignamos al aleron el ultimo valor de los materiales, si esta vacio el array asigna null

            aleronIndex++; // Aumentamos el valor del indice
            aleronIndex = aleronIndex % aleronMeshes.Length; // Asignamos al indice el proximo valor segun el tamaño del array
            indiceGlobal[1] = aleronIndex; // Asignamos a la segunda posicion del indice global
        }

        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local
    }

    /// <summary>
    /// Metodo invodado desde BtnSillas en el CanvasPersonalizacion
    /// </summary>
    public void CambiarMeshSilla()
    {
        sillaMeshFilter.mesh = sillaMeshes[sillaIndex].sharedMesh; // Asignamos a la silla el proximo mesh en el array
        sillaRenderer.material = cabinaMaterials[sillaIndex]; // Asignamos a la silla el proximo material en el array

        sillaIndex++; // Aumentamos el valor del indice
        sillaIndex = sillaIndex % sillaMeshes.Length; // Asignamos al indice el proximo valor segun el tamaño del array
        indiceGlobal[2] = sillaIndex; // Asignamos a la tercer posicion del indice global

        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local
    }

    /// <summary>
    /// Metodo invodado desde BtnVolante en el CanvasPersonalizacion
    /// </summary>
    public void CambiarMeshVolante()
    {
        volanteMeshFilter.mesh = volanteMeshes[volanteIndex].sharedMesh; // Asignamos al volante el proximo mesh en el array
        volanteRenderer.material = cabinaMaterials[volanteIndex]; // Asignamos al volante el proximo material en el array

        volanteIndex++; // Aumentamos el valor del indice
        volanteIndex = volanteIndex % volanteMeshes.Length; // Asignamos al indice el proximo valor segun el tamaño del array
        indiceGlobal[3] = volanteIndex; // Asignamos a la cuarta posicion del indice global

        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local
    }

    /// <summary>
    /// Metodo invodado desde BtnLlantas en el CanvasPersonalizacion
    /// </summary>
    public void CambiarMeshLlanta()
    {
        // Llanta Delantera Izquierda
        llantaMeshFilterDIzq.mesh = llantaMeshesDelanteraIzquierda[llantaIndex].sharedMesh; // Asignamos a las llantas el proximo mesh en el array
        llantaRendererDIzq.material = llantaMaterials[llantaIndex]; // Asignamos a las llantas el proximo material en el array

        // Llanta Delantera Derecha
        llantaMeshFilterDDer.mesh = llantaMeshesDelanteraDerecha[llantaIndex].sharedMesh; 
        llantaRendererDDer.material = llantaMaterials[llantaIndex]; 

        // Llanta Trasera
        llantaMeshFilterTrasera.mesh = llantaMeshesTrasera[llantaIndex].sharedMesh; 
        llantaRendererTrasera.material = llantaMaterials[llantaIndex]; 
        
        llantaIndex++; // Aumentamos el valor del indice
        llantaIndex = llantaIndex % llantaMeshesDelanteraIzquierda.Length; // Asignamos al indice el proximo valor segun el tamaño del array
        indiceGlobal[4] = llantaIndex; // Asignamos a la quinta posicion del indice global

        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local
    }



    /// <summary>
    /// Metodo invodado desde BtnAlerones en el CanvasPersonalizacion
    /// </summary>
    public void CambiarBateria()
    {
        //Si el indice es mayor a 2 regresamos el numero de la bateria al valor por defecto
        if (bateriaIndex > 2)
        {
            bateriaIndex = 0;
        }
        
        indiceGlobal[5] = bateriaIndex; // Asignamos a la quinta posicion del indice global

        // Recorremos al array para encontrar cada parte
        for (int i = 1; i < 3; i++)
        {
            // Recorrer todos los hijos del objeto actual
            foreach (Transform child in baterias.transform)
            {
                // Verificar si el nombre del hijo empieza con un patrón específico
                if (child.gameObject.name.StartsWith("ba" + bateriaIndex)) // En este caso buscamos activar las baterias
                {
                    // Activar el objeto hijo que cumple la condición
                    child.gameObject.SetActive(true);
                }
                else
                {
                    // Desactivamos el objeto hijo que no cumple la condición
                    child.gameObject.SetActive(false);
                }
            }
        }
        ConvertirATexto(); // Convertimos a texto para su posterior guardado en local  
        bateriaIndex++; // Aumentamos el valor del indice
    }

    /// <summary>
    /// Para convertir las posiciones en una sola cadena de texto y poder guardarlas en el local
    /// </summary>
    /// <returns> Devuelve una variable tipo String con el texto convertido </returns>
    public string ConvertirATexto()
    {
        string texto = "";
        for (int i = 0; i < indiceGlobal.Length; i++)
        {
            texto += (texto.Length > 0) ? "|" : "";
            texto += indiceGlobal[i].ToString();
        }
        // Enviamos los datos que queremos guardar
        saveManager.PersonalizacionFurtivos(texto);
        PlayerPrefs.SetString("Furtivo", texto);
        return texto;
    }

    /// <summary>
    /// Metodo invocado desde el script SaveManager. Para convertir las posiciones de texto a enteros y cargarlas desde los archivos locales
    /// </summary>
    /// <param name="texto"> Parametro que recibe la cadena con el texto con el fin de convertirla en datos enteros </param>
    public void ConvertirDesdeTexto(string texto)
    {
        string[] datos = texto.Split("|");
        if (texto.Length > 0)
        {
            for (int i = 0; i < datos.Length; i++)
            {
                indiceGlobal[i] = int.Parse(datos[i]);
            }
        }
        // Asignamos los datos guardados localmente
        AsignarDatosGuardados();
    }

    /// <summary>
    /// Metodo que se invoca en el script de SaveManager para asignar los datos del furtivo loggeado
    /// </summary>
    /// <param name="array"></param>
    public void TraerInformacionBD(int[] array)
    {
        for (int i = 0; i < indiceGlobal.Length; i++)
        {
            indiceGlobal[i] = array[i + 15];
        }
        // Asignamos los datos guardados
        AsignarDatosGuardados();
    }

    /// <summary>
    /// Metodo utilizado para la asignacion de los valores guardados en el script SaveManager
    /// </summary>
    public void AsignarDatosGuardados()
    {
        // Índices actuales para cada parte Restamos uno a la cantidad guardada para evitar el aumento que se realiza en cada metodo
        carroceriaIndex = indiceGlobal[0] == 0 ? 8 : indiceGlobal[0] - 1;
        aleronIndex = indiceGlobal[1] == 0 ? 5 : indiceGlobal[1] - 1;
        sillaIndex = indiceGlobal[2] == 0 ? 8 : indiceGlobal[2] - 1;
        volanteIndex = indiceGlobal[3] == 0 ? 8 : indiceGlobal[3] - 1;
        llantaIndex = indiceGlobal[4] == 0 ? 8 : indiceGlobal[4] - 1;
        bateriaIndex = indiceGlobal[5] == 0 ? 0 : indiceGlobal[5];
        
        // Ejecutamos todos los metodos para cargas los elementos de la personalizacion
        CambiarMeshCarroceria();
        CambiarMeshAleron();
        CambiarMeshSilla();
        CambiarMeshVolante();
        CambiarMeshLlanta();
        CambiarBateria();
    }
}
