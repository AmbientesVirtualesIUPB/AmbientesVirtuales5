using UnityEngine.UI;
using UnityEngine;

public class CargarBateriaPits : MonoBehaviour
{
    public Bateria  bateria; // Referencia a la bateria
    public Slider   slCarga; // Referencia al slider
    public Image    imgFill; //Referencia a la imagen del slider, fill
    public float    velocidadCarga = 0.2f; // Velocidad a la cual queremos realizar la carga
    private bool    cargar; // Booleano para validar si el coche colisiono y podemos cargar

    public void InicializarComponentesBateria(GameObject obj)
    {
        bateria = obj.GetComponent<Bateria>();
    }

    private void Update()
    {
        if (cargar)
        {
            // Incrementa el valor del slider progresivamente
            if (slCarga.value < 1)  // Asume que el valor máximo es 1, cambia si es necesario
            {
                slCarga.value += velocidadCarga * Time.deltaTime; // Incrementa el valor del slider basado en la velocidad de carga
                bateria.cargaActual += velocidadCarga; // Incrementa el valor de la carga actual basado en la velocidad de carga
                imgFill.color = bateria.colores.Evaluate(bateria.cargaActual / bateria.capacidadMaxima); // Damos el efecto visual a medida que realiza la carga        
            }
            if (slCarga.value >= 1)  // Asume que el valor máximo es 1, cambia si es necesario
            {
                cargar = false; // indicamos que ya termino la carga para que no se repita mas el codigo
                bateria.cargaActual = bateria.capacidadMaxima; // Establecemos la carga actual en el maximo
                bateria.enabled = true; // Habilitamos nuevamente el script de la bateria
            }
        }  
    }
    void OnTriggerEnter(Collider coche)
    {
        if (coche.transform.root.CompareTag("coche")) // Verifica que el objeto padre que colisiona tenga el tag "coche"
        {
            bateria.enabled = false; // Mientras cargamos la bateria desactivamos el script para evitar errores
            cargar = true; // indicamos que podemos cargar
        }
    }
}
