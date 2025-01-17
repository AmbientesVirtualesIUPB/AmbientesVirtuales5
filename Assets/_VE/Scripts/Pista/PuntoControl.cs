using UnityEngine;

public class PuntoControl : MonoBehaviour
{
    private void Start()
    {
        // Adherimos el punto de control a la lista
        ManagerPuntoControl.Instance.controls.Add(this);  
    }

}
