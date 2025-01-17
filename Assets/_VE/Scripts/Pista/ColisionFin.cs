
using UnityEngine;

public class ColisionFin : MonoBehaviour
{
    public Carrera carrera;  // Referencia al componente del objeto padre
    public ContadorTiempo contadorTiempo;  // Referencia al componente del objeto padre

    //public GameObject publico;
    public bool debugEnConsola;

    void OnTriggerEnter(Collider coche)
    {
        if (coche.transform.root.CompareTag("coche")) // Verifica que el objeto padre que colisiona tenga el tag "coche"
        {
            // Obtiene el nombre del objeto que entra en el trigger
            string objectName = coche.gameObject.name;

            // Muestra el nombre del objeto en la consola
            Debug.Log("El objeto que ha atravesado el trigger es: " + objectName);
            contadorTiempo.DetenerContador();
        }
    }
}
