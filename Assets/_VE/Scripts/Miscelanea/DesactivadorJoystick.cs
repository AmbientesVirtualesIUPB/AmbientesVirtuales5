using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivadorJoystick : MonoBehaviour
{
    public Joystick j;
    public Conducir[] carros;
    public void desactivarjoystick()
    {
        SingleJoistink.singlenton.joystick.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Conducir carro in carros)
        {
            if(j.Horizontal < -0.5f)
            {
                carro.GirarIzquierda();
            }
            else if(j.Horizontal > 0.5f)
            {
                carro.GirarDerecha();

            }
            else
            {
                carro.SoltarGiroDerecha();
                carro.SoltarGiroIzquierda();
            }
        }
    }
}
