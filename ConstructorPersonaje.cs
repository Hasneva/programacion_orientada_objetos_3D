using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Nombre del desarrollador: Mariana Hasneva Vazquez Rebollo
Asignatura: Programación orientada a objetos
Profesor: Josue Israel Rivas Diaz
Script: Constructor Personaje
Descripcion: Indica todo elemento que un personaje tiene en su codigo y va a ser utilizado.*/

[System.Serializable] //Serializable: Indica que una clase o una estructura se pueden serializar.
public class ConstructorPersonaje
{
    //Es una variable de acceso publico que le asigna nombre, vida y daño al personaje.
    public string nombre; 
    public int vida; 
    public int daño;
    
    
    //El metodo movimiento registra los valores bases de dirección y desplazamiento que tendra un objeto intanciado.
    public void Movimiento(Rigidbody2D rb, float v) //Registra el movimiento del objeto aplicando una fuerza especifica. Rigidbody2D: el sprite se verá afectado por la gravedad y puede controlarse desde los scripts.
    {
        rb.velocity = new Vector2(v, rb.velocity.y); //Rb va a trabajar con velocidad y la va a aplicar en un vector 2 en el cual va a aplicar fuerza sobre X y va a mantener su fuerza natural sobre Y.
    }

    public void PlayAnimation(Animator anim, string np, bool estado) //Especifica el Animator a utilizar, que nombre debe tener y en que estado debe estar. PlayAnimation: reproduce la animacion. Animator: Interfaz para controlar el sistema de animación.
    {
        anim.SetBool(np, estado); //Declara la acción que va a hacer.
    }

    //Regresa la info de cuando se esta en el suelo y cuando no. | Detecta cuando los pies del Player1 entran en contacto con el suelo
    public bool EstoyEnPiso(Transform suelaZapatos,float numeroZapato, LayerMask terreno)
    {
        bool estaEnTierra; //Registra cuando esta en True o False
        estaEnTierra = Physics2D.OverlapCircle(suelaZapatos.position,numeroZapato, terreno); //Se declara e inicializa. Se registra informacion de la colision con el piso a traves de otro objeto. 

        return estaEnTierra; //Return: regresa el valor 
    }
    //Metodo 1 Pide dos parametros y revisa el tag o nombre y manda el mensaje. 
    // Comportamiento vacio que pide info de quien va a colisionar y hace comprobacion del resultado. 

    public void RegistroColision(string n, Collision2D other)
    {
        if (other.gameObject.tag==n || other.gameObject.name==n) //Se le indica quien es a traves de una etiqueta o un nombre.
        {
            Debug.Log("Haste pa'ya "); //Debug.Log: Registra un mensaje en la Consola de Unity si se detecta una colision.
        }
    }

  
  //Metodo 2 Utiliza un tercer parametro 'String mensaje'

    public void RegistroColision(string n, Collision2D other, string mensaje) //SobreCarga al metodo RegistroColision: A partir del mismo nombre obtener diferentes comportamientos.
    {
        if (other.gameObject.tag == n || other.gameObject.name == n) //Se le indica quien es a traves de una etiqueta o un nombre.
        {
            Debug.Log(mensaje); //Retorna el mensaje indicado en el parametro 'mensaje'
        }
    }

    //Metodo 3 Parametros con Arrey

    public void RegistroColision(string[] n, Collision2D other) //Se agregaron parametros de tipo arrey en string y mensaje 
    {
        var etiqueta = other.gameObject.tag; //Variable interna va a tomar el valor de other
        var velocidad = 0;

        //************Bloque 1 de info***********

        if (etiqueta == n[0]) //Si la Etiqueta es igual a 0...
        {
            velocidad = 5; //El valor de velocidad será 5
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * velocidad, ForceMode2D.Impulse); //El objeto con la etiqueta en 0 es el que reaccionara siendo impulsado a la derecha con el valor que tiene 'velocidad'

        }

        //************Bloque 2 de info***********

        else if (etiqueta == n[1])
        {
            var Player = GameObject.Find("Fairy"); //Player es igual a un objeto que se encuentra por nombre ''Fairy''
            velocidad = 10; //El Valor sera 10 en el caso de la etiqueta 1
            Player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * velocidad, ForceMode2D.Impulse); //Se lee el componente rigidbody y se le aplica una fuerza.
        }

    
    }

    //Metodo 4 Switch 

    public void RegistroColision(Collision2D other,string player) //Define el nombre con quien se va a colisionar y el collider.
    {

        var etiqueta = other.gameObject.tag; 
        var velocidad = 0; //Cambia de valor segun con que se colisione
        var Player = GameObject.Find(player); //Encuentra el objeto almacenado en el parametro Player

        switch (etiqueta) //Switch: Declaracion de condicion con diferentes casos de codigo
        {
            //*******CASO UNO*******
            case "Enchufe": //Se da en caso de que etiqueta tenga nombre "Enchufe"
                velocidad = 50; //Cambia la velocidad a valor de 50
                Debug.Log("El nombre del objeto es:" + other.gameObject.name); //Manda el Nombre almacenado en etiqueta
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * velocidad, ForceMode2D.Impulse); //Una vez localizado aplica una fuerza de empuje

                break; //Break rompe con el evento

            //******CASO DOS *******

            case "Bola":
                velocidad = 10000; //Velocidad en caso bola
                Debug.Log("El nombre del objeto es:" + other.gameObject.name); //mensaje de la consola 

                if (Player.transform.position.x < other.gameObject.transform.position.x) //Si esta en lado izquierdo o derecho aplica una fuerza de empuje
                {
                    Player.GetComponent<Rigidbody2D>().AddForce(Player.transform.right * -velocidad); //añadir fuerza desde el rigidbody
                }

                else if (Player.transform.position.x > other.gameObject.transform.position.x) //Si esta en lado izquierdo o derecho aplica una fuerza de empuje
                {
                    Player.GetComponent<Rigidbody2D>().AddForce(Player.transform.right * velocidad); //añadir fuerza desde el rigidbody
                }

                    break; //Break: Termina con el loop.

            //******CASO TRES******

            case "Pinchito":

                break; //Break: Termina con el loop.

            //******CASO POR DEFAULT*****
            default:
                break; //Break: Termina con el loop.
        }

    }


}
