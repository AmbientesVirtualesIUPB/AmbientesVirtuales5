using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputEsferas : MonoBehaviour
{
    public string nombreMano;
    public bool manubrioPresionado;
    public InputActionProperty agarrarBoton;
    public bool agarrarPresionado;

    private void Update()
    {
        agarrarPresionado = agarrarBoton.action.ReadValue<float>() > 0.5f;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (DiccionarioPosiciones.GetData(nombreMano) != null)
        {
            if (collision.transform == DiccionarioPosiciones.GetData(nombreMano))
            {
                if(agarrarPresionado) manubrioPresionado = true;
            }
        }   
    }

    private IEnumerator OnTriggerExit(Collider collision)
    {
        if (DiccionarioPosiciones.GetData(nombreMano) != null)
        {
            if (collision.transform == DiccionarioPosiciones.GetData(nombreMano))
            {
                yield return new WaitUntil(()=>!agarrarPresionado);
                manubrioPresionado = false;
            }
        }      
    }
}
