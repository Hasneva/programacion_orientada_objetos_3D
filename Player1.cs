using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

/*Nombre del desarrollador: Mariana Hasneva Vazquez Rebollo
Asignatura: Programación orientada a objetos
Profesor: Josue Israel Rivas Diaz
Script: Player1
Descripcion: Le da movimiendo al Player1 y activa los sprites, le da registro a la vida y daño.*/

public class Player1 : MonoBehaviour
{
    // 1. Se llama al plano o blueprint ConstructorPersonaje
    // 2. Se le da un Id Bob
    // 3. Se inicializa el dato como nuevo objeto "new" ConstructorPersonaje

    #region Variables
    public Transform startPoint; //Posicion Inicial
    public ConstructorPersonaje Bob = new ConstructorPersonaje();
    public static Animator animarDINO; //Declara el componente Animator: Interfaz para controlar el sistema de animación




    Rigidbody fisicasRB; //Declara componente Rigidbody2D se maneje a traves de fisicas. RigidBody2D: el sprite se verá afectado por la gravedad y puede controlarse desde los scripts.


    public static int vidaPlayer = 100;
    [SerializeField] //SerializeField: Hace visibles las variables en el inspector y se mantienen privados y protegidos. 
    Transform suelaZapatos; //Se declara el componente suela zapatos 
    [SerializeField]
    float numeroZapato;
    [SerializeField]
    LayerMask terreno;
    [SerializeField]
    float fuerzaSaltoPersonaje; //Declara la magnitud de fuerza de salto del personaje
    [SerializeField]
    float Velocidad = 5f;
    [SerializeField]
    float VelocidadActual;

    public bool piso;
    public bool jump;
    public bool cambiarAnimacion;
    public bool jugar = true;
    public bool boolBaile;
    public int cambioBaile;
    #endregion

    private void Awake()
    {
        suelaZapatos = GameObject.Find("zapatos").GetComponent<Transform>();
        Velocidad = VelocidadActual;
    }
    // Start is called before the first frame update
    void Start()
    {

        //---------------Datos de Personaje ---------------
        //Se inicializan los datos de Bob | Se accede a ellos mediante punto 
        Bob.nombre = "MayorTom"; //Se asigna un nombre al objeto Bob atraves de cadena de texto | Da acceso a un dato publico de tipo cadena de texto en al Constructor prsonaje.
        gameObject.name = Bob.nombre; //Manda a llamar al objeto GameObject y cuando da play toma el dato de Bob.Nombre | Del objeto creado que tiene como nombre bob, le da ese nombre al objeto en Unity
        Bob.vida = vidaPlayer; //Referencia el valor de inforamacion de vida 10
        Bob.daño = 2; //Referencia valor de daño de 2


        //-------------------------------

        //----------- Inicializacion de Componentes ---------

        animarDINO = GetComponent<Animator>(); //Llama a la variable y lee el componente como Animator

        fisicasRB = GetComponent<Rigidbody>(); //Lee el componente y se inicializa 
    }

    // Update is called once per frame
    void Update()
    {
        if (jugar == true)
        {
            Bob.vida = vidaPlayer;

            piso = Bob.EstoyEnPiso(suelaZapatos, numeroZapato, terreno);

            //Controles de movimeinto

            #region Control Movimiento Izquierda Derecha

            float movimientoZ = Input.GetAxis("Vertical") * Velocidad;
            float movimientoX = Input.GetAxis("Horizontal") * Velocidad;
            bool botonDisparo = Input.GetButton("Fire1");  //Control de disparo

            bool movimientoIzquierdo = movimientoX <= -1 ? true : false;
            bool movimientoDerecho = movimientoX >= 1 ? true : false;
            bool movimientoAdelante = movimientoZ >= 1 ? true : false;
            bool movimientoAtras = movimientoZ <= -1 ? true : false;

            Bob.Movimiento(fisicasRB, movimientoZ);
            Bob.MovimientoHorizontal(fisicasRB, movimientoX);

            //Activa las animaciones del Player
            Bob.PlayAnimation(animarFairy, "Velocidad", movimientoAdelante);
            Bob.PlayAnimation(aanimarFairy, "Atras", movimientoAtras);
            Bob.PlayAnimation(animarFairy, "MovimientoDerecho", movimientoDerecho);
            Bob.PlayAnimation(animarFairy, "MovimientoIzquierdo", movimientoIzquierdo);

            if (botonDisparo)
            {
                Bob.PlayAnimation(animarFairy, "Ataque", botonDisparo);
            }


            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Input.mousePosition.y * 5f, transform.eulerAngles.z); //Lee la posiscion del Mouse sobre el eje horizontal para provocar que el jugador gire

            if (transform.position.y <= 0)
            {
                Velocidad = VelocidadActual;
            }

            else if (transform.rotation.y >= 180)
            {
                Velocidad = -VelocidadActual;
            }

            //Si el player se encuentra en el piso y la tecla espacio es presionada este pasa salto a verdadero
            if (piso)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jump = true;
                }

                else
                {
                    jump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                CambiarPesos();
            }

            //Si la letra B es presionada el Player cambia pesos y activa el baile
            if (Input.GetKeyDown(KeyCode.B))
            {
                CambiarPesos(boolBaile);
            }

            if (cambiarAnimacion)
            {
                animarFairy.SetLayerWeight(0, 0);
                animarFairy.SetLayerWeight(1, 0);
                animarFairy.SetLayerWeight(2, 1);//Asigna el peso de cada capa
            }

            else
            {
                animarFairy.SetLayerWeight(0, 1);
                animarFairy.SetLayerWeight(1, 1);
                animarFairy.SetLayerWeight(2, 0);
            }

            if (boolBaile)
            {
                animarFairy.SetLayerWeight(0, 0);
                animarFairy.SetLayerWeight(1, 0);
                animarFairy.SetLayerWeight(2, 0);
                animarFairy.SetLayerWeight(3, 1);//Asigna el peso de cada capa Regresa a la capa 3
                if (Input.GetKeyDown(KeyCode.Alpha1)) //Cuando cambia el peso da acceso a Alpha1
                {
                    cambioBaile++;
                    if (cambioBaile >= 3)
                    {
                        cambioBaile = 0;

                    }

                    animarFairy.SetInteger("numeroDanza", cambioBaile);
                }
            }

            else
            {
                animarFairy.SetLayerWeight(0, 1); //Regresa a la capa 1
                animarFairy.SetLayerWeight(1, 1);
                animarFairy.SetLayerWeight(2, 0);
                animarFairy.SetLayerWeight(3, 0);
            }



            #endregion

        }







    }
    //Es llamado cada frame y mantiene la lectura de cuadros en 50
    //FixedUpdate es ideal para registrar fuerzas que imitan la fisica | Mantiene estable la lectura de cuadros a 50 frames y esto estabiliza el motor de fisicas.
    private void FixedUpdate()
    {

        if (jump)
        {
            fisicasRB.AddForce(Vector2.up * fuerzaSaltoPersonaje, ForceMode.Impulse);
        }

        //Si presiono tecla space           y  bob esta en piso
        if (Input.GetKeyDown(KeyCode.Space) && Bob.EstoyEnPiso(suelaZapatos, numeroZapato, terreno))
        {
            //Entonces aplica la fuerza
            fisicasRB.AddForce(Vector2.up * fuerzaSaltoPersonaje, ForceMode.Impulse); //Agrega una fuerza en una direccion especifica por una magnitud especifica
        }

    }

    void CambiarPesos() //Cuando presiona el boton la animacion cambia a true o false
    {
        if (!cambiarAnimacion)
            cambiarAnimacion = true;

        else
            cambiarAnimacion = false;


    }

    void CambiarPesos(bool cambiar) //Cuando presiona el boton la animacion cambia a true o false
    {
        if (!cambiar)
            cambiar = true;

        else
            cambiar = false;


    }
} //Fin de class
