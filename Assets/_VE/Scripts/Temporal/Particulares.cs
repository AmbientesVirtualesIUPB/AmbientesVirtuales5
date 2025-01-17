using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulares : MonoBehaviour
{
    public float duracion;
    public Color color;
    public ParticleSystem particulas;

    private void Start()
    {
        GestionMensajesServidor.singeton.RegistrarAccion("PA01", Interpretar);
    } 

    public void Interpretar(string msj)
	{
        InfoParticulas info = JsonUtility.FromJson<InfoParticulas>(msj);
        duracion = info.duracion;
        color = info.color;
        Iniciar();
	}

	public void Iniciar()
	{
        var main = particulas.main;
        main.duration = duracion;
        main.startColor = color;

        particulas.Play();
    }

    public void IniciarDesdeServidor()
	{
        InfoParticulas info = new InfoParticulas();
        info.duracion = duracion;
        info.color = color;
        string json = JsonUtility.ToJson(info);
        GestionMensajesServidor.singeton.EnviarMensaje("PA01", json);
        Iniciar();
	}

}

[System.Serializable]
public class InfoParticulas
{
    public float duracion;
    public Color color;
}