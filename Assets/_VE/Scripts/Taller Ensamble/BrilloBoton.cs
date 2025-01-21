
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrilloBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public RectTransform botonPersonalizacion; // Referecias solo para los botones de guardado de personalizacion
    public GameObject imagenBrillo, brilloIcono; // El brillo del boton en accion, y el brillo de los iconos del panel informativo
    public Image imagenIcono, imagenIconoFurtivo; // Icono del panel informativo e icono del canvas furtivo sobre el vehiculo
    public Sprite icono; // Imagen a la que queremos cambiar los iconos
    public bool esBoton, esPanel; // Para validar si es un boton con cambio de icono, o es un boton del panel informativo
    public float valorAlfa; // Para variar el cambio del valor alfa en todos los botones
    private Vector3 posicionInicialBoton; // Para guardar la escala inicial de los botones de personalizacion

    private void Start()
    {
        if (botonPersonalizacion != null)
        {
            posicionInicialBoton = botonPersonalizacion.localScale; // Guardamos la escala inicial
        }
        
        if (imagenBrillo != null)
        {
            imagenBrillo.SetActive(false); // Nos aseguramos de que el brillo esté apagado al inicio
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imagenBrillo != null)
        {
            imagenBrillo.SetActive(true); // Activa el brillo al resaltar

            Image imagenActual = this.gameObject.GetComponent<Image>(); // Obtenemos el componenete imagen de nuestro objeto actual
            Color colorActual = imagenActual.color; // Obtenemos el color actual de la imagen
            colorActual.a = 1f; // Seteamos el alfa en 1
            imagenActual.color = colorActual; // Asignamos el nuevo color a nuestra imagen

            if (esPanel) // Validamos si es un boton perteneciente al panel informativo
            {
                brilloIcono.SetActive(true);
                Color color = imagenIcono.color; // Obtenemos el color actual de la imagen
                color.a = 1f; // Seteamos el alfa en 1
                imagenIcono.color = color;
            }

            if (botonPersonalizacion != null)
            {
                botonPersonalizacion.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (imagenBrillo != null)
        {
            imagenBrillo.SetActive(false); // Desactiva el brillo al salir  

            Image imagenActual = this.gameObject.GetComponent<Image>(); // Obtenemos el componenete imagen de nuestro objeto actual
            Color colorActual = imagenActual.color; // Obtenemos el color actual de la imagen
            colorActual.a = valorAlfa; // Seteamos el alfa en 1
            imagenActual.color = colorActual; // Asignamos el nuevo color a nuestra imagen

            if (esPanel)
            {
                brilloIcono.SetActive(false);
                Color color = imagenIcono.color; // Obtenemos el color actual de la imagen
                color.a = 0.5f; // Seteamos el alfa en 1
                imagenIcono.color = color;
            }

            if (botonPersonalizacion != null)
            {
                botonPersonalizacion.localScale = posicionInicialBoton;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (esBoton)
        {      
            imagenIcono.gameObject.SetActive(true);
            imagenIconoFurtivo.gameObject.SetActive(true);

            imagenIcono.sprite = icono;
            imagenIconoFurtivo.sprite = icono;
        }
    }

}
