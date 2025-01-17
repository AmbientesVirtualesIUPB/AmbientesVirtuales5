using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovimientoSinoidal : MonoBehaviour
{
    public Vector3  posicionInicial;
    public float    amplitud;
    public float    frecuencia;

    private void Start()
    {
        posicionInicial = transform.localPosition;
    }

    private void Update()
    {
        // Usamos un movimiento suave que el objeto realiza de arriba a abajo 
        transform.localPosition = posicionInicial + Vector3.up * Mathf.Sin(Time.time * frecuencia) * amplitud;
    }
}
