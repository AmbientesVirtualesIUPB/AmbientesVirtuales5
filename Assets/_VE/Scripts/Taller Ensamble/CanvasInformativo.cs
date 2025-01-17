
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasInformativo : MonoBehaviour
{
    public Button           buttonCarroceria, buttonAlerones, buttonLlantas, buttonBateria; // Arrastra tu botón desde el inspector
    public Color            originalColor,newNormalColor; // Define el nuevo color normal
    public TextMeshProUGUI  txtInformacion; // Texto informativo
    public TextMeshProUGUI  txtTitulo; // Titulos

    /// <summary>
    /// Metodo utilizado para cambiar la propiedad color del boton, especificamente el color Normal
    /// </summary>
    /// <param name="button"> Boton al que queremos cambiar el color normal</param>
    public void CambiarColorNormal(Button button)
    {
        ColorBlock cbNormal = buttonCarroceria.colors; // Accede al ColorBlock del botón 
        cbNormal.normalColor = newNormalColor; // Cambia el color normal
        button.colors = cbNormal; // Asignamos el color normal al boton
    }

    /// <summary>
    /// Metodo utilizado para cambiar la propiedad color del boton, especificamente el color Normal
    /// </summary>
    /// <param name="button"> Boton al que queremos cambiar el color normal</param>
    public void CambiarColorOriginal(Button button)
    {
        ColorBlock cbOriginal = buttonCarroceria.colors; // Accede al ColorBlock del botón 
        cbOriginal.normalColor = originalColor; // Cambia el color al original
        button.colors = cbOriginal; // Asignamos el color original al boton
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonCarroceria del canvas informativo
    /// </summary>
    public void DetallarCarrocerias()
    {
        CambiarColorNormal(buttonCarroceria); // Asigna el ColorBlock actualizado al botón
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonLlantas);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Carrocerías"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: De una ligereza extrema, mejorando la aceleración, maniobrabilidad y consumo de carga, tiene una resistencia y rigidez superior a la" +
            " del acero, buena capacidad de resistencia a los impactos, pero aún así se debe tener en consideración que es un producto costoso, con dificultad en su reparación, con una" +
            " personalización limitada por su material y una conductividad términa baja. El aislamiento acústico y de vibraciones no es tan bueno como en otros materiales como el aluminio" +
            " o el plástico y la sostenibilidad es muy limitada dado sus elevados costos. \n\n Aluminio: Más ligero que el acero, mejorando el rendimiento y la aceleración. A pesar de ser" +
            " más ligero que el acero tiene una resistencia alta, resiste la corrosión, es un material muy maleable y personalizable, una capacidad de absorción de impactos bastante alta," +
            " pero propenso a deformarse fácilmente en comparación con otros materiales, es resistente al calor, reciclable y se puede reparar muy fácilmente. El aluminio tiene una menor " +
            " densidad que el acero, lo que contribuye a su ligereza y ahorro en los costos de fabricación. \n\n Plástico Reforzado: Más ligeras que las de acero o de aluminio lo que reduce el peso de nuestro furtivo" +
            " y mejora el rendimiento y consumo de batería, puede ser reforzado con materiales de fibra de vidrio o de carbono lo que ofrece una gran resistencia a los impactos, tiene" +
            " una alta durabilidad y resistencia a la corrosión, es flexible en su diseño y personalización, de bajos costos en sus reparaciones y de alta sostenibilidad. Este tipo de" +
            " plástico es altamente resistente a productos químicos como aceites, grasas y ácidos, lo que lo hace ideal para soportar entornos industriales. \n\n Acero: " +
            " Material comúnmente utilizado en la industria automotriz debido a su resistencia, durabilidad y facilidad de fabricación. Puede soportar impactos severos sin romperse ni " +
            " deformarse fácilmente y gran protección ante una colisión. Es una material de alta resistencia a la tracción, es maleable y fácil de moldear, costo eficiente y de fácil personalización," +
            " aislamiento térmico y acústico, reciclable y de rigidez torsional, lo que significa que la carrocería es resistente a la torsión o doblado cuando el vehiculo se enfrenta a" +
            " fuerzas desiguales en las curvas o terrenos irregulares.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonAleron del canvas informativo
    /// </summary>
    public void DetallarAlerones()
    {
        CambiarColorNormal(buttonAlerones); // Asigna el ColorBlock actualizado al botón
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonLlantas);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Alerones"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: Es un material muy liviano lo que reduce el peso del vehículo y mejora la eficiencia de la carga, es extremadamente duradero soportando" +
            " altas tensiones y condiciones climáticas adversas. Este material nos ayudará a mejorar la estabilidad a altas velocidades al generar mayor carga aerodinámica" +
            " sin agregar mucho peso, adicional el aspecto visual del carbono añade un toque de lujo y estilo deportivo al furtivo. Pero se debe tener en cuenta que el carbono es más caro" +
            " que otros materiales como el acero o el plástico reforzado, adicional aunque es un material muy resistente, en caso de un choque puede quebrarse de forma irreparable y mantener" +
            " la apariencia del carbono requiere mayor mantenimiento y cuidado ya que puede deteriorarse por los rayos UV si no tiene un buen recubrimiento. \n\n Plástico Reforzado: Son más" +
            " económicos que los de fibra de carbono, es flexible y absorbe mejor los golpes menores sin romperse fácilmente. Aunque no tan ligero como la fibra de carbono, sigue siendo un" +
            " material relativamente liviano, lo que afecta significativamente el peso del vehículo. Es fácil de moldear lo que se traduce en que puede fabricarse de diversas formas y diseños," +
            " ofreciendo más opciones personalizables. Se debe tener en consideración que con el tiempo puede desgastarse, agrietarse o decolorarse debido a la exposición al sol y condiciones" +
            " climáticas extremas, no ofrece el mismo nivel de eficiencia aerodinámica que materiales más avanzados ni tampoco tiene la estética deportiva y premium de " +
            "otros materiales como la fibra de carbono, lo que puede ser menos atractivo para entusiastas del diseño automovilístico.\n\n Acero: Son extremadamente fuertes y resistentes a los impactos," +
            " lo que lo hace ideal para soportar condiciones extremas y desgaste prolongado. Proporciona una excelente estabilidad y rigidez, lo que puede mejorar el rendimiento aerodinámico" +
            " y el control del vehículo a altas velocidades, es más resistente a los daños causados por el clima, como la exposición a los rayos UV o a la corrosión. Se debe tener en consideración" +
            " que el acero es más pesado que la fibra de carbono o el plástico reforzado, lo que afecta negativamente el rendimiento del vehículo y aumenta el consumo de la carga, aunque" +
            " es rígido y estable, el peso adicional puede contrarrestar las ventajas aerodinámicas que ofrece. También debido a su peso la instalación puede ser más compleja y en algunos" +
            " casos, puede requerir refuerzos adicionales en el vehículo y si no está adecuadamente protegido, el acero puede oxidarse con el tiempo, aunque con tratamientos como el galvanizado" +
            " este problema se minimiza. \n\n Aluminio: Son más livianos que los de acero, lo que ayuda a la eficiencia de la batería y a la velocidad y además tiene buena resistencia-peso" +
            ", es resistente a la corrosión ya que no se oxida fácilmente, los alerones de aluminio pueden ser fácilmente moldeados, lo que nos da una amplia variedad de colores y estéticas," +
            " además al ser un material más liviano que el acero facilita el proceso de instalación y ajustes sin necesidad de extensiones. Se debe tener en cuenta que puede ser un material más" +
            " costoso que el plástico reforzado. En comparación con la fibra de carbono no es tan rígido, lo que no lo hace tan eficiente a grandes velocidades, puede además generar vibraciones" +
            " no deseadas a altas velocidades, también es afectado por las altas temperaturas ya que el aluminio es un buen conductor de calor y es más propenso a abollarse que otros materiales" +
            " como el acero o la fibra de carbono.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonLlantas del canvas informativo
    /// </summary>
    public void DetallarLlantas()
    {
        CambiarColorNormal(buttonLlantas); // Asigna el ColorBlock actualizado al botón
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonBateria);

        txtTitulo.text = "Tipos De Llantas"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Fibra De Carbono: Es un material mucho más ligero que el acero o el aluminio, lo que nos da una reducción significativa en el peso, mejora el rendimiento dinámico, la" +
            " aceleración, la frenada y eficiencia en el combustible ya que el motor necesita menos energía para mover las ruedas. Son de alta resistencia a la tracción lo que significa" +
            " que pueden soportar grandes fuerzas sin deformarse o romperse, son más rígidaz que las de acero o aluminio lo que mejora la precisión en la dirección, es decir, más ágiles" +
            " y estables. Mayor absorción de vibraciones y resistencia a la corrosión y a altas temperaturas. Se debe tener en cuenta que es un material de costo elevado lo cual se traduce" +
            " en dificultades para su reparación y una reciclabilidad muy limitada aunque por su gran durabilidad y resistencia necesitan poco mantenimiento. \n\n Goma Natural: También" +
            " conocidas como llantas de caucho o neumáticos, es un material altamente elástico lo que permite que las llantas puedan deformarse y volver a su forma orginal sin daños, esto" +
            " es crucial para la absorción de impactos, ofrece una excelente tracción y adherencia a la carretera, tiene buena resistencia al desgaste pero sufre mucho en altas temperaturas," +
            " su fricción es baja lo cual se traduce en una buena eficiencia de combustible, aunque no es tan resistente como otros materiales, también ofrece buena protección contra cortes" +
            " o perforaciones, se puede adaptar a diferentes tipos de terrenos, es un material con buena reciblabilidad, resiste el envejecimiento y generan un menor ruido y vibración en la" +
            " carretera pues esta absorbe las vibraciones y las suaviza con el pavimento, lo que contribuye a una experiencia de conducción más silenciosa. \n\n Caucho Sintético: Es el tipo" +
            " de neumático más utilizado en la actualidad, debido a su capacidad para proporcionar un buen rendimiento y durabilidad a un costo relativamente bajo, y aunque se utiliza comúnmente" +
            " en combinación con la goma natural también ofrece unas características únicas por si solo. Este tiene una mayor resistencia al calor que la goma natural, lo que ayuda a soportar" +
            " altas temperaturas y la fricción en la carretera, lo que las hace ideales para viajes más largos. En general son más duraderas que las de goma natural pues soportan un mayor" +
            " desgaste en condiciones de uso intenso y también una mayor tracción en superficies mojadas, también tiene más calidad que la goma natural y resiste mejor la exposición a productos" +
            " químicos, pero tienen menor elasticidad que la goma natural, pues no son tan flexibles ni absorben tan bien los impactos, pero a su vez tienen mayor resistencia a las deformaciones y aguantan" +
            " mejor las temperaturas de los climas extremos que la goma natural. Por último agregar que no es un material tan reciclable como la goma natural, lo que plantea grandes desafíos ambientales.";
    }

    /// <summary>
    /// Metodo invocado desde el evento onClick en el ButtonBateria del canvas informativo
    /// </summary>
    public void DetallarBaterias()
    {
        CambiarColorNormal(buttonBateria); // Asigna el ColorBlock actualizado al botón
        // Devolvemos al color original los demas botones
        CambiarColorOriginal(buttonCarroceria);
        CambiarColorOriginal(buttonAlerones);
        CambiarColorOriginal(buttonLlantas);

        txtTitulo.text = "Batería Litio (Li-ion)"; // Asignamos el titulo
        // Colocamos la informacion detallada
        txtInformacion.text = " Amperios: Los amperios en la batería de litio de nuestro furtivo afectarán principalmente la capacidad de entrega de corriente y duración de la carga. Una batería con más amperios-" +
            "hora (Ah) puede proporcionar más energía durante más tiempo, lo que extiende la autonomía del vehículo eléctrico o aumenta el tiempo de funcionamiento de los sistemas electrónicos" +
            ". Sin embargo más amperios también puede implicar un tamaño o peso mayor de la batería. En resumen, una mayor capacidad de amperios permite un mejor rendimiento en términos de potencia" +
            " y duración, pero también puede tener implicaciones de peso y el espacio disponible en el furtivo. \n\n Voltios: Los voltios en la batería de litio de el furtivo, determinan" +
            " la cantidad de energía que puede entregar a los sistemas eléctricos. Un voltaje más alto permite una mayor potencia y eficiencia en el rendimiento del furtivo ya que puede mejorar" +
            " la aceleración y la velocidad. También puede reducir la necesidad de corriente alta, lo que disminuye el calentamiento y aumenta la eficiencia del sistema. En resumen, un" +
            " voltaje mayor permite un mejor rendimiento y una mayor eficiencia energética, pero puede requerir componentes eléctricos más robustos y específicos. \n\n Kilovatios: Los kilovatios" +
            " en la batería de nuestro furtivo indican la cantidad total de energía almacenada y afectan directamente la autonomía del furtivo. Cuanto mayor sea la capacidad de kilovatios, más energía" +
            " puede almacenar la batería, lo que permite recorrer distancias más largas sin necesidad de recargar. Además puede proporcionar más potencia, mejorando el rendimiendo en" +
            " aceleración y velocidad. En resumen, entre más kilovatios tenga nuestra batería, tendremos más capacidad de aceleración, pero también puede implicar un mayor tamaño, peso" +
            " y costo de la batería.";
    }
}


