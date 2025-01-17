
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasInformativo : MonoBehaviour
{
    public Button           buttonCarroceria, buttonAlerones, buttonLlantas, buttonBateria; // Arrastra tu bot�n desde el inspector
    public Color            originalColor,newNormalColor; // Define el nuevo color normal
    public TextMeshProUGUI  txtInformacion; // Texto informativo
    public TextMeshProUGUI  txtTitulo; // Titulos

    /// <summary>
    /// Metodo utilizado para cambiar la propiedad color del boton, especificamente el color Normal
    /// </summary>
    /// <param name="button"> Boton al que queremos cambiar el color normal</param>
    public void CambiarColorNormal(Button button)
    {
        ColorBlock cbNormal = buttonCarroceria.colors; // Accede al ColorBlock del bot�n 
        cbNormal.normalColor = newNormalColor; // Cambia el color normal
        button.colors = cbNormal; // Asignamos el color normal al boton
    }

    /// <summary>
    /// Metodo utilizado para cambiar la propiedad color del boton, especificamente el color Normal
    /// </summary>
    /// <param name="button"> Boton al que queremos cambiar el color normal</param>
    public void CambiarColorOriginal(Button button)
    {
        ColorBlock cbOriginal = buttonCarroceria.colors; // Accede al ColorBlock del bot�n 
        cbOriginal.normalColor = originalColor; // Cambia el color al original
        button.colors = cbOriginal; // Asignamos el color original al boton
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonCarroceria del canvas informativo
    /// </summary>
    public void DetallarCarrocerias()
    {
        CambiarColorNormal(buttonCarroceria); // Asigna el ColorBlock actualizado al bot�n
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonLlantas);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Carrocer�as"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: De una ligereza extrema, mejorando la aceleraci�n, maniobrabilidad y consumo de carga, tiene una resistencia y rigidez superior a la" +
            " del acero, buena capacidad de resistencia a los impactos, pero a�n as� se debe tener en consideraci�n que es un producto costoso, con dificultad en su reparaci�n, con una" +
            " personalizaci�n limitada por su material y una conductividad t�rmina baja. El aislamiento ac�stico y de vibraciones no es tan bueno como en otros materiales como el aluminio" +
            " o el pl�stico y la sostenibilidad es muy limitada dado sus elevados costos. \n\n Aluminio: M�s ligero que el acero, mejorando el rendimiento y la aceleraci�n. A pesar de ser" +
            " m�s ligero que el acero tiene una resistencia alta, resiste la corrosi�n, es un material muy maleable y personalizable, una capacidad de absorci�n de impactos bastante alta," +
            " pero propenso a deformarse f�cilmente en comparaci�n con otros materiales, es resistente al calor, reciclable y se puede reparar muy f�cilmente. El aluminio tiene una menor " +
            " densidad que el acero, lo que contribuye a su ligereza y ahorro en los costos de fabricaci�n. \n\n Pl�stico Reforzado: M�s ligeras que las de acero o de aluminio lo que reduce el peso de nuestro furtivo" +
            " y mejora el rendimiento y consumo de bater�a, puede ser reforzado con materiales de fibra de vidrio o de carbono lo que ofrece una gran resistencia a los impactos, tiene" +
            " una alta durabilidad y resistencia a la corrosi�n, es flexible en su dise�o y personalizaci�n, de bajos costos en sus reparaciones y de alta sostenibilidad. Este tipo de" +
            " pl�stico es altamente resistente a productos qu�micos como aceites, grasas y �cidos, lo que lo hace ideal para soportar entornos industriales. \n\n Acero: " +
            " Material com�nmente utilizado en la industria automotriz debido a su resistencia, durabilidad y facilidad de fabricaci�n. Puede soportar impactos severos sin romperse ni " +
            " deformarse f�cilmente y gran protecci�n ante una colisi�n. Es una material de alta resistencia a la tracci�n, es maleable y f�cil de moldear, costo eficiente y de f�cil personalizaci�n," +
            " aislamiento t�rmico y ac�stico, reciclable y de rigidez torsional, lo que significa que la carrocer�a es resistente a la torsi�n o doblado cuando el vehiculo se enfrenta a" +
            " fuerzas desiguales en las curvas o terrenos irregulares.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonAleron del canvas informativo
    /// </summary>
    public void DetallarAlerones()
    {
        CambiarColorNormal(buttonAlerones); // Asigna el ColorBlock actualizado al bot�n
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonLlantas);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Alerones"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: Es un material muy liviano lo que reduce el peso del veh�culo y mejora la eficiencia de la carga, es extremadamente duradero soportando" +
            " altas tensiones y condiciones clim�ticas adversas. Este material nos ayudar� a mejorar la estabilidad a altas velocidades al generar mayor carga aerodin�mica" +
            " sin agregar mucho peso, adicional el aspecto visual del carbono a�ade un toque de lujo y estilo deportivo al furtivo. Pero se debe tener en cuenta que el carbono es m�s caro" +
            " que otros materiales como el acero o el pl�stico reforzado, adicional aunque es un material muy resistente, en caso de un choque puede quebrarse de forma irreparable y mantener" +
            " la apariencia del carbono requiere mayor mantenimiento y cuidado ya que puede deteriorarse por los rayos UV si no tiene un buen recubrimiento. \n\n Pl�stico Reforzado: Son m�s" +
            " econ�micos que los de fibra de carbono, es flexible y absorbe mejor los golpes menores sin romperse f�cilmente. Aunque no tan ligero como la fibra de carbono, sigue siendo un" +
            " material relativamente liviano, lo que afecta significativamente el peso del veh�culo. Es f�cil de moldear lo que se traduce en que puede fabricarse de diversas formas y dise�os," +
            " ofreciendo m�s opciones personalizables. Se debe tener en consideraci�n que con el tiempo puede desgastarse, agrietarse o decolorarse debido a la exposici�n al sol y condiciones" +
            " clim�ticas extremas, no ofrece el mismo nivel de eficiencia aerodin�mica que materiales m�s avanzados ni tampoco tiene la est�tica deportiva y premium de " +
            "otros materiales como la fibra de carbono, lo que puede ser menos atractivo para entusiastas del dise�o automovil�stico.\n\n Acero: Son extremadamente fuertes y resistentes a los impactos," +
            " lo que lo hace ideal para soportar condiciones extremas y desgaste prolongado. Proporciona una excelente estabilidad y rigidez, lo que puede mejorar el rendimiento aerodin�mico" +
            " y el control del veh�culo a altas velocidades, es m�s resistente a los da�os causados por el clima, como la exposici�n a los rayos UV o a la corrosi�n. Se debe tener en consideraci�n" +
            " que el acero es m�s pesado que la fibra de carbono o el pl�stico reforzado, lo que afecta negativamente el rendimiento del veh�culo y aumenta el consumo de la carga, aunque" +
            " es r�gido y estable, el peso adicional puede contrarrestar las ventajas aerodin�micas que ofrece. Tambi�n debido a su peso la instalaci�n puede ser m�s compleja y en algunos" +
            " casos, puede requerir refuerzos adicionales en el veh�culo y si no est� adecuadamente protegido, el acero puede oxidarse con el tiempo, aunque con tratamientos como el galvanizado" +
            " este problema se minimiza. \n\n Aluminio: Son m�s livianos que los de acero, lo que ayuda a la eficiencia de la bater�a y a la velocidad y adem�s tiene buena resistencia-peso" +
            ", es resistente a la corrosi�n ya que no se oxida f�cilmente, los alerones de aluminio pueden ser f�cilmente moldeados, lo que nos da una amplia variedad de colores y est�ticas," +
            " adem�s al ser un material m�s liviano que el acero facilita el proceso de instalaci�n y ajustes sin necesidad de extensiones. Se debe tener en cuenta que puede ser un material m�s" +
            " costoso que el pl�stico reforzado. En comparaci�n con la fibra de carbono no es tan r�gido, lo que no lo hace tan eficiente a grandes velocidades, puede adem�s generar vibraciones" +
            " no deseadas a altas velocidades, tambi�n es afectado por las altas temperaturas ya que el aluminio es un buen conductor de calor y es m�s propenso a abollarse que otros materiales" +
            " como el acero o la fibra de carbono.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonLlantas del canvas informativo
    /// </summary>
    public void DetallarLlantas()
    {
        CambiarColorNormal(buttonLlantas); // Asigna el ColorBlock actualizado al bot�n
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Llantas"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: Es un material mucho m�s ligero que el acero o el aluminio, lo que nos da una reducci�n significativa en el peso, mejora el rendimiento din�mico, la" +
            " aceleraci�n, la frenada y eficiencia en el combustible ya que el motor necesita menos energ�a para mover las ruedas. Son de alta resistencia a la tracci�n lo que significa" +
            " que pueden soportar grandes fuerzas sin deformarse o romperse, son m�s r�gidaz que las de acero o aluminio lo que mejora la precisi�n en la direcci�n, es decir, m�s �giles" +
            " y estables. Mayor absorci�n de vibraciones y resistencia a la corrosi�n y a altas temperaturas. Se debe tener en cuenta que es un material de costo elevado lo cual se traduce" +
            " en dificultades para su reparaci�n y una reciclabilidad muy limitada aunque por su gran durabilidad y resistencia necesitan poco mantenimiento. \n\n Goma Natural: Tambi�n" +
            " conocidas como llantas de caucho o neum�ticos, es un material altamente el�stico lo que permite que las llantas puedan deformarse y volver a su forma orginal sin da�os, esto" +
            " es crucial para la absorci�n de impactos, ofrece una excelente tracci�n y adherencia a la carretera, tiene buena resistencia al desgaste pero sufre mucho en altas temperaturas," +
            " su fricci�n es baja lo cual se traduce en una buena eficiencia de combustible, aunque no es tan resistente como otros materiales, tambi�n ofrece buena protecci�n contra cortes" +
            " o perforaciones, se puede adaptar a diferentes tipos de terrenos, es un material con buena reciblabilidad, resiste el envejecimiento y generan un menor ruido y vibraci�n en la" +
            " carretera pues esta absorbe las vibraciones y las suaviza con el pavimento, lo que contribuye a una experiencia de conducci�n m�s silenciosa. \n\n Caucho Sint�tico: Es el tipo" +
            " de neum�tico m�s utilizado en la actualidad, debido a su capacidad para proporcionar un buen rendimiento y durabilidad a un costo relativamente bajo, y aunque se utiliza com�nmente" +
            " en combinaci�n con la goma natural tambi�n ofrece unas caracter�sticas �nicas por si solo. Este tiene una mayor resistencia al calor que la goma natural, lo que ayuda a soportar" +
            " altas temperaturas y la fricci�n en la carretera, lo que las hace ideales para viajes m�s largos. En general son m�s duraderas que las de goma natural pues soportan un mayor" +
            " desgaste en condiciones de uso intenso y tambi�n una mayor tracci�n en superficies mojadas, tambi�n tiene m�s calidad que la goma natural y resiste mejor la exposici�n a productos" +
            " qu�micos, pero tienen menor elasticidad que la goma natural, pues no son tan flexibles ni absorben tan bien los impactos, pero a su vez tienen mayor resistencia a las deformaciones y aguantan" +
            " mejor las temperaturas de los climas extremos que la goma natural. Por �ltimo agregar que no es un material tan reciclable como la goma natural, lo que plantea grandes desaf�os ambientales.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonBateria del canvas informativo
    /// </summary>
    public void DetallarBaterias()
    {
        CambiarColorNormal(buttonBateria); // Asigna el ColorBlock actualizado al bot�n
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonLlantas);

        txtTitulo.text = "Bater�a Litio (Li-ion)"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Amperios: Los amperios en la bater�a de litio de nuestro furtivo afectar�n principalmente la capacidad de entrega de corriente y duraci�n de la carga. Una bater�a con m�s amperios-" +
            "hora (Ah) puede proporcionar m�s energ�a durante m�s tiempo, lo que extiende la autonom�a del veh�culo el�ctrico o aumenta el tiempo de funcionamiento de los sistemas electr�nicos" +
            ". Sin embargo m�s amperios tambi�n puede implicar un tama�o o peso mayor de la bater�a. En resumen, una mayor capacidad de amperios permite un mejor rendimiento en t�rminos de potencia" +
            " y duraci�n, pero tambi�n puede tener implicaciones de peso y el espacio disponible en el furtivo. \n\n Voltios: Los voltios en la bater�a de litio de el furtivo, determinan" +
            " la cantidad de energ�a que puede entregar a los sistemas el�ctricos. Un voltaje m�s alto permite una mayor potencia y eficiencia en el rendimiento del furtivo ya que puede mejorar" +
            " la aceleraci�n y la velocidad. Tambi�n puede reducir la necesidad de corriente alta, lo que disminuye el calentamiento y aumenta la eficiencia del sistema. En resumen, un" +
            " voltaje mayor permite un mejor rendimiento y una mayor eficiencia energ�tica, pero puede requerir componentes el�ctricos m�s robustos y espec�ficos. \n\n Kilovatios: Los kilovatios" +
            " en la bater�a de nuestro furtivo indican la cantidad total de energ�a almacenada y afectan directamente la autonom�a del furtivo. Cuanto mayor sea la capacidad de kilovatios, m�s energ�a" +
            " puede almacenar la bater�a, lo que permite recorrer distancias m�s largas sin necesidad de recargar. Adem�s puede proporcionar m�s potencia, mejorando el rendimiendo en" +
            " aceleraci�n y velocidad. En resumen, entre m�s kilovatios tenga nuestra bater�a, tendremos m�s capacidad de aceleraci�n, pero tambi�n puede implicar un mayor tama�o, peso" +
            " y costo de la bater�a.";
    }
}


