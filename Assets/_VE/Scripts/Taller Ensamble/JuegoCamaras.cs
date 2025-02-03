using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoCamaras : MonoBehaviour
{
    public Transform camaraPrincipal;
    public Transform[] puntosDeDestinoCamara; // Puntos de referencia para la camara

    public float velocidadDeMovimiento = 5f; // Velocidad con la que la cámara se mueve
    private int indiceDestino = 0; // Índice para determinar el punto de destino actual 1 = silla, 2 = volante, 3 = bateria

    void Update()
    {
        // Mover la cámara de forma fluida hacia el punto de destino
        if (indiceDestino != 0)
        {
            camaraPrincipal.gameObject.SetActive(true);
            camaraPrincipal.transform.position = Vector3.Lerp(camaraPrincipal.transform.position, puntosDeDestinoCamara[indiceDestino].position, Time.deltaTime * velocidadDeMovimiento);
            camaraPrincipal.transform.rotation = Quaternion.Lerp(camaraPrincipal.transform.rotation, puntosDeDestinoCamara[indiceDestino].rotation, Time.deltaTime * velocidadDeMovimiento); // Opcional, si quieres suavizar la rotación también
        }
        else
        {
            camaraPrincipal.gameObject.SetActive(false);
        }
    }

    public void ActualizarIndice(int id)
    {
        indiceDestino = id;
    }
}
