using System.Collections;
using UnityEngine;

public class TallerServidor : MonoBehaviour
{
    public PersonalizarFurtivo  personalizarFurtivo;
    public string               datosFurtivo;

    /// <summary>
    /// Se encarga de presentar a los usuarios que ingresen al taller
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        GestionMensajesServidor.singeton.RegistrarAccion("TF00", RecibirDatos);
        GestionMensajesServidor.singeton.RegistrarAccion("TF01", EnviarPersonalizacionFurtivo);

        yield return new WaitForSeconds(0.5f);

        EnviarPersonalizacionFurtivo("");
        GestionMensajesServidor.singeton.EnviarMensaje("TF01", " ");
    }

    /// <summary>
    /// Recibe los datos del usuario a presentarse
    /// </summary>
    /// <param name="dato"> Datos del usuario presentado </param>
    public void RecibirDatos(string dato)
    {
        FurtivoPersonalizado furtivoPersonalizado = JsonUtility.FromJson<FurtivoPersonalizado>(dato);
        datosFurtivo = furtivoPersonalizado.datosFurtivo;
    }

    /// <summary>
    /// Envia la personalizacion que se realice a los usuarios que esten en el taller
    /// </summary>
    /// <param name="dato"></param>
    public void EnviarPersonalizacionFurtivo(string dato)
    {
        FurtivoPersonalizado fp = new FurtivoPersonalizado();
        fp.datosFurtivo = datosFurtivo;
        GestionMensajesServidor.singeton.EnviarMensaje("TF00", JsonUtility.ToJson(fp));
        personalizarFurtivo.ConvertirDesdeTexto(fp.datosFurtivo);
    }

    /// <summary>
    /// Metodo invocado desde los botones de personalizacion al momento de cambiar algun componente
    /// </summary>
    public void LLamadoPersonalizarFurtivo()
    {
        datosFurtivo = PlayerPrefs.GetString("Furtivo");
        EnviarPersonalizacionFurtivo("");
        GestionMensajesServidor.singeton.EnviarMensaje("TF01", " ");
    }
}
