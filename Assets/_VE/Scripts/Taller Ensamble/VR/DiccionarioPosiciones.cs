using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiccionarioPosiciones : MonoBehaviour
{
    public static Dictionary<string,Transform> diccionario = new Dictionary<string, Transform>();
    public string nombre;


    public void AsignarDato(string nom, Transform pos)
    {
        if (diccionario.ContainsKey(nom))
        {
            diccionario[nom] = pos;
        }
        else
        {
            diccionario.Add(nom, pos);
        }
    }

    public static Transform GetData(string nom)
    {
        if (!diccionario.ContainsKey(nom))
        {
            return null;
        }
        return diccionario[nom];
    }


    // Update is called once per frame
    void Update()
    {
        if (nombre.Length > 0)
        {
            AsignarDato(nombre, this.transform);
        }
    }
}
