using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtcraftClanBtn : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
	public ClanArtcraft clan;

	public void Inicializar(ClanArtcraft _clan)
	{
		clan = _clan;
		tmpro.text = clan.nombre_clan;
	}
    public void Activar()
	{
		print("Activado el botón del clan: " + clan.nombre_clan + " que pertenece al grupo " + clan.nombre_grupo);
	}
}
