
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(Slider))]

public class JoystickLineal : MonoBehaviour/*, IPointerUpHandler*/
{
    Slider sl;
    public float v;


    private void Awake()
    {
        sl = GetComponent<Slider>();
        sl.minValue = -1;
        sl.maxValue = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        sl.value = 0;
    }

    public void Reiniciar()
    {
        sl.value = 0;
    }
}
