
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPersonalizacion : MonoBehaviour
{
    public PersonalizarFurtivo  personalizarFurtivo; // Referencia al script de personalizacion del furtivo
    public TextMeshProUGUI      txtAmperios, txtVoltaje, txtKilovatios, txtPeso, txtInformacion; // Textos con la informacion de las baterias
    public TextMeshProUGUI      txtChasis, txtAleron, txtLlanta, txtPesoTotal; // Textos con la informacion del furtivo
    private int                 pesoCarroceria, pesoAleron, pesoLlantas, pesoBateria, pesoTotal; // Variables para el manejo del peso de los elementos
    private bool                flag; // Bandera

    public void ActualizarPanelInformativo()
    {
        DatosCanvasInformativo.voltiosVelocidad = 0;
        DatosCanvasInformativo.centroMasa = 0;

        // Validamos que carroceria se tiene seleccionada en la personalizacion y a raiz de esto modificamos el tipo de chasis y el peso
        if (personalizarFurtivo.carroceriaIndex == 0)
        {
            txtChasis.text = "Sin Carrocer�a";
            pesoCarroceria = 5;
            DatosCanvasInformativo.centroMasa = DatosCanvasInformativo.centroMasa + 0.8f;
        }
        else if (personalizarFurtivo.carroceriaIndex <= 2)
        {
            txtChasis.text = "Fibra Carbono";
            pesoCarroceria = 7;
            DatosCanvasInformativo.centroMasa = 0f;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 2;
        }
        else if (personalizarFurtivo.carroceriaIndex <= 4)
        {
            txtChasis.text = "Pl�stico Reforzado";
            pesoCarroceria = 9;
            DatosCanvasInformativo.centroMasa = 0f;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 4;
        }
        else if (personalizarFurtivo.carroceriaIndex <= 6)
        {
            txtChasis.text = "Aluminio";
            pesoCarroceria = 10;
            DatosCanvasInformativo.centroMasa = 0f;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 6;
        }
        else if (personalizarFurtivo.carroceriaIndex <= 8)
        {
            txtChasis.text = "Acero";
            pesoCarroceria = 12;
            DatosCanvasInformativo.centroMasa = 0f;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 8;
        }

        // Validamos que alerones se tiene seleccionado en la personalizacion y a raiz de esto modificamos el tipo de aleron y el peso
        if (personalizarFurtivo.aleronIndex == 0)
        {
            txtAleron.text = "Sin Aler�n";
            pesoAleron = 0;
            DatosCanvasInformativo.centroMasa = DatosCanvasInformativo.centroMasa + 0.2f;
        }
        else if (personalizarFurtivo.aleronIndex == 1)
        {
            txtAleron.text = "Fibra Carbono";
            pesoAleron = 5;
        }
        else if (personalizarFurtivo.aleronIndex <= 3)
        {
            txtAleron.text = "Pl�stico Reforzado";
            pesoAleron = 6;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 1;
        }
        else if (personalizarFurtivo.aleronIndex <= 4)
        {
            txtAleron.text = "Aluminio";
            pesoAleron = 8;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 2;
        }
        else if (personalizarFurtivo.aleronIndex == 5)
        {
            txtAleron.text = "Acero";
            pesoAleron = 10;
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad - 3;
        }

        // Validamos que llantas se tiene seleccionadas en la personalizacion y a raiz de esto modificamos el tipo de llanta y el peso
        if (personalizarFurtivo.llantaIndex == 0)
        {
            txtLlanta.text = "Sin Llantas";
            pesoLlantas = 0;
        }
        else if (personalizarFurtivo.llantaIndex <= 3)
        {
            txtLlanta.text = "Fibra Carbono";
            pesoLlantas = 5;

            DatosCanvasInformativo.llantasFrenado = 100; // Guardamos el dato de las llanatas para aumentarle la fuerza de frenado a nuestro furtivo
        }
        else if (personalizarFurtivo.llantaIndex <= 6)
        {
            txtLlanta.text = "Goma Natural";
            pesoLlantas = 7;

            DatosCanvasInformativo.llantasFrenado = 200; // Guardamos el dato de las llanatas para aumentarle la fuerza de frenado a nuestro furtivo
        }
        else if (personalizarFurtivo.llantaIndex <= 8)
        {
            txtLlanta.text = "Caucho Sint�tico";
            pesoLlantas = 8;

            DatosCanvasInformativo.llantasFrenado = 300; // Guardamos el dato de las llanatas para aumentarle la fuerza de frenado a nuestro furtivo
        }

        // Validamos que bateria se tiene seleccionadas en la personalizacion y a raiz de esto modificamos la informacion de la bateria
        if (personalizarFurtivo.bateriaIndex - 1 == 0)
        {
            txtAmperios.text = "30 Ah";
            txtVoltaje.text = "48 V";
            txtKilovatios.text = "0.48 kW";
            txtPeso.text = "5 Kg";
            pesoBateria = 5;

            DatosCanvasInformativo.amperiosCarga = 500; // Guardamos el dato del amperio para aumentarle la carga a nuestro furtivo //430
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad + 12; // Guardamos el dato del voltaje para aumentarle la velocidad a nuestro furtivo
            DatosCanvasInformativo.kilovatiosAceleracion = 2; // Guardamos el dato de los kilovatios para aumentarle la aceleracion a nuestro furtivo
        }
        else if (personalizarFurtivo.bateriaIndex - 1 == 1)
        {
            txtAmperios.text = "20 Ah";
            txtVoltaje.text = "60 V";
            txtKilovatios.text = "1.2 kW";
            txtPeso.text = "8 Kg";
            pesoBateria = 8;

            DatosCanvasInformativo.amperiosCarga = 310; // Guardamos el dato del amperio para aumentarle la carga a nuestro furtivo
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad + 22; // Guardamos el dato del voltaje para aumentarle la velocidad a nuestro furtivo
            DatosCanvasInformativo.kilovatiosAceleracion = 4; // Guardamos el dato de los kilovatios para aumentarle la aceleracion a nuestro furtivo
        }
        else if (personalizarFurtivo.bateriaIndex - 1 == 2)
        {
            txtAmperios.text = "10 Ah";
            txtVoltaje.text = "80 V";
            txtKilovatios.text = "2.4 kW";
            txtPeso.text = "10 Kg";
            pesoBateria = 10;

            DatosCanvasInformativo.amperiosCarga = 125; // Guardamos el dato del amperio para aumentarle la carga a nuestro furtivo //84
            DatosCanvasInformativo.voltiosVelocidad = DatosCanvasInformativo.voltiosVelocidad + 32; // Guardamos el dato del voltaje para aumentarle la velocidad a nuestro furtivo
            DatosCanvasInformativo.kilovatiosAceleracion = 6; // Guardamos el dato de los kilovatios para aumentarle la aceleracion a nuestro furtivo
        }

        pesoTotal = pesoCarroceria + pesoAleron + pesoLlantas + pesoBateria; // Sumamos el peso total de cada parte 
        txtPesoTotal.text = pesoTotal.ToString() + " Kg"; // lo mostramos en el canvas
        DatosCanvasInformativo.pesoFurtivo = pesoTotal; // Guardamos el peso para poder utilizarlo en el script de conducir y aumentarle el peso a nuestro furtivo
    }

    private void LateUpdate()
    {      
        if (!flag) // Cargamos inicialmente los datos segun la informacion guardada en la personalizacion con anterioridad
        {
            ActualizarPanelInformativo();
            flag = true;
        }
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionBateria()
    {
        txtInformacion.text = "Este tipo de bater�a es conocida por su alta densidad de energ�a, ligereza, durabilidad y capacidad para mantener su carga durante per�odos prolongados de tiempo.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionAmperios()
    {
        txtInformacion.text = "Los amperios se refieren a la cantidad de energ�a de carga que tendremos para movernos, a mayor amperaje, mayor tiempo de carga para nuestra bater�a.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionVoltaje()
    {
        txtInformacion.text = "Capacidad de la bater�a, nos indica cu�nta corriente suministra durante cierto tiempo, a mayor voltaje, mayor ser� la velocidad m�xima de nuesto furtivo.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionKilovatios()
    {
        txtInformacion.text = "Estos afectan directamente a la potencia de empuje de nuestro furtivo, entre m�s alto sea el valor, mayor ser� nuestro multiplicador de aceleraci�n.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionPeso()
    {
        txtInformacion.text = "Peso de nuestra bater�a, entre m�s pesada sea, afectar� el peso total de nuestro furtivo y al mismo tiempo reducir� nuestra velocidad de empuje.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionFurtivo()
    {
        txtInformacion.text = "Utilizado cada a�o en competencias de tracci�n el�ctrica, enmarcado en un proyecto de investigaci�n de la decanatura de ingenier�a del Pascual Bravo.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionChasis()
    {
        txtInformacion.text = "Tambi�n llamado carrocer�a, este afecta el peso y el aerodinamismo del veh�culo, nos afectar� en la velocidad de empuje y la velocidad m�xima de nuestro furtivo.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionAlerones()
    {
        txtInformacion.text = "Estructura aerodin�mica de forma plana y curva, ubicada en la parte trasera del veh�culo, este afectar� nuestra estabilidad y rendimiento aerodin�mico del furtivo.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionLlantas()
    {
        txtInformacion.text = "Hechas con caracter�sticas �nicas para mejorar el rendimiento y la eficiencia de nuestro furtivo, dependiendo del material estas frenar�n de forma diferente.";
    }

    /// <summary>
    /// Metodo invocado al pasar el cursor por encima de los campos (Pointer Enter) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void InformacionPesoFurtivo()
    {
        txtInformacion.text = "Peso total del veh�culo, este es modificado directamente por todos nuestros componentes, afectar� nuestra velocidad de empuje y estabilidad del furtivo.";
    }

    /// <summary>
    /// Metodo invocado al sacar el cursor de los campos (Pointer Exit) en el panel informativo del Canvas Personalizacion
    /// </summary>
    public void LimpiarPanel()
    {
        txtInformacion.text = "";
    }

    /// <summary>
    /// Metodo invocado desde los BtnFurtivo en el CanvasPersonalizacion en la escena del taller, este se encarga de guardar las caracteristicas de cada furtivo
    /// </summary>
    /// <param name="id"> Identificador del id, dependiendo del furtivo seleccionado </param>
    public void GuardarDatosParaFurtivo(int id)
    {
        DatosCanvasInformativo.datos = "";

        // Seleccionar el arreglo de datosFurtivo basado en el id
        float[] datosSeleccionados = id switch
        {
            1 => DatosCanvasInformativo.datosFurtivo1,
            2 => DatosCanvasInformativo.datosFurtivo2,
            3 => DatosCanvasInformativo.datosFurtivo3,
            4 => DatosCanvasInformativo.datosFurtivo4,
            _ => null
        };

        if (datosSeleccionados == null) return;

        // Asignar valores comunes
        datosSeleccionados[0] = DatosCanvasInformativo.pesoFurtivo;
        datosSeleccionados[1] = DatosCanvasInformativo.amperiosCarga;
        datosSeleccionados[2] = DatosCanvasInformativo.voltiosVelocidad;
        datosSeleccionados[3] = DatosCanvasInformativo.kilovatiosAceleracion;
        datosSeleccionados[4] = DatosCanvasInformativo.llantasFrenado;
        datosSeleccionados[5] = DatosCanvasInformativo.centroMasa;

        // Crear la cadena de datos separada por "|"
        DatosCanvasInformativo.datos = string.Join("|", datosSeleccionados.Select(d => d.ToString()));
    }
}


public static class DatosCanvasInformativo
{
    public static string    datos;
    public static int       pesoFurtivo;
    public static int       amperiosCarga;
    public static int       voltiosVelocidad;
    public static int       kilovatiosAceleracion;
    public static int       llantasFrenado;
    public static float     centroMasa;

    // 
    public static float[]   datosFurtivo1 = new float[6];
    public static float[]   datosFurtivo2 = new float[6];
    public static float[]   datosFurtivo3 = new float[6];
    public static float[]   datosFurtivo4 = new float[6];

    
    public static void RecibirDatosParaFurtivo(int id)
    {
        if (id == 1)
        {
            pesoFurtivo = (int)datosFurtivo1[0];
            amperiosCarga = (int)datosFurtivo1[1];
            voltiosVelocidad = (int)datosFurtivo1[2];
            kilovatiosAceleracion = (int)datosFurtivo1[3];
            llantasFrenado = (int)datosFurtivo1[4];
            centroMasa = datosFurtivo1[5];
        }
        else if (id == 2)
        {
            pesoFurtivo = (int)datosFurtivo2[0];
            amperiosCarga = (int)datosFurtivo2[1];
            voltiosVelocidad = (int)datosFurtivo2[2];
            kilovatiosAceleracion = (int)datosFurtivo2[3];
            llantasFrenado = (int)datosFurtivo2[4];
            centroMasa = datosFurtivo2[5];
        }
        else if (id == 3)
        {
            pesoFurtivo = (int)datosFurtivo3[0];
            amperiosCarga = (int)datosFurtivo3[1];
            voltiosVelocidad = (int)datosFurtivo3[2];
            kilovatiosAceleracion = (int)datosFurtivo3[3];
            llantasFrenado = (int)datosFurtivo3[4];
            centroMasa = datosFurtivo3[5];
        }
        else if (id == 4)
        {
            pesoFurtivo = (int)datosFurtivo4[0];
            amperiosCarga = (int)datosFurtivo4[1];
            voltiosVelocidad = (int)datosFurtivo4[2];
            kilovatiosAceleracion = (int)datosFurtivo4[3];
            llantasFrenado = (int)datosFurtivo4[4];
            centroMasa = datosFurtivo4[5];
        }
    }
}
