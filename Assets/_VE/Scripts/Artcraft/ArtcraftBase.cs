using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ArtcraftBase : MonoBehaviour
{
    public UsuarioArtcraft usuarioArtcraft;
    public GameObject prBotonClan;
    public Transform padreBotonesClanes;

    void Start()
    {
        StartCoroutine(CargarDatos());
        print(JsonUtility.ToJson(usuarioArtcraft));
    }

    public void CrearBotones()
	{
		for (int i = 0; i < usuarioArtcraft.clanes.Length; i++)
		{
            ArtcraftClanBtn btn = Instantiate(prBotonClan, padreBotonesClanes).GetComponent<ArtcraftClanBtn>();
            btn.Inicializar(usuarioArtcraft.clanes[i]);
		}
	}

    public IEnumerator CargarDatos()
    {
        string url = ConfiguracionGeneral.configuracionDefault.urlAPIArtcraft + "estudiante_json.php?id=3";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Enviar la solicitud
            yield return request.SendWebRequest();

            // Verificar si hubo un error
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error al cargar los datos: {request.error}");
            }
            else
            {
                // Obtener la respuesta en formato JSON
                string json = request.downloadHandler.text;

                // Deserializar el JSON en un objeto UsuarioArtcraft
                try
                {
                    usuarioArtcraft = JsonUtility.FromJson<UsuarioArtcraft>(json);
                    CrearBotones();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error al deserializar el JSON: {ex.Message}\n" + request.downloadHandler.text);
                }
            }
        }
    }
}

[System.Serializable]
public class UsuarioArtcraft
{
    public ClanArtcraft[] clanes;
}

[System.Serializable]
public class ClanArtcraft
{
    public string nombre_clan;
    public string nombre_grupo;
}
