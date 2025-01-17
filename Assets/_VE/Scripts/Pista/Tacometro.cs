
using UnityEngine;

public class Tacometro : MonoBehaviour
{
    public Conducir     conducir; // Referencia al script, este lo tiene el furtivo
    public Transform    aguja; // Referencia al transfor de la aguja en la interfaz
    float               pdt;
    float               t;
    public bool         debugEnConsola; // Gestionador de mensajes

    public void InicializarComponentesTacometro(GameObject obj)
    {
        conducir = obj.GetComponent<Conducir>();
    }

    private void FixedUpdate()
    {
        if (conducir != null && aguja != null)
        {
            // Calculamos la velocidad a la cual se mueve el furtivo para actualizar la informacion del tacometro y mover su aguja
            t = Mathf.Lerp(t, Mathf.Abs(conducir.carSpeed / 100), 0.05f);
            pdt = (1 - t) * 220 + t * 76;
            aguja.localEulerAngles = Vector3.forward * pdt;
        }else
        {
            if (debugEnConsola) print("Falta inicializar componenetes del tacometro");
        }
    }
}
