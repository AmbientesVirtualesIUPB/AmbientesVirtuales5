using UnityEngine;
using UnityEngine.Events;

public class Clickeable : MonoBehaviour
{
    public UnityEvent eventoClick;

    public bool desemparentar;

    private void Start()
    {
        if (desemparentar)
        {
            transform.parent = null;
        }
    }


    public void OnMouseDown()
    {
        print("cik");
        if (eventoClick != null)
        {
            eventoClick.Invoke();
        }
    }
    
}
