using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NPC.Enemy;
using NPC.Ally;

[RequireComponent(typeof(FPS))]         //  Movement component
[RequireComponent(typeof(FPSmouse))]    //  Vision component
[RequireComponent(typeof(Rigidbody))]   //  Rigidbody component
public class Hero : MonoBehaviour
{
    readonly float speed; // Speed of movement
    GameManager gameManager;

    ZombieInfo zombieInfo;
    CitizenInfo citizenInfo;

    public int healt = 100;

    public static bool heroDead = false;
    public Hero()
    { // Constructor
        speed = Initializer.heroSpeed; // Initialize
    }

    void Start()
    {
        //gameObject.GetComponent<RayCastController>();
        gameObject.GetComponent<Rigidbody>().freezeRotation = enabled;      //cancelar rotacipon
        gameObject.GetComponent<FPSmouse>().lookCase = 2;                   // vision del jugador Y

        Camera.main.gameObject.AddComponent<FPSmouse>().lookCase = 1;       // vision del jugador X
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);     // Same position
        Camera.main.transform.SetParent(gameObject.transform);              // Camera is child

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>(); // Referencia
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public int municion = 50; // un entero de municion que tiene un valor de 50

    public int numbalas; // contador de zombies

    void Update()
    {
        gameObject.GetComponent<FPS>().Movement(speed); // movieminto jugador
        ZombieMessage(); // Dialogue
        if (municion > 0) //si la municion es mayor a 0
        {
            if (Input.GetMouseButtonDown(0)) //si preciono fire click izquierdo del mouse
            {
                // aqui estoy realizando un raycast que dice que cuando precione el boton fire me dispara un rayo drawray con una fuerza de 100 y al enemi me llama un metodo de daño del enemy
                RaycastHit hit; //variable hit del raycast
                municion--; //si disparo la municion se me disminuye en 1
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    if (hit.collider.GetComponent<Rigidbody>() != null)
                    {
                        hit.collider.GetComponent<Rigidbody>().AddForce(hit.normal * -100);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); //rayo drawray color blaco
                }
            }
        }
        else if (municion == 0) // aqui le estoy diciendo que so la municion llega a 0 me inprima que debe recargar 
        {
            print("recargue");
        }
        numbalas = municion; // al numbalas del cambas le digo que es igual a la municion
    }

    void ZombieMessage()
    {
        for (int i = 0; i < GameManager.npcList.Count; i++)
        {
            if (GameManager.npcList[i].GetComponent<Zombie>())
            {
                float distance = Vector3.Distance(GameManager.npcList[i].transform.position, transform.position);

                if (distance < 5 && heroDead == false)
                {
                    zombieInfo = GameManager.npcList[i].GetComponent<Zombie>().GetInfo(); 
                    gameManager.zombieDialogue.text = "Te comeré tu " + zombieInfo.bodyParts;
                    gameManager.zomDiagAnimator.SetBool("abierto", true); // muestra mensaje
                }
                else if (heroDead == true)
                {
                    gameManager.zomDiagAnimator.SetBool("abierto", false); // esconde mensaje
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Zombie>())
        {
            DestroyHero();  //  metodo de llamda
        }

        if (collision.gameObject.GetComponent<Citizen>())
        {
            citizenInfo = collision.gameObject.GetComponent<Citizen>().GetInfo();
            NPCs cit = collision.gameObject.GetComponent<NPCs>();
            gameManager.citizenDialogue.text = "buenas, soy  " + citizenInfo.names + " y tengo " + cit.info.age + " años";
            StartCoroutine(CitizenMessage());
        }
    }

    public void DestroyHero()
    {
        heroDead = true;
        gameManager.gameOver.enabled = true;                                    //  activa texto en pantalla
        gameObject.GetComponent<Collider>().enabled = false;                    //  desactiva componentes
        gameObject.GetComponent<FPSmouse>().enabled = false;                    //  
        Camera.main.gameObject.GetComponent<FPSmouse>().enabled = false;        //

        Hero h = gameObject.GetComponent<Hero>();                               //  busco componete hero
        Destroy(h);                                                             //  destruye el componente
    }

    IEnumerator CitizenMessage()
    {
        gameManager.citDiagAnimator.SetBool("abierto", true); // muestra mensaje
        yield return new WaitForSeconds(3f);
        gameManager.citDiagAnimator.SetBool("abierto", false); // oculta mensaje
    }
}