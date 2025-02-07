using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    public float duracionTurbo = 3f; // Duración del turbo en segundos
    public int multiplicadorVelocidad = 2; // Multiplicador de velocidad
    public int multiplicadorAceleracion = 2; // Multiplicador de aceleración

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("coche")) // Asegúrate de que el coche tiene el tag "Player"
        {
            Debug.Log("Entro");
            Conducir conducir = other.GetComponent<Conducir>();

            if (conducir != null)
            {
                StartCoroutine(ActivateTurbo(conducir));
            }
        }
    }

    private IEnumerator ActivateTurbo(Conducir car)
    {
        // Guardamos los valores originales
        int velocidadOriginal = car.maxSpeed;
        int aceleracionOriginal = car.accelerationMultiplier;

        // Aplicamos el turbo
        car.maxSpeed *= multiplicadorVelocidad;
        car.accelerationMultiplier *= multiplicadorAceleracion;

        // Esperamos la duración del turbo
        yield return new WaitForSeconds(duracionTurbo);

        // Restauramos los valores originales
        car.maxSpeed = velocidadOriginal;
        car.accelerationMultiplier = aceleracionOriginal;
    }
}
