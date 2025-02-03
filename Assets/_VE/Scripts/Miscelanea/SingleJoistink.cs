using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleJoistink : MonoBehaviour
{

    public static SingleJoistink singlenton;
    public GameObject joystick;

    private void Awake()
    {
        singlenton = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
