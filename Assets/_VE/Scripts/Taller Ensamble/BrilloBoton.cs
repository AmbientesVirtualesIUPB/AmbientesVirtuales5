
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrilloBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject imagenBrillo; // Asigna la imagen del brillo
    public GameObject imagenIcono, imagenIconoFurtivo;
    public Sprite icono; 
    public bool esBoton;

    [HideInInspector]
    public bool botonPresionado;

    private void Start()
    {
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
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (imagenBrillo != null)
        {
            imagenBrillo.SetActive(false); // Desactiva el brillo al salir  
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (imagenIcono != null)
        {
            imagenIcono.SetActive(true);
            imagenIconoFurtivo.SetActive(true);

            Image image = imagenIcono.GetComponent<Image>();
            Image image2 = imagenIconoFurtivo.GetComponent<Image>();

            if (image != null)
            {
                image.sprite = icono;
                image2.sprite = icono;
            }
            else
            {
                Debug.LogError("El GameObject no tiene un componente Image.");
            }
        }
    }

}
