using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    public float duracionTurbo = 3f; // Duración del turbo en segundos
    public int multiplicadorVelocidad = 2; // Multiplicador de velocidad
    public int multiplicadorAceleracion = 2; // Multiplicador de aceleración

    public int maxVelocidadPermitida = 150; // Límite máximo de velocidad
    public int maxAceleracionPermitida = 12; // Límite máximo de aceleración

    public float fovAumentado = 90f; // Valor máximo de Field of View
    public float fovNormal = 60f; // Valor normal del Field of View
    public float fovVelocidad = 10f; // Velocidad de cambio del Field of View


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("coche")) // Asegúrate de que el coche tiene el tag "Player"
        {
            Conducir conducir = other.GetComponent<Conducir>();
            Camera camaraTurbo = BuscarCamara(other.transform);

            if (conducir != null)
            {
                StartCoroutine(ActivarTurbo(conducir, camaraTurbo));
            }
        }
    }

    private IEnumerator ActivarTurbo(Conducir car, Camera camara)
    {
        // Aplicamos el turbo
        car.maxSpeed = Mathf.Min(car.maxSpeed * multiplicadorVelocidad, maxVelocidadPermitida);
        car.accelerationMultiplier = Mathf.Min(car.accelerationMultiplier * multiplicadorAceleracion, maxAceleracionPermitida);

        // Aumentamos el Field of View de manera gradual
        yield return StartCoroutine(CambiarFOV(camara, fovAumentado));

        // Esperamos la duración del turbo
        yield return new WaitForSeconds(duracionTurbo);

        // Restauramos los valores originales
        car.maxSpeed = car.velocidadOriginal;
        car.accelerationMultiplier = car.aceleracionOriginal;

        yield return StartCoroutine(CambiarFOV(camara, fovNormal));
    }

    private IEnumerator CambiarFOV(Camera camara, float objetivo)
    {
        float tiempo = 0f;
        float inicio = camara.fieldOfView;
        float duracion = Mathf.Abs(objetivo - inicio) / fovVelocidad; // Calcula la duración en base a la velocidad de cambio

        while (tiempo < duracion)
        {
            camara.fieldOfView = Mathf.Lerp(inicio, objetivo, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        camara.fieldOfView = objetivo; // Asegurarse de que llega exactamente al objetivo
    }

    private Camera BuscarCamara(Transform root)
    {
        // Busca en los hijos el objeto llamado "camara"
        Transform camaraTransform = root.Find("CamaraPrimeraPersona");
        if (camaraTransform != null)
        {
            Camera camara = camaraTransform.GetComponent<Camera>();
            return camara;
        }
        return null;
    }
}
