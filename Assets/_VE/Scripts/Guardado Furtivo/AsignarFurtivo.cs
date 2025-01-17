using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignarFurtivo : MonoBehaviour
{
    public GameObject[] furtivos = new GameObject [4];
    public GestionFurtivos gestionFurtivos;
    // Start is called before the first frame update
    void Start()
    {
        gestionFurtivos.ObtenerPersonalizacion();
        if (furtivos[0] != null)
        {
            StartCoroutine(TraerInformacionFurtivo());
        }
        else
        {
            Debug.LogWarning("No se han inicializado los furtivos");
        }

    }
    public IEnumerator TraerInformacionFurtivo()
    {
        yield return new WaitUntil(()=>gestionFurtivos.tabla.TieneDatos()); // Esperamos hasta saber que tiene datos

        // FURTIVO 1
        Transform hijoFurtivo1 = furtivos[0].transform.GetChild(0);
        PersonalizarFurtivo personalizarFurtivo1 = hijoFurtivo1.GetComponent<PersonalizarFurtivo>();
        personalizarFurtivo1.ConvertirDesdeTexto(gestionFurtivos.tabla.furtivo1.Substring(0,11));
       
        string[] datosFurtivo1 = gestionFurtivos.tabla.furtivo1.Split("|");
        if (gestionFurtivos.tabla.furtivo1.Length > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                DatosCanvasInformativo.datosFurtivo1[i] = float.Parse(datosFurtivo1[i + 6]);
            }
        }

        // FURTIVO 2
        Transform hijoFurtivo2 = furtivos[1].transform.GetChild(0);
        PersonalizarFurtivo personalizarFurtivo2 = hijoFurtivo2.GetComponent<PersonalizarFurtivo>();
        personalizarFurtivo2.ConvertirDesdeTexto(gestionFurtivos.tabla.furtivo2.Substring(0, 11));

        string[] datosFurtivo2 = gestionFurtivos.tabla.furtivo2.Split("|");
        if (gestionFurtivos.tabla.furtivo2.Length > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                DatosCanvasInformativo.datosFurtivo2[i] = float.Parse(datosFurtivo2[i + 6]);
            }
        }

        // FURTIVO 3
        Transform hijoFurtivo3 = furtivos[2].transform.GetChild(0);
        PersonalizarFurtivo personalizarFurtivo3 = hijoFurtivo3.GetComponent<PersonalizarFurtivo>();
        personalizarFurtivo3.ConvertirDesdeTexto(gestionFurtivos.tabla.furtivo3.Substring(0, 11));

        string[] datosFurtivo3 = gestionFurtivos.tabla.furtivo3.Split("|");
        if (gestionFurtivos.tabla.furtivo3.Length > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                DatosCanvasInformativo.datosFurtivo3[i] = float.Parse(datosFurtivo3[i + 6]);
            }
        }

        // FURTIVO 4
        Transform hijoFurtivo4 = furtivos[3].transform.GetChild(0);
        PersonalizarFurtivo personalizarFurtivo4 = hijoFurtivo4.GetComponent<PersonalizarFurtivo>();
        personalizarFurtivo4.ConvertirDesdeTexto(gestionFurtivos.tabla.furtivo4.Substring(0, 11));

        string[] datosFurtivo4 = gestionFurtivos.tabla.furtivo4.Split("|");
        if (gestionFurtivos.tabla.furtivo4.Length > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                DatosCanvasInformativo.datosFurtivo4[i] = float.Parse(datosFurtivo4[i + 6]);
            }
        }
    }
}
