/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSexi : MonoBehaviour
{
    public float velocidad = 2;
    public float velRotacion = 90;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (!GetComponent<MorionID>().isOwner) enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * InputAndroid.GetVeretical1()) * velocidad * Time.deltaTime);
        transform.Rotate(Vector3.up * InputAndroid.GetHorizontal1() * velRotacion * Time.deltaTime) ;
    }

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSexi : MonoBehaviour
{
    public float velocidad = 2f;
    public float velRotacion = 90f;

    public Joystick joystick; // Referencia al joystick de la UI

    //public static MovimientoSexi Instance { get; private set; }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (!GetComponent<MorionID>().isOwner) enabled = false;

        /*if (joystick == null)
        {
            Debug.LogError("Joystick no asignado en MovimientoSexi. Arrástralo en el Inspector.");
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        /*transform.Translate((Vector3.forward * InputAndroid.GetVeretical1()) * velocidad * Time.deltaTime);
        transform.Rotate(Vector3.up * InputAndroid.GetHorizontal1() * velRotacion * Time.deltaTime) ;*/
        // Obtener los valores del joystick
        float vertical = joystick.Vertical; // Movimiento hacia adelante y atrás
        float horizontal = joystick.Horizontal; // Movimiento lateral

        // Movimiento y rotación
        transform.Translate(Vector3.forward * vertical * velocidad * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontal * velRotacion * Time.deltaTime);

    }
    /*void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hace que el objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    /*public void ActivarJoystick(bool estado)
    {
        if (joystick != null)
        {
            joystick.gameObject.SetActive(estado);
            Debug.Log("Joystick " + (estado ? "Activado" : "Desactivado"));
        }
        else
        {
            Debug.LogError("No se encontró el Joystick.");
        }
    }*/
   


}