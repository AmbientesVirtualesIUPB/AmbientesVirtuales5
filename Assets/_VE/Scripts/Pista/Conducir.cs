using System;
using UnityEngine;
using UnityEngine.UI;

public class Conducir : MonoBehaviour
{

  //  public MovimientoSexi movimientoJugador; // Referencia al script MovimientoSexi

   
    public void PresionarAcelerador() => throttlePTI = true;
    public void SoltarAcelerador() => throttlePTI = false;

    public void PresionarReversa() => reversePTI = true;
    public void SoltarReversa() => reversePTI = false;

    public void GirarIzquierda() => turnLeftPTI = true;
    public void SoltarGiroIzquierda() => turnLeftPTI = false;

    public void GirarDerecha() => turnRightPTI = true;
    public void SoltarGiroDerecha() => turnRightPTI = false;

    public void FrenoMano() => handbrakePTI = true;
    public void SoltarFrenoMano() => handbrakePTI = false;

    //CAR SETUP
    [Header("CONFIGURACION COCHE")]
    [Space(10)]
    [Range(20, 190)]
    public int              maxSpeed = 30; // La velocidad máxima que puede alcanzar el coche en km/h.
    [Range(10, 120)]
    public int              maxReverseSpeed = 20; // La velocidad máxima que puede alcanzar el coche en reversa dada en km/h
    [Range(1, 10)]
    public int              accelerationMultiplier = 2; // Qué tan rápido puede acelerar el auto. 1 es una aceleración lenta y 10 es la más rápida
    [Space(10)]
    [Range(10, 45)]
    public int              maxSteeringAngle = 27; // El ángulo máximo que pueden alcanzar los neumáticos al girar el volante.
    [Range(0.1f, 1f)]
    public float            steeringSpeed = 0.5f; // Qué tan rápido gira el volante
    [Space(10)]
    [Range(100, 600)]
    public int              brakeForce = 300; // La fuerza de los frenos de las ruedas
    [Range(1, 10)]
    public int              decelerationMultiplier = 2; // Qué tan rápido desacelera el automóvil cuando el usuario no usa el acelerador
    [Range(1, 10)]
    public int              handbrakeDriftMultiplier = 5; // Cuánto agarre pierde el automóvil cuando el usuario pisa el freno de mano
    [Space(10)]
    public Vector3          bodyMassCenter; // Este es un vector que contiene el centro de masa del automóvil. Recomiendan establecer este valor
                                            // en los puntos x = 0 y z = 0 de tu coche. Puedes seleccionar el valor que quieras en el eje y,
                                            // sin embargo, debes notar que cuanto mayor es este valor, más inestable se vuelve el auto.
                                            // Normalmente el valor de y va de 0 a 1,5.

    //WHEELS
    [Space(20)]
    [Header("LLANTAS")]
    [Space(10)]
    // Variables para almacenar los Mesh de las ruedas con sus respectivos colliders
    public MeshFilter       frontLeftMesh; // Mesh rueda delantera izquierda
    public WheelCollider    frontLeftCollider; // Collider rueda delantera izquierda
    [Space(10)]
    public MeshFilter       frontRightMesh; // Mesh rueda delantera derecha
    public WheelCollider    frontRightCollider;// Collider rueda delantera derecha
    [Space(10)]
    public MeshFilter       rearMesh; // Mesh rueda trasera
    public WheelCollider    rearCollider;// Collider rueda trasera

    //PARTICLE SYSTEMS
    [Space(20)]
    [Header("EFECTOS PARTICULAS")]
    [Space(10)]
    public bool             useEffects = false; //Para indicar si se va utilizar o no el sistema de particulas
    public ParticleSystem   RLWParticleSystem; // Particulas de humo para los neumaticos
    public TrailRenderer    RLWTireSkid; // Particulas para el derrape de los neumaticos

    //SPEED TEXT (UI)
    [Space(20)]
    [Header("TEXTO UI")]
    [Space(10)]
    // Variables para almacenar la velocidad del usuario y se muestre en pantalla
    public bool             useUI = false;
    public Text             carSpeedText; // Variable para asignar el texto de la UI

    //SOUNDS
    [Space(20)]
    [Header("SONIDOS")]
    [Space(10)]
    // Variables para configurar el sonido del auto o derrape de los neumaticos
    public bool             useSounds = false;
    public AudioSource      carEngineEnd; // Sonido de apagado del motor
    public AudioSource      carEngineSound; // Sonido del motor
    public AudioSource      tireScreechSound; // Sonido de los neumaticos al derrapar
    float                   initialCarEngineSoundPitch; // Para almacenar el tono inicial del sonido carEngineSound y poderlo modificar

    //CONTROLS
    [Space(20)]
    [Header("CONTROLES")]
    [Space(10)]
    //Variables para configurar los controles tactil para movil
    public bool throttlePTI;
    public bool reversePTI;
    public bool turnRightPTI;
    public bool turnLeftPTI;
    public bool handbrakePTI;
    //
    [HideInInspector]
    public bool             acelerando; // Para validar que solo descargar la bateria mientras este acelerando o reversando
    [HideInInspector]
    public bool             descargado; // Para apagar el vehiculo en caso de ser necesario, en este caso solo puede manejar cuando la interfaz encienda
    [HideInInspector]
    public bool             sinLlantas; // Para validar si tiene o no tiene llantas, sino tiene no puede manejar
    [HideInInspector]
    public int              contador; // Contador para solo hacer dos validaciones en el LatedUpdate

    //DATOS COCHE
    [HideInInspector]
    public float            carSpeed; // Velocidad actual del coche
    [HideInInspector]
    public bool             isDrifting; // Saber si esta derrapando o no
    [HideInInspector]
    public bool             isTractionLocked; // Saber si la traccion del coche esta bloqueada o no

    //PRIVATE VARIABLES
    /*
    IMPORTANT: No darles valores a estas variables
    */
    Rigidbody               carRigidbody; // Stores the car's rigidbody.
    float                   steeringAxis; // Se utiliza para saber si el volante ha alcanzado el valor máximo. Va de -1 a 1.
    float                   throttleAxis; // Se utiliza para saber si el acelerador ha alcanzado el valor máximo. Va de -1 a 1.
    float                   driftingAxis;
    float                   localVelocityZ;
    float                   localVelocityX;
    public bool             deceleratingCar;
    bool                    touchControlsSetup = false;

    /*
    Las siguientes variables se utilizan para almacenar información sobre la fricción lateral de las ruedas como 
    (extremumSlip,extremumValue, asymptoteSlip, asymptoteValue and stiffness). Cambiamos estos valores y haz que el auto comience a derrapar
    */
    WheelFrictionCurve      FLwheelFriction;
    float                   FLWextremumSlip;
    WheelFrictionCurve      FRwheelFriction;
    float                   FRWextremumSlip;
    WheelFrictionCurve      RLwheelFriction;
    float                   RLWextremumSlip;


    private Conducir conducir;
    // Start is called before the first frame update
    void Start()
    {
        /*if (movimientoJugador == null)
        {
            Debug.LogError("MovimientoSexi no asignado en Conducir. Arrástralo en el Inspector.");
        }*/

        carRigidbody = GetComponent<Rigidbody>(); // Inicializamos el componenete rigibody para acceder a su masa
        carRigidbody.mass = 1400 + DatosCanvasInformativo.pesoFurtivo; // Asignamos a la masa el valor adicional dependiendo del peso total del furtivo
        maxSpeed = maxSpeed + DatosCanvasInformativo.voltiosVelocidad; // Asignamos a la velocidad el valor adicional dependiendo de la bateria elegida
        accelerationMultiplier = accelerationMultiplier + DatosCanvasInformativo.kilovatiosAceleracion; // Asignamos a la aceleracion el valor adicional dependiendo de la bateria elegida
        brakeForce = brakeForce + DatosCanvasInformativo.llantasFrenado; // Asignamos a la fuerza de frenado el valor adicional dependiendo de las llantas elegida
        bodyMassCenter.y = DatosCanvasInformativo.centroMasa; // Asignamos el centro de masa segun la carroceria elegida

        //Nos aseguramos de que estos dos audios inicien desactivados
        //carEngineSound.enabled = false; // Desactivamos el sonido del apagado del motor
        //carEngineEnd.enabled = false; // Damos play al sonido de detener el motor

        //En esta parte, configuramos el valor 'carRigidbody' con el Rigidbody adjunto a este
        //Además, definimos el centro de masa del coche con el Vector3 dado en el inspector
        carRigidbody = gameObject.GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = bodyMassCenter;

        // Configuración inicial para calcular el valor de derrape del coche. Esta parte podría parecer un poco
        // complicado, pero lo unico que se realiza es guardar el valor predeterminado de friccion del coche
        // para que podamos establecer un valor de derrape apropiado más adelante.
      FLwheelFriction = new WheelFrictionCurve();
        FLwheelFriction.extremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
        FLWextremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
        FLwheelFriction.extremumValue = frontLeftCollider.sidewaysFriction.extremumValue;
        FLwheelFriction.asymptoteSlip = frontLeftCollider.sidewaysFriction.asymptoteSlip;
        FLwheelFriction.asymptoteValue = frontLeftCollider.sidewaysFriction.asymptoteValue;
        FLwheelFriction.stiffness = frontLeftCollider.sidewaysFriction.stiffness;
      FRwheelFriction = new WheelFrictionCurve();
        FRwheelFriction.extremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
        FRWextremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
        FRwheelFriction.extremumValue = frontRightCollider.sidewaysFriction.extremumValue;
        FRwheelFriction.asymptoteSlip = frontRightCollider.sidewaysFriction.asymptoteSlip;
        FRwheelFriction.asymptoteValue = frontRightCollider.sidewaysFriction.asymptoteValue;
        FRwheelFriction.stiffness = frontRightCollider.sidewaysFriction.stiffness;
      RLwheelFriction = new WheelFrictionCurve();
        RLwheelFriction.extremumSlip = rearCollider.sidewaysFriction.extremumSlip;
        RLWextremumSlip = rearCollider.sidewaysFriction.extremumSlip;
        RLwheelFriction.extremumValue = rearCollider.sidewaysFriction.extremumValue;
        RLwheelFriction.asymptoteSlip = rearCollider.sidewaysFriction.asymptoteSlip;
        RLwheelFriction.asymptoteValue = rearCollider.sidewaysFriction.asymptoteValue;
        RLwheelFriction.stiffness = rearCollider.sidewaysFriction.stiffness;

        // Guardamos el tono inicial del sonido del motor del coche
        if (carEngineSound != null)
        {
            initialCarEngineSoundPitch = carEngineSound.pitch;
        }

        // Invocamos 2 métodos dentro de este script.. CarSpeedUI() cambia el texto del objeto UI que almacena la velocidad del carro
        // y CarSounds() controla los sonidos del motor y de derrape 
        // Ambos métodos se invocan en 0 segundos y se llaman repetidamente cada 0,1 segundos.
        if (useUI)
        {
            InvokeRepeating("CarSpeedUI", 0f, 0.1f);
        }
        else if (!useUI)
        {
            if (carSpeedText != null)
            {
                carSpeedText.text = "0";                      
            }
        }

        if (useSounds)
        {
            InvokeRepeating("CarSounds", 0f, 0.1f);
        }
        else if (!useSounds)
        {
            if (carEngineSound != null)
            {
                carEngineSound.Stop();
            }
            if (tireScreechSound != null)
            {
                tireScreechSound.Stop();
            }
        }

        if (!useEffects)
        {
            if (RLWParticleSystem != null)
            {
                RLWParticleSystem.Stop();
            }

            if (RLWTireSkid != null)
            {
                RLWTireSkid.emitting = true;
            }
        }

        if (ConfiguracionGeneral.configuracionDefault.plataformaObjetivo == Plataforma.Movil || ConfiguracionGeneral.configuracionDefault.plataformaObjetivo == Plataforma.VR)
        {
            touchControlsSetup = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        acelerando = false;
        //DATOS DEL COCHE

        // Determinamos la velocidad del coche.
        carSpeed = (2 * Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60) / 1000;
        // Guarde la velocidad local del automóvil en el eje x. Se utiliza para saber si el coche se está derrapando.
        localVelocityX = transform.InverseTransformDirection(carRigidbody.velocity).x;
        // Guarde la velocidad local del automóvil en el eje z. Se utiliza para saber si el coche va hacia adelante o hacia atrás.
        localVelocityZ = transform.InverseTransformDirection(carRigidbody.velocity).z;

        //FISICAS DEL COCHE
        /*      
        La siguiente parte trata sobre el controlador del automóvil. Primero, verifica si el usuario desea usar controles táctiles 
        (por ejemplo dispositivos móviles) o controles de entrada analógica (WASD + Space)

        Los siguientes métodos se llaman cada vez que se presiona una determinada tecla. Por ejemplo, en el primer 'if' llamamos al
        método GoForward() si el usuario ha presionado W.

        En esta parte del código especificamos qué debe hacer el automóvil si el usuario presiona W (acelerador), S (marcha atrás),
        A (girar a la izquierda), D (girar a la derecha) o Barra espaciadora (freno de mano).
        */
        if ((ConfiguracionGeneral.configuracionDefault.plataformaObjetivo == Plataforma.Movil || ConfiguracionGeneral.configuracionDefault.plataformaObjetivo == Plataforma.VR) && touchControlsSetup)
        {

            if (throttlePTI)
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                GoForward();
            }
            if (reversePTI)
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                GoReverse();
            }

            if (turnLeftPTI)
            {
                TurnLeft();
            }
            if (turnRightPTI)
            {
                TurnRight();
            }
            if (handbrakePTI)
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                Handbrake();
            }
            if (!handbrakePTI)
            {
                RecoverTraction();
            }
            if ((!throttlePTI && !reversePTI))
            {
                ThrottleOff();
            }
            if ((!reversePTI && !throttlePTI) && !handbrakePTI && !deceleratingCar)
            {
                InvokeRepeating("DecelerateCar", 0f, 0.1f);
                deceleratingCar = true;
            }
            if (!turnLeftPTI && !turnRightPTI && steeringAxis != 0f)
            {
                ResetSteeringAngle();
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                GoForward();
            }

            if (Input.GetKey(KeyCode.S))
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                GoReverse();
            }

            if (Input.GetKey(KeyCode.A))
            {
                TurnLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                TurnRight();
            }

            if (Input.GetKey(KeyCode.F))
            {
                Brakes();
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                CancelInvoke("DecelerateCar");
                deceleratingCar = false;
                Handbrake();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                RecoverTraction();
            }
            if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))
            {
                ThrottleOff();
            }
            if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) && !deceleratingCar)
            {
                InvokeRepeating("DecelerateCar", 0f, 0.1f);
                deceleratingCar = true;
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && steeringAxis != 0f)
            {
                ResetSteeringAngle();
            }
            
        }
        // Llamamos al metodo AnimateWheelMeshes() para hacer coincidir los movimientos del colisionador de ruedas con las mallas 3D de las ruedas.
        AnimateWheelMeshes();
    }

    private void LateUpdate()
    {
        if (contador < 4) // Mientras que el contador sea menor que dos para evitar que el ciclo se repita mas de dos veces
        {
            // Tomamos el nombre del mesh que tengan asignadas las ruedas actualmente
            string nombreRuedaDelanteraIzq = frontLeftMesh.mesh.name;
            string nombreRuedaDelanteraDere = frontRightMesh.mesh.name;
            string nombreRuedaTrasera = rearMesh.mesh.name;
            
            // Validamos si alguno de ellos tiene el nombre whe_1_trasera Instance que corresponde a que no tiene ruedas
            if (nombreRuedaDelanteraIzq == "cro_#1 Instance" || nombreRuedaDelanteraDere == "cro_#1 Instance" || nombreRuedaTrasera == "cro_#1 Instance")
            {
                sinLlantas = true; // Indicamos que no puede andar
            }
            else
            {
                sinLlantas = false;
            }
            contador ++;
        }  
    }
    

    // Este método convierte los datos de velocidad del automóvil de flotante a cadena y luego establece el texto de la interfaz de usuario carSpeedText con este valor
    public void CarSpeedUI()
    {
        if (useUI)
        {      
            try
            {
                float absoluteCarSpeed = Mathf.Abs(carSpeed); // math.abs retorna el valor absoluto de un numero
                carSpeedText.text = Mathf.RoundToInt(absoluteCarSpeed).ToString();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
    }

    // Este método controla los sonidos del coche. Por ejemplo, el motor del automóvil sonará lento cuando la velocidad del automóvil es baja
    // porque el tono del sonido estará en su punto más bajo. Por otro lado, sonará rápido cuando la velocidad del automóvil sea alta porque
    // el tono del sonido será la suma del tono inicial + la velocidad del auto dividida por 100f.
    // Aparte de eso, tireScreechSound se reproducirá cada vez que el automóvil comience a derrapar o perder tracción.
    public void CarSounds()
    {
        if (useSounds)
        {
            try
            {
                if (carEngineSound != null)
                {
                    float engineSoundPitch = initialCarEngineSoundPitch + (Mathf.Abs(carRigidbody.velocity.magnitude) / 25f);
                    carEngineSound.pitch = engineSoundPitch;
                }
                if ((isDrifting) || (isTractionLocked && Mathf.Abs(carSpeed) > 0.12f))
                {
                    if (!tireScreechSound.isPlaying)
                    {
                        tireScreechSound.Play();
                    }
                }
                else if ((!isDrifting) && (!isTractionLocked || Mathf.Abs(carSpeed) < 0.12f))
                {
                    tireScreechSound.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        else if (!useSounds)
        {
            if (carEngineSound != null && carEngineSound.isPlaying)
            {
                carEngineSound.Stop();
            }
            if (tireScreechSound != null && tireScreechSound.isPlaying)
            {
                tireScreechSound.Stop();
            }
        }
    }

    //
    //METODOS DE DIRECCION
    //

    //El siguiente método gira las ruedas delanteras del coche hacia la izquierda. La velocidad de este movimiento dependerá de la variable steeringSpeed.
    public void TurnLeft()
    {
        steeringAxis = steeringAxis - (Time.deltaTime * 10f * steeringSpeed);
        if (steeringAxis < -1f)
        {
            steeringAxis = -1f;
        }
        var steeringAngle = steeringAxis * maxSteeringAngle;
        frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
        frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
    }

    //El siguiente método gira las ruedas delanteras del coche hacia la derecha. La velocidad de este movimiento dependerá de la variable steeringSpeed
    public void TurnRight()
    {
        steeringAxis = steeringAxis + (Time.deltaTime * 10f * steeringSpeed);
        if (steeringAxis > 1f)
        {
            steeringAxis = 1f;
        }
        var steeringAngle = steeringAxis * maxSteeringAngle;
        frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
        frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
    }

    // El siguiente método lleva las ruedas delanteras del automóvil a su posición predeterminada (rotación = 0). 
    // La velocidad de este movimiento dependerá de la variable steeringSpeed.
    public void ResetSteeringAngle()
    {
        if (steeringAxis < 0f)
        {
            steeringAxis = steeringAxis + (Time.deltaTime * 10f * steeringSpeed);
        }
        else if (steeringAxis > 0f)
        {
            steeringAxis = steeringAxis - (Time.deltaTime * 10f * steeringSpeed);
        }
        if (Mathf.Abs(frontLeftCollider.steerAngle) < 1f)
        {
            steeringAxis = 0f;
        }
        var steeringAngle = steeringAxis * maxSteeringAngle;
        frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
        frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
    }

    // Este método hace coincidir tanto la posición como la rotación de WheelColliders con WheelMeshes..
    void AnimateWheelMeshes()
    {
        try
        {
            Quaternion FLWRotation;
            Vector3 FLWPosition;
            frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
            frontLeftMesh.transform.position = FLWPosition;
            frontLeftMesh.transform.rotation = FLWRotation;

            Quaternion FRWRotation;
            Vector3 FRWPosition;
            frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
            frontRightMesh.transform.position = FRWPosition;
            frontRightMesh.transform.rotation = FRWRotation;

            Quaternion RLWRotation;
            Vector3 RLWPosition;
            rearCollider.GetWorldPose(out RLWPosition, out RLWRotation);
            rearMesh.transform.position = RLWPosition;
            rearMesh.transform.rotation = RLWRotation;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    //
    //METODOS DE MOTOR Y FRENADO
    //

    // Este método aplica un torque positivo a las ruedas para poder avanzar
    public void GoForward()
    {   
        
        acelerando = true;
        
        if (descargado) 
        {
            Brakes();
            return;
        }
        
        if (sinLlantas)
        {
            Brakes();
            return;
        }
        
        //Si las fuerzas aplicadas al cuerpo rígido en el eje 'x' son mayores que
        //3f, significa que el coche está perdiendo tracción, entonces el coche empezará a emitir sistemas de partículas.
        if (Mathf.Abs(localVelocityX) > 2.5f)
        {
            isDrifting = true;
            DriftCarPS();
        }
        else
        {
            isDrifting = false;
            DriftCarPS();
        }
        // La siguiente parte ajusta la potencia del acelerador a 1 suavemente.
        throttleAxis = throttleAxis + (Time.deltaTime * 3f);
        if (throttleAxis > 1f)
        {
            throttleAxis = 1f;
        }
        //Si el automóvil va hacia atrás, aplique los frenos para evitar comportamientos extraños.
        //Si la velocidad local en el eje 'z' es menor que -1f, entonces Es seguro aplicar torsión positiva para avanzar.
        if (localVelocityZ < -1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.RoundToInt(carSpeed) < maxSpeed)
            {
                //Aplique torque positivo en todas las ruedas para avanzar si no se ha alcanzado la velocidad máxima.
                frontLeftCollider.brakeTorque = 0;
                frontLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
                frontRightCollider.brakeTorque = 0;
                frontRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
                rearCollider.brakeTorque = 0;
                rearCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
            }
            else
            {
                // Si se ha alcanzado la velocidad máxima, deje de aplicar torsión a las ruedas
                // IMPORTANT: La variable maxSpeed ​​debe considerarse como una aproximación; La velocidad del coche podría ser un poco mayor de lo esperado.
                frontLeftCollider.motorTorque = 0;
                frontRightCollider.motorTorque = 0;
                rearCollider.motorTorque = 0;
            }
        }
    }

    // Este método aplica un torque negativo a las ruedas para poder retroceder.
    public void GoReverse()
    {
        acelerando = true;
        if (descargado)
        {
            Brakes();
            return;
        }

        if (sinLlantas)
        {
            Brakes();
            return;
        }

        //Si las fuerzas aplicadas al cuerpo rígido en el eje 'x' son mayores que 3f, significa que el automóvil está perdiendo tracción,
        //entonces el automóvil comenzará a emitir sistemas de partículas.

        if (Mathf.Abs(localVelocityX) > 2.5f)
        {
            isDrifting = true;
            DriftCarPS();
        }
        else
        {
            isDrifting = false;
            DriftCarPS();
        }
        // La siguiente parte ajusta la potencia del acelerador a -1 suavemente.
        throttleAxis = throttleAxis - (Time.deltaTime * 3f);
        if (throttleAxis < -1f)
        {
            throttleAxis = -1f;
        }
        //Si el coche sigue avanzando, aplique los frenos para evitar comportamientos extraños. Si la velocidad local en el eje 'z' es mayor que 1f,
        //entonces es seguro aplicar un torque negativo para retroceder.
        if (localVelocityZ > 1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.Abs(Mathf.RoundToInt(carSpeed)) < maxReverseSpeed)
            {
                //Aplique torque negativo en todas las ruedas para ir en reversa si no se ha alcanzado maxReverseSpeed.
                frontLeftCollider.brakeTorque = 0;
                frontLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
                frontRightCollider.brakeTorque = 0;
                frontRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
                rearCollider.brakeTorque = 0;
                rearCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
            }
            else
            {
                // Si se ha alcanzado maxReverseSpeed entonces deje de aplicar torsión a las ruedas.
                // IMPORTANT: La variable maxReverseSpeed ​​debe considerarse como una aproximación; La velocidad del coche podría ser un poco mayor de lo esperado.
                frontLeftCollider.motorTorque = 0;
                frontRightCollider.motorTorque = 0;
                rearCollider.motorTorque = 0;
            }
        }
    }

    //La siguiente función establece el torque del motor en 0 (en caso de que el usuario no esté presionando W o S).
    public void ThrottleOff()
    {
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearCollider.motorTorque = 0;
    }

    // El siguiente método desacelera la velocidad del automóvil de acuerdo con la variable multiplicadora de desaceleración,
    // donde 1 es la desaceleración más lenta y 10 es la más rápida. Este método es llamado por la función InvokeRepeating,
    // generalmente cada 0.1f cuando el usuario no está presionando W (acelerador), S (marcha atrás) o la barra espaciadora (freno de mano).
    public void DecelerateCar()
    {
        if (Mathf.Abs(localVelocityX) > 2.5f)
        {
            isDrifting = true;
            DriftCarPS();
        }
        else
        {
            isDrifting = false;
            DriftCarPS();
        }
        // La siguiente parte restablece la potencia del acelerador a 0 suavemente.
        if (throttleAxis != 0f)
        {
            if (throttleAxis > 0f)
            {
                throttleAxis = throttleAxis - (Time.deltaTime * 10f);
            }
            else if (throttleAxis < 0f)
            {
                throttleAxis = throttleAxis + (Time.deltaTime * 10f);
            }
            if (Mathf.Abs(throttleAxis) < 0.15f)
            {
                throttleAxis = 0f;
            }
        }
        carRigidbody.velocity = carRigidbody.velocity * (1f / (1f + (0.025f * decelerationMultiplier)));
        // Como queremos desacelerar el auto, vamos a quitar el torque de las ruedas
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearCollider.motorTorque = 0;
        // Si la magnitud de la velocidad del automóvil es inferior a 0,25 f (velocidad muy lenta), detenga el automóvil por completo
        // y cancele también la invocación de este método.
        if (carRigidbody.velocity.magnitude < 0.25f)
        {
            carRigidbody.velocity = Vector3.zero;
            CancelInvoke("DecelerateCar");
        }
    }

    // Esta función aplica el torque de frenado a las ruedas de acuerdo con la fuerza de frenado proporcionada por el usuario..
    public void Brakes()
    {
        frontLeftCollider.brakeTorque = brakeForce;
        frontRightCollider.brakeTorque = brakeForce;
        rearCollider.brakeTorque = brakeForce;
    }

    // Esta función se utiliza para hacer que el coche pierda tracción. Al usar esto, el coche comenzará a derrapar.
    // La cantidad de tracción perdida dependerá de la variable handbrakeDriftMultiplier. Si este valor es pequeño,
    // entonces el automóvil no se desviará demasiado, pero si es alto, entonces podría hacer que el automóvil tenga la sensación de estar sobre hielo.
    public void Handbrake()
    {
        CancelInvoke("RecoverTraction");
        // Vamos a empezar a perder tracción suavemente, ahí es donde nuestra variable 'driftingAxis' toma lugar
        // Esta variable comenzará desde 0 y alcanzará un valor máximo de 1, lo que significa que se ha alcanzado el valor máximo de derrape.
        // Aumentará suavemente utilizando la variable Time.deltaTime.
        driftingAxis = driftingAxis + (Time.deltaTime);
        float secureStartingPoint = driftingAxis * FLWextremumSlip * handbrakeDriftMultiplier;

        if (secureStartingPoint < FLWextremumSlip)
        {
            driftingAxis = FLWextremumSlip / (FLWextremumSlip * handbrakeDriftMultiplier);
        }
        if (driftingAxis > 1f)
        {
            driftingAxis = 1f;
        }
        //Si las fuerzas aplicadas al cuerpo rígido en el eje 'x' son mayores que 3f, significa que el automóvil perdió tracción,
        //entonces el automóvil comenzará a emitir sistemas de partículas.
        if (Mathf.Abs(localVelocityX) > 2.5f)
        {
            isDrifting = true;
        }
        else
        {
            isDrifting = false;
        }
        //Si el valor de 'driftingAxis' no es 1f, significa que las ruedas no han alcanzado su valor máximo de derrape,
        //por lo que vamos a seguir aumentando la fricción lateral de las ruedas hasta que driftingAxis = 1f
        if (driftingAxis < 1f)
        {
            FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            frontLeftCollider.sidewaysFriction = FLwheelFriction;

            FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            frontRightCollider.sidewaysFriction = FRwheelFriction;

            RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            rearCollider.sidewaysFriction = RLwheelFriction;
        }

        // Cada vez que el jugador usa el freno de mano, significa que las ruedas están bloqueadas, por lo que configuramos 'isTractionLocked = true'
        // y, como consecuencia, el auto comienza a emitir estelas para simular el derrape de las ruedas.
        isTractionLocked = true;
        DriftCarPS();

    }

    // Esta función se utiliza para emitir tanto los sistemas de partículas del humo de los neumáticos como los renderizadores de rastros de los derrapes
    // de los neumáticos dependiendo del valor de las variables bool 'isDrifting' e 'isTractionLocked'.
    public void DriftCarPS()
    {
        if (useEffects)
        {
            try
            {
                if (isDrifting)
                {
                    RLWParticleSystem.Play();
                }
                else if (!isDrifting)
                {
                    RLWParticleSystem.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }

            try
            {
                if ((isTractionLocked || Mathf.Abs(localVelocityX) > 5f) && Mathf.Abs(carSpeed) > 0.12f)
                {
                    RLWTireSkid.emitting = true;
                }
                else
                {
                    RLWTireSkid.emitting = false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
        else if (!useEffects)
        {
            if (RLWParticleSystem != null)
            {
                RLWParticleSystem.Stop();
            }
            if (RLWTireSkid != null)
            {
                RLWTireSkid.emitting = false;
            }
        }
    }

    // Esta función se utiliza para recuperar la tracción del coche cuando el usuario ha dejado de utilizar el freno de mano del coche.
    public void RecoverTraction()
    {
        isTractionLocked = false;
        driftingAxis = driftingAxis - (Time.deltaTime / 1.5f);
        if (driftingAxis < 0f)
        {
            driftingAxis = 0f;
        }

        //Si el valor de 'driftingAxis' no es 0f, significa que las ruedas no han recuperado la tracción.
        //Vamos a seguir disminuyendo el rozamiento lateral de las ruedas hasta llegar al agarre inicial del coche.
        if (FLwheelFriction.extremumSlip > FLWextremumSlip)
        {
            FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            frontLeftCollider.sidewaysFriction = FLwheelFriction;

            FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            frontRightCollider.sidewaysFriction = FRwheelFriction;

            RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
            rearCollider.sidewaysFriction = RLwheelFriction;

            Invoke("RecoverTraction", Time.deltaTime);

        }
        else if (FLwheelFriction.extremumSlip < FLWextremumSlip)
        {
            FLwheelFriction.extremumSlip = FLWextremumSlip;
            frontLeftCollider.sidewaysFriction = FLwheelFriction;

            FRwheelFriction.extremumSlip = FRWextremumSlip;
            frontRightCollider.sidewaysFriction = FRwheelFriction;

            RLwheelFriction.extremumSlip = RLWextremumSlip;
            rearCollider.sidewaysFriction = RLwheelFriction;

            driftingAxis = 0f;
        }
    }

   /* public void MontarVehiculo()
    {
        if (MovimientoSexi.Instance != null)
        {
            MovimientoSexi.Instance.ActivarJoystick(false);
        }
    }*/

    /*public void BajarVehiculo()
    {
        if (MovimientoSexi.Instance != null)
        {
            MovimientoSexi.Instance.ActivarJoystick(true);
        }
    }*/
}
