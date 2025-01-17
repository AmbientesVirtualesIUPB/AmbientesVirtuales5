
using UnityEngine;

public class ConduccionMultiplataforma : MonoBehaviour
{

    public Conducir conducir;
    bool acelerar;
    bool reversar;
    bool izquierda;
    bool derecha;

    /// <summary>
    /// Acelerar furtivo
    /// </summary>
    /// <param name="b"> Booleano True o False</param>
    public void Acelerar(bool b)
    {
        acelerar = b;
        conducir.throttlePTI = b;
    }

    /// <summary>
    /// Reversar furtivo
    /// </summary>
    /// <param name="b"> Booleano True o False</param>
    public void Reversar(bool b)
    {
        reversar = b;
        conducir.reversePTI = b;
    }

    /// <summary>
    /// Girar a la izquierda el furtivo
    /// </summary>
    /// <param name="b"> Booleano True o False</param>
    public void GirarIzquierda(bool b)
    {
        izquierda = b;
        conducir.turnLeftPTI = b;
    }

    /// <summary>
    /// Girar a la derecha el furtivo
    /// </summary>
    /// <param name="b"> Booleano True o False</param>
    public void GirarDerecha(bool b)
    {
        derecha = b;
        conducir.turnRightPTI = b;
    }

    /// <summary>
    /// Frenar furtivo
    /// </summary>
    /// <param name="b"> Booleano True o False</param>
    public void Frenar()
    {
        conducir.Brakes();
    }

    //Enviar booleano true
    public void FrenoDeManos(bool b)
    {
        conducir.handbrakePTI = b;
    }

    //Enviar booleano false
    public void RecuperarTraccion(bool b)
    {
        conducir.handbrakePTI = b;
    }
}
