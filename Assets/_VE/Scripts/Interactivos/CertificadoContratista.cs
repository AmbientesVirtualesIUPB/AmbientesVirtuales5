using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CertificadoContratista : MonoBehaviour
{

    public string urlContratista = "https://sicau.pascualbravo.edu.co/SICAU/Contratacion/General/CertificadosDePrestacionDeServicios";
    public bool contratista;

    public string urlAspirante = "https://sicau.pascualbravo.edu.co/SICAU/Aspirante/Aspirante/InstruccionesDeInicioDeInscripcion";
    public bool aspirante;
    private void OnMouseDown()
    {
        if (contratista)
        {
            //Activamos el objeto
            Application.OpenURL("https://sicau.pascualbravo.edu.co/SICAU/Contratacion/General/CertificadosDePrestacionDeServicios");
        }
        else if (aspirante)
        {
            //Activamos el objeto
            Application.OpenURL("https://sicau.pascualbravo.edu.co/SICAU/Aspirante/Aspirante/InstruccionesDeInicioDeInscripcion");
        }   
    }
}
