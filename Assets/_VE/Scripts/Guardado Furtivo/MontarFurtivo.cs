
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ConduccionMultiplataforma))]
[RequireComponent(typeof(Conducir))]
public class MontarFurtivo : MonoBehaviour
{
    public int                          id;
    public Conducir                     conducir;
    public IniciarIntefazVehiculo       iniciarInterfaz;
    public Tacometro                    tacometro;
    public Carrera                      carrera;
    public CargarBateriaPits            cargarBateriaPits;
    public MiniMapaIcono                miniMapaIcono;
    public MonoBehaviour[]              scriptsActivar;
    public GameObject[]                 objetosMixActivar;
    public GameObject[]                 objetosMixDesactivar;
    public MorionID                     morionID;
    public Rigidbody                    body;
    public Button                       button;
    public ConduccionMultiplataforma    conduccionMultiplataforma;
    public PistaServidor                pistaServidor;
    public Transform                    posicionConductor;
    public ContadorTiempo               contadorTiempo;
    public TextMeshProUGUI              txtTiempo, tiempoFinal;


    private void Start()
    {
        conduccionMultiplataforma = GetComponent<ConduccionMultiplataforma>();
        conducir = GetComponent<Conducir>();
    }
  
    /// <summary>
    /// Metodo invocado desde el boton Entrar, que se encuentra sobre los furtivos
    /// </summary>
    public void ActivarVehiculo()
    {
        pistaServidor.LlamadoASubirmeFurtivo(morionID.GetID());

        conduccionMultiplataforma.enabled = true;
        //ControlUsuarios.singleton.usuarioLocal.gameObject.SetActive(false);
        DatosCanvasInformativo.RecibirDatosParaFurtivo(id);

        conducir.enabled = true;
        conducir.descargado = true;

        if (ManejadorAndroid.instance != null)
        {
            ManejadorAndroid.instance.conduccionMultiplataforma = conduccionMultiplataforma;
        }

        for (int i = 0; i < scriptsActivar.Length; i++)
        {
            scriptsActivar[i].enabled = true;
        }

        for (int i = 0; i < objetosMixActivar.Length; i++)
        {
            objetosMixActivar[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < objetosMixDesactivar.Length; i++)
        {
            objetosMixDesactivar[i].gameObject.SetActive(false);
        }

        morionID.isOwner = true;
        body.isKinematic = false;

        iniciarInterfaz.InicializarComponentesInterfaz(this.gameObject);
        carrera.InicializarComponentesCarrera(this.gameObject);
        carrera.InicializarComponentesCarreraInterfaz(iniciarInterfaz);
        contadorTiempo.InicializarComponentesTiempo(txtTiempo, tiempoFinal);
        cargarBateriaPits.InicializarComponentesBateria(this.gameObject);
        tacometro.InicializarComponentesTacometro(this.gameObject);

        miniMapaIcono.cambioColor = true;
        miniMapaIcono.flag = true;
        AsignarBotonMetodo(this.gameObject);

        PosicionarPersonajeXR.singleton.transform.parent = posicionConductor.transform;
        PosicionarPersonajeXR.singleton.transform.localPosition = Vector3.zero;
        PosicionarPersonajeXR.singleton.transform.localEulerAngles = Vector3.zero;
        PosicionarPersonajeXR.singleton.characterController.enabled = false;

    }

    /// <summary>
    /// Asignamos al boton con el que interactuemos, el metodo de restaurar posicion
    /// </summary>
    /// <param name="targetObject"></param>
    public void AsignarBotonMetodo(GameObject targetObject)
    {
        // Limpia los listeners anteriores
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => RestaurarPosicion(targetObject));
    }

    /// <summary>
    /// Metodo incovado desde el boton VR Posicionador dentro del furtivo, para enviar nuestra posicion y rotacion
    /// </summary>
    public void RestaurarPosicion(GameObject obj)
    {
        // Posicionamos a nuestro vehiculo y su rotacion en el punto de checkPoint mas cercano
        body.MovePosition(ManagerPuntoControl.Instance.ObtenerPuntoCercano(obj.transform.position).position);
        body.MoveRotation(ManagerPuntoControl.Instance.ObtenerPuntoCercano(obj.transform.position).rotation);
    }
}
