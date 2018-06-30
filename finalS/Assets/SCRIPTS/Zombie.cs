using System.Collections;
using UnityEngine;
using NPC.Ally;

namespace NPC
{
    namespace Enemy
    {
        public enum ZombieBodyParts { brazos, Hígado, cerebro, corazón, estomago }  //  Parts to eat

        public struct ZombieInfo
        {
            public ZombieBodyParts bodyParts;  //variable con las partes a comer
        }

        public class Zombie : NPCs
        {
            ZombieInfo zombieInfo;  //  variable for the return of information

            GameManager gameManager;

            public ZombieInfo GetInfo()  //  Function Info
            {
                return zombieInfo;    //  Return Info
            }

            void Start()
            {
                zombieInfo.bodyParts = (ZombieBodyParts)Random.Range(0, 5);   //  Random of body parts
                gameManager = FindObjectOfType<GameManager>();
                info.healt = 150;
                int colRand = Random.Range(1, 4);   //  Random para el color
                switch (colRand)    //  Color case
                {
                    case 1:
                        //  Set del Color
                        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                        break;
                    case 2:
                        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        break;
                    case 3:
                        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                        break;
                }
            }
            
            void Update()
            { // Methods that use rigid body
                Reaction(); //  Pursue citizens and hero
                States();   //  States (idle, moving, rotating)
            }

            void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.GetComponent<Citizen>())   //  If the object has a citizen component
                {
                    Citizen c = collision.gameObject.GetComponent<Citizen>();   //  Get the component
                    Zombie z = c;   //  The component is now a Zombie
                    gameManager.zomCount++;
                    gameManager.citCount--;
                    print(z);
                }

                if (collision.gameObject.GetComponent<Hero>())
                {
                    if (hero.hideFlags <= 0) reaction = false;
                }

                if (collision.gameObject.tag == "Bullet")
                {

                }
            }

            int Attacked(int damage)
            {
                return info.healt -= damage;
            }
        }
    }
}