using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentacionFurtivo : MonoBehaviour
{
    public AsignarFurtivo asignarFurtivo;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Asignar());
    }

    public IEnumerator Asignar()
    {
        yield return new WaitForSeconds(3f);
        //asignarFurtivo.AsignarVehiculos(id, this.gameObject);
        Debug.Log("f2");
    }
}
