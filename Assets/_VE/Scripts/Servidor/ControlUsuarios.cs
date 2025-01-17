using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUsuarios : MonoBehaviour
{
    public static ControlUsuarios singleton;
    public Usuario usuarioLocal;
    public List<Usuario> usuarios;

	public GameObject prUsuario;

	public bool debugEnConsola = false;
	private void Awake()
	{
        singleton = this;
	}
	void Start()
	{
		GestionMensajesServidor.singeton.RegistrarAccion("AC00", AC00);
		GestionMensajesServidor.singeton.RegistrarAccion("PR00", PR00);
	}

	/// <summary>
	/// Solicitud de que todos se presenten para crear la lista de usuarios
	/// </summary>
	/// <param name="msj"></param>
	public void AC00(string msj)
	{
		// Enviar presentación del usuario
		if (usuarioLocal != null)
		{
			GestionMensajesServidor.singeton.EnviarMensaje("PR00", usuarioLocal.GetPresentacion());
		}
	}

	/// <summary>
	/// Mensaje de presentación, se utiliza cuando un usuario se va a presentar ante los demás para que se cree la instancia.
	/// </summary>
	/// <param name="msj"></param>
	public void PR00(string msj)
	{
		if (debugEnConsola)
		{
			print("PR00 para presentar a:" + msj);
		}
		Presentacion p = JsonUtility.FromJson<Presentacion>(msj);
		if (VerificarExistenciaUsuario(p.id_con) == -1)
		{
			GameObject nu = Instantiate(prUsuario);
			Usuario u = nu.GetComponent<Usuario>();
			u.Inicializar(msj, false);
			usuarios.Add(u);
		}
	}

	public int VerificarExistenciaUsuario(string id_con)
	{
		int id = -1;
		for (int i = 0; i < usuarios.Count; i++)
		{
			if (usuarios[i].GetMorionID().GetID() == id_con)
			{
				id = i;
				break;
			}
		}
		return id;
	}
}
