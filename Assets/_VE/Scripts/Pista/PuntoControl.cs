using UnityEngine;

public class PuntoControl : MonoBehaviour
{
    private void Start()
    {
        // Adherimos el punto de control a la lista
        ManagerPuntoControl.Instance.controls.Add(this);
    }

    void OnTriggerEnter(Collider coche)
    {
        // Verifica si el collider es un Checkpoint
        if (coche.transform.root.CompareTag("coche"))
        {
            coche.transform.root.GetComponent<LapsManager>().PuntoControlAlcanzado(this.name);
        }
    }

}
