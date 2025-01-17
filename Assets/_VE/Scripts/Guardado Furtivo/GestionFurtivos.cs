using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GestionFurtivos : MonoBehaviour
{
    public TablaFurtivos tabla;
    public PersonalizarFurtivo furtivo;
    public EnvioDatosBD managerBD;
    public string url = "CRUD/Read/leer_Furtivo.php"; // URL para guardar la informacion de los usuarios

    private void Start()
    {
        // Busca el objeto por nombre para buscar la referencia al objeto que administra la base de datos, ya que este pasar� entre escenas
        GameObject obj = GameObject.Find("EnvioBD");

        if (obj != null)
        {
            managerBD = obj.GetComponent<EnvioDatosBD>(); // Si encuentra el objeto lo almacenamos en la variable
        }
        else
        {
            managerBD = null;
        }
        ObtenerPersonalizacion();
    }
    public void GuardarFurtivo(int id)
    {
        StartCoroutine(Guardar(id));
    }
    bool cargados = false;
    IEnumerator Guardar(int id)
    {
        cargados = false;
        ObtenerPersonalizacion();
        yield return new WaitUntil(() => cargados);
        tabla.GuardarFurtivo(id, furtivo.ConvertirATexto() + "|" + DatosCanvasInformativo.datos);
        EnviarDatosFurtivo();
    }

    public void EnviarDatosFurtivo()
    {
        managerBD.EnviarDatosFurtivo(JsonUtility.ToJson(tabla));
    }

    [ContextMenu("traer datos")]
    public void ObtenerPersonalizacion()
    {
        StartCoroutine(ObtenerFurtivo());
    }
    IEnumerator ObtenerFurtivo()
    {
        string url_base = ConfiguracionGeneral.configuracionDefault.url;

        UnityWebRequest www = UnityWebRequest.Get(url_base + url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Furtivo recibido: " + www.downloadHandler.text);
            tabla = JsonUtility.FromJson<TablaFurtivos>(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error al obtener furtivo: " + www.error);
        }
        cargados = true;
    }
}

[System.Serializable]
public class TablaFurtivos
{
    public string furtivo1 = "";
    public string furtivo2 = "";
    public string furtivo3 = "";
    public string furtivo4 = "";

    public void GuardarFurtivo(int id, string furtivo)
    {
        Debug.Log("aqui " + furtivo);
        switch (id)
        {
            case 1:
                furtivo1 = furtivo;
                break;
            case 2:
                furtivo2 = furtivo;
                break;
            case 3:
                furtivo3 = furtivo;
                break;
            case 4:
                furtivo4 = furtivo;
                break;
            default:
                break;
        }
    }
    public bool TieneDatos()
    {
        return (furtivo1.Length + furtivo2.Length + furtivo3.Length + furtivo4.Length) > 4;
    }
}