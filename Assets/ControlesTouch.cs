using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesTouch : MonoBehaviour
{
    public Conducir conducir;

    public void PresionarAcelerador() => conducir.throttlePTI = true;
    public void SoltarAcelerador() => conducir.throttlePTI = false;

    public void PresionarReversa() => conducir.reversePTI = true;
    public void SoltarReversa() => conducir.reversePTI = false;

    public void GirarIzquierda() => conducir.turnLeftPTI = true;
    public void SoltarGiroIzquierda() => conducir.turnLeftPTI = false;

    public void GirarDerecha() => conducir.turnRightPTI = true;
    public void SoltarGiroDerecha() => conducir.turnRightPTI = false;

    public void FrenoMano() => conducir.handbrakePTI = true;
    public void SoltarFrenoMano() => conducir.handbrakePTI = false;
}