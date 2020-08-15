using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

/*Nombre del desarrollador: Mariana Hasneva Vázquez Rebollo
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
    public Transform startPoint;
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

    public bool piso;
    public bool jump;
    public bool jugar = true;
    #endregion

    private void Awake()
    {
        suelaZapatos = GameObject.Find("zapatos").GetComponent<Transform>();
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

            //Controles de movimeinto para Player 2

            #region Control Movimiento Izquierda Derecha

            float movimientoZ = Input.GetAxis("Vertical2") * Velocidad;
            float movimientoX = Input.GetAxis("Horizontal2") * Velocidad;
            bool botonDisparo = Input.GetButton("Fire2");

            bool movimientoIzquierdo = movimientoX <= -1 ? true : false;
            bool movimientoDerecho = movimientoX >= 1 ? true : false;
            bool movimientoAdelante = movimientoZ >= 1 ? true : false;
            bool movimientoAtras = movimientoZ <= -1 ? true : false;

            Bob.Movimiento(fisicasRB, movimientoZ);
            Bob.MovimientoHorizontal(fisicasRB, movimientoX);

            //Animaciones de Player 2 

            Bob.PlayAnimation(animarFairy, "Velocidad", movimientoAdelante);
            Bob.PlayAnimation(animarFairy, "Atras", movimientoAtras);
            Bob.PlayAnimation(animarFairy, "MovimientoDerecho", movimientoDerecho);
            Bob.PlayAnimation(animarFairy, "MovimientoIzquierdo", movimientoIzquierdo);

            if (botonDisparo)
            {
                Bob.PlayAnimation(animarFairy, "Ataque", botonDisparo);
            }



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



} //Fin de class
