
using UnityEngine;

public class ActivadorPublico : MonoBehaviour
{
    public GameObject publico;

    private void Start()
    {
        if ((publico!=null) && ConfiguracionGeneral.configuracionDefault.tipoGraficos == TipoGraficos.altos)
        {
            Instantiate(publico,transform);
        }
    }

    void OnTriggerEnter(Collider coche)
    {
        if (coche.transform.root.CompareTag("coche"))
        {
            // Buscar el objeto hijo de la tribuna
            Transform objetoHijo = transform.GetChild(0);  // Obtiene el primer hijo de la tribuna (�ndice 0)

            if (objetoHijo != null) // Validamos si existe el objeto
            {
                // Activar el objeto hijo
                objetoHijo.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider coche)
    {
        if (coche.transform.root.CompareTag("coche"))
        {
            // Buscar el objeto hijo de la tribuna
            Transform objetoHijo = transform.GetChild(0);  // Obtiene el primer hijo de la tribuna (�ndice 0)

            if (objetoHijo != null) // Validamos si existe el objeto
            {
                // Desactivar el objeto hijo
                objetoHijo.gameObject.SetActive(false);
            }
        }
    }
}
