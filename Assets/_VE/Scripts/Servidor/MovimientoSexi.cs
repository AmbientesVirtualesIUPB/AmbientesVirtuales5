using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSexi : MonoBehaviour
{
    public float velocidad = 2;
    public float velRotacion = 90;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (!GetComponent<MorionID>().isOwner) enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * InputAndroid.GetVeretical1()) * velocidad * Time.deltaTime);
        transform.Rotate(Vector3.up * InputAndroid.GetHorizontal1() * velRotacion * Time.deltaTime) ;
    }

}
