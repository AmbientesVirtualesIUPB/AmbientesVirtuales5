using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAndroid : MonoBehaviour
{
    public Joystick j1;
    public Joystick j2;


    public static InputAndroid instance;
    public static Joystick sj1;
    public static Joystick sj2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (j1 != null)
            {
                sj1 = j1;
            }
            if (j2 != null)
            {
                sj2 = j2;
            }
        }
        else
        {
            DestroyImmediate(this);
        }
    }


    public static float GetHorizontal1()
    {
        if (sj1 == null)
        {
            return Input.GetAxis("Horizontal");
        }

        return sj1.Horizontal + Input.GetAxis("Horizontal");
    }

    public static float GetVeretical1()
    {
        if (sj1 == null)
        {
            return Input.GetAxis("Vertical");
        }

        return sj1.Vertical + Input.GetAxis("Vertical");
    }

    public static float GetHorizontal2()
    {
        if (sj2 == null)
        {
            return Input.GetAxis("Horizontal");
        }

        return sj2.Horizontal + Input.GetAxis("Horizontal");
    }

    public static float GetVeretical2()
    {
        if (sj2 == null)
        {
            return Input.GetAxis("Vertical");
        }

        return sj2.Vertical + Input.GetAxis("Vertical");
    }

}
