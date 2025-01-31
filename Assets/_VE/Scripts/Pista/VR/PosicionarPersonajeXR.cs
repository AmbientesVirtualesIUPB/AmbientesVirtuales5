using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionarPersonajeXR : MonoBehaviour
{

    public static PosicionarPersonajeXR singleton;
    public CharacterController characterController;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
}
