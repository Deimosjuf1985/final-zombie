using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RayCastController : MonoBehaviour
//{
//    public int municion = 50; // un entero de municion que tiene un valor de 50

//    public int numbalas; // contador de zombies

//    void Update()
//    {
//        if (municion > 0) //si la municion es mayor a 0
//        {
//            if (Input.GetMouseButtonDown(0)) //si preciono fire click izquierdo del mouse
//            {
//                // aqui estoy realizando un raycast que dice que cuando precione el boton fire me dispara un rayo drawray con una fuerza de 100 y al enemi me llama un metodo de daño del enemy
//                RaycastHit hit; //variable hit del raycast
//                municion--; //si disparo la municion se me disminuye en 1
//                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
//                {
//                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
//                    if (hit.collider.GetComponent<Rigidbody>() != null)
//                    {
//                        hit.collider.GetComponent<Rigidbody>().AddForce(hit.normal * -100);
//                    }
//                    //if (hit.collider.GetComponent<Zombie>() != null)
//                    //{
//                    //    hit.collider.GetComponent<Zombie>().ReceiveDamage();
//                    //}
//                }
//                else
//                {
//                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); //rayo drawray color blaco
//                }
//            }
//        }
//        else if (municion == 0) // aqui le estoy diciendo que so la municion llega a 0 me inprima que debe recargar 
//        {
//            print("debes recargar");
//        }
//        numbalas = municion; // al numbalas del cambas le digo que es igual a la municion
//    }
//}