using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class GirarManubrio : MonoBehaviour
{
    public Transform pivoteMano;
    public Transform pivoteDireccion;
    public InputEsferas inputEsferaDerecha;
    public InputEsferas inputEsferaIzquierda;
    public InputActionProperty btnAcelerar;
    public InputActionProperty btnFrenar;
    public string nombreMano;
    public Transform manoDerecha;
    public float valorGiro;
    public ConduccionMultiplataforma conduccionMultiplataforma;
    public Conducir conduccion;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (manoDerecha == null)
        {
            yield return new WaitForSeconds(1f);
            manoDerecha = DiccionarioPosiciones.GetData(nombreMano);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEsferaDerecha.manubrioPresionado && inputEsferaIzquierda.manubrioPresionado)
        {
            if (manoDerecha != null)
            {
                pivoteMano.position = manoDerecha.position;
                pivoteMano.localPosition = new Vector3(pivoteMano.localPosition.x, pivoteMano.localPosition.y, 0);

                pivoteDireccion.LookAt(pivoteMano);
                pivoteDireccion.localEulerAngles = Vector3.right * pivoteDireccion.localEulerAngles.x + Vector3.up * 90;

                valorGiro = ((pivoteDireccion.localEulerAngles.x > 180)? pivoteDireccion.localEulerAngles.x-360: pivoteDireccion.localEulerAngles.x) / 80f;

                conduccion.Turn(valorGiro);
                conduccionMultiplataforma.Acelerar(btnAcelerar.action.ReadValue<float>()>0.5f);
                conduccionMultiplataforma.Reversar(btnFrenar.action.ReadValue<float>()>0.5f);
            }
        }
    }
}
