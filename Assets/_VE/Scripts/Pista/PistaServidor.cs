using Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class PistaServidor : MonoBehaviour
{
    public string       datosFurtivo;
    public string       jugadorActual;
    public Carrera      carrera;
    public GameObject[] botonesSubirFurtivo;

    [HideInInspector]
    public EnvioDatosBD managerBD;

    private IEnumerator Start()
    {
        // Busca el objeto por nombre para buscar la referencia al objeto que administra la base de datos, ya que este pasar� entre escenas
        GameObject obj = GameObject.Find("EnvioBD");

        if (obj != null)
        {
            managerBD = obj.GetComponent<EnvioDatosBD>(); // Si encuentra el objeto lo almacenamos en la variable
        }
        else
        {
            managerBD = null;
        }
        jugadorActual = managerBD.id_usuario.ToString();

        GestionMensajesServidor.singeton.RegistrarAccion("PF00", RecibirDatos);
        GestionMensajesServidor.singeton.RegistrarAccion("PF01", Presentarme);
        GestionMensajesServidor.singeton.RegistrarAccion("PF02", IniciarCarrera);
        GestionMensajesServidor.singeton.RegistrarAccion("PF03", Reiniciararrera);
        GestionMensajesServidor.singeton.RegistrarAccion("PF04", Terminarrarrera);
        GestionMensajesServidor.singeton.RegistrarAccion("PF05", SubirmeFurtivo);

        yield return new WaitForSeconds(0.5f);
        datosFurtivo = PlayerPrefs.GetString("Furtivo");

        Presentarme("");
        GestionMensajesServidor.singeton.EnviarMensaje("PF01", " ");
    }

    /// <summary>
    /// recibe los datos del furtivo que se esta presentando
    /// </summary>
    /// <param name="dato"> Furtivo a presentarse</param>
    public void RecibirDatos(string dato)
    {
        FurtivoPersonalizado furtivoPersonalizado = JsonUtility.FromJson<FurtivoPersonalizado>(dato);

        if (furtivoPersonalizado.numeroJugador == jugadorActual)
        {
            datosFurtivo = furtivoPersonalizado.datosFurtivo;
        }
        else
        {
            print($"Jugador no reconocido: {furtivoPersonalizado.numeroJugador}");
        }
    }

    /// <summary>
    /// Presenta a cada usuario que inicia en el servidor
    /// </summary>
    /// <param name="dato"> Datos del furtivo presentado </param>
    public void Presentarme(string dato)
    {
        FurtivoPersonalizado fp = new FurtivoPersonalizado();
        fp.numeroJugador = jugadorActual;
        fp.datosFurtivo = datosFurtivo;
        GestionMensajesServidor.singeton.EnviarMensaje("PF00", JsonUtility.ToJson(fp));
    }

    /// <summary>
    /// Metodo de servidor para iniciar la carrera en terminos generales
    /// </summary>
    /// <param name="dato"> Dato del furtivo que inicia la carrera </param>
    public void IniciarCarrera(string dato)
    {
        carrera.Iniciar();
    }

    /// <summary>
    /// Metodo invocado desde el BtnEncender en el canvas del furtivo, para iniciar la carrera
    /// </summary>
    public void LlamadoAIniciarCarrera()
    {
        IniciarCarrera("");
        GestionMensajesServidor.singeton.EnviarMensaje("PF02", " ");
    }

    /// <summary>
    /// Metodo de servidor para reiniciar la carrera en terminos generales
    /// </summary>
    /// <param name="dato"> Dato del furtivo que reinicia la carrera </param>
    public void Reiniciararrera(string dato)
    {
        carrera.RecargarScena();
    }

    /// <summary>
    /// Metodo invocado desde el BtnReiniciar en el canvas del furtivo, para iniciar la carrera
    /// </summary>
    public void LlamadoAReiniciarCarrera()
    {
        GestionMensajesServidor.singeton.EnviarMensaje("PF03", " ");
        Reiniciararrera("");
    }

    /// <summary>
    /// Metodo de servidor para terminar la carrera
    /// </summary>
    /// <param name="dato"> Dato del furtivo que termina la carrera </param>
    public void Terminarrarrera(string dato)
    {
        carrera.ColisionFinal();
    }

    /// <summary>
    /// Metodo invocado desde el scrip LapsManager al momento de finalizar la carrera, por el primer furtivo que pase la meta
    /// </summary>
    public void LlamadoATerminarCarrera()
    {
        Terminarrarrera("");
        GestionMensajesServidor.singeton.EnviarMensaje("PF04", " ");
    }

    /// <summary>
    /// Metodo de servidor, al momento de subirnos al vehiculo nos envia los datos del vehiculo que lo realizó
    /// </summary>
    /// <param name="dato"> Los datos del vehiculo ál que nos subimos </param>
    public void SubirmeFurtivo(string dato)
    {
        MontandoFurtivo mf = JsonUtility.FromJson<MontandoFurtivo>(dato);
        int i = ControlUsuarios.singleton.VerificarExistenciaUsuario(mf.idJugador);
        if (i > -1)
        {
            ControlUsuarios.singleton.usuarios[i].gameObject.SetActive(false);
        }
        int idFurtivo = int.Parse(mf.idFurtivo[7].ToString());
        botonesSubirFurtivo[idFurtivo - 1].gameObject.SetActive(false); 
    }

    /// <summary>
    /// Metodo invocado desde el scrip de MontarFurtivo al momento de subirnos al vehiculo
    /// </summary>
    /// <param name="carro"></param>
    public void LlamadoASubirmeFurtivo(string carro)
    {
        MontandoFurtivo mf = new MontandoFurtivo();
        mf.idJugador = jugadorActual;
        mf.idFurtivo = carro;
        GestionMensajesServidor.singeton.EnviarMensaje("PF05", JsonUtility.ToJson(mf));
    }
}

[System.Serializable]
public class FurtivoPersonalizado
{
    public string numeroJugador;
    public string datosFurtivo;
}

[System.Serializable]
public class MontandoFurtivo
{
    public string idJugador;
    public string idFurtivo;
}