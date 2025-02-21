using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnirSala : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OdinHandler.Instance.JoinRoom("prueba");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
