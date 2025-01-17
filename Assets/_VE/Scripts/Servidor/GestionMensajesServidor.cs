using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Morion Servidor/Gestion Mensajes Servidor")]
public class GestionMensajesServidor : MonoBehaviour
{
    public static GestionMensajesServidor singeton;
	public bool debugEnConsola = false;
	public List<MorionTransform> morionTransforms = new List<MorionTransform>();
	private Dictionary<string, Action<string>> acciones = new Dictionary<string, Action<string>>();

	private void Awake()
	{
		if (singeton != null)
		{
			DestroyImmediate(this);
		}
		else
		{
			singeton = this;
		}
	}

	public void RecibirMensaje(string mensaje)
	{
		if (debugEnConsola) print("MENSAJE:" + mensaje);
		string codigo = mensaje.Substring(0, 4);
		string msj = mensaje.Substring(4);
		switch (codigo)
		{
			case "AT00":
				AT00(msj);
				break;
			default:
				break;
		}
		EjecutarAccion(codigo, msj);
	}
	public void EnviarMensaje(string msj)
	{
		Servidor.singleton.EnviarMensaje(msj);
	}

	public void EnviarMensaje(string codigo, string msj)
	{
		Servidor.singleton.EnviarMensaje(codigo + msj);
	}




	// M�todo para registrar una acci�n
	public void RegistrarAccion(string palabraClave, Action<string> accion)
	{
		if (!acciones.ContainsKey(palabraClave))
		{
			acciones.Add(palabraClave, accion);
			if (debugEnConsola) Debug.Log($"Acci�n registrada: {palabraClave}");
		}
		else
		{
			Debug.LogWarning($"La palabra clave '{palabraClave}' ya tiene una acci�n registrada.");
		}
	}

	// M�todo para ejecutar una acci�n basada en una palabra clave
	public void EjecutarAccion(string palabraClave, string parametro)
	{
		if (acciones.TryGetValue(palabraClave, out Action<string> accion))
		{
			accion.Invoke(parametro); // Llama al m�todo registrado con el par�metro dado
		}
		else
		{
			Debug.LogWarning($"No se encontr� ninguna acci�n registrada para la palabra clave '{palabraClave}'.");
		}
	}

	public void AT00(string msj)
	{
		if(debugEnConsola) print("Mensaje AT00 :::::::> " + msj);
		Posicion0 po0 = JsonUtility.FromJson<Posicion0>(msj);
		for (int i = 0; i < morionTransforms.Count; i++)
		{
			if (morionTransforms[i].morionID.GetID() == po0.id_con)
			{
				morionTransforms[i].ActualizarObjetivos(po0);
			}
		}
	}

	public void EnviarActualizacionTransform(MorionID morionID, Transform trans) 
	{
		Posicion0 pos = new Posicion0();
		pos.id_con = morionID.GetID();
		pos.rotacion = trans.eulerAngles;
		pos.posicion = trans.position;
		string msj = "AT00" + JsonUtility.ToJson(pos);
		Servidor.singleton.EnviarMensaje(msj);
	}

	public void MatricularMorionTransform(MorionTransform mt)
	{
		if (debugEnConsola)
			Debug.Log("Inicia la matricula de " + mt.gameObject.name);
		for (int i = 0; i < morionTransforms.Count; i++)
		{
			if (debugEnConsola)
				Debug.Log("Revisando id de uno a uno " + i);
			if (mt.morionID.GetID().Equals(morionTransforms[i].morionID.GetID()))
			{
				if (debugEnConsola)
					Debug.Log("se encontró " + i);
				morionTransforms[i] = mt;
				return;
			}
		}
		if (debugEnConsola)
			Debug.Log("matriculando");
		morionTransforms.Add(mt);
	}
}
