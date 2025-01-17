using UnityEngine;

public class ManejadorAndroid : MonoBehaviour
{
    public ConduccionMultiplataforma conduccionMultiplataforma;

    public static ManejadorAndroid instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Update()
    {
        if (conduccionMultiplataforma == null)
        {
            return;
        }

        conduccionMultiplataforma.GirarDerecha(InputAndroid.GetHorizontal1() > 0.5f);
        conduccionMultiplataforma.GirarIzquierda(InputAndroid.GetHorizontal1() < -0.5f);
        conduccionMultiplataforma.Acelerar(InputAndroid.GetVeretical1() > 0.5f);
        conduccionMultiplataforma.Reversar(InputAndroid.GetVeretical1() < -0.5f);
    }
}
