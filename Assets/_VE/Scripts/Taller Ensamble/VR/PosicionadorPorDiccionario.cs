using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionadorPorDiccionario : MonoBehaviour
{
    public string nombre;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (DiccionarioPosiciones.GetData(nombre) != null)
        {
            transform.position = DiccionarioPosiciones.GetData(nombre).position;
            transform.rotation = DiccionarioPosiciones.GetData(nombre).rotation;
            transform.Translate(offset);

        }
        
    }
}
