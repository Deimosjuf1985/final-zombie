using UnityEngine;
using NPC.Enemy;

namespace NPC
{
    namespace Ally
    {
        public enum Names { Camilo = 1, Arnulfo, Mariano, ROBIN, Prudencio, Cristo, Mariana, Joselina,Martina, campoelias, Tom,
            Yajire, michael, leny, alejandra, claudia, mariajose, Pedrito, nini,  }   //  nombres de objeto

        public struct CitizenInfo
        {
            public Names names; // Vnombres del enumerador
        }

        public class Citizen : NPCs
        {
            CitizenInfo citizenInfo; // Variable that contains the information contained in the structure

            void Start()
            {
                citizenInfo.names = (Names)Random.Range(1, 21); // Random enum names
            }

            public CitizenInfo GetInfo()
            { 
                return citizenInfo; 
            }

            void Update()
            { // Methods that use rigid body
                States();   // States (idle, moving, rotating)
                Reaction(); // Scape from the zombie
            }

            public override void Reaction()
            { // Escape from the zombies
                for (int i = 0; i < GameManager.npcList.Count; i++)
                {
                    if (GameManager.npcList[i].GetComponent<Zombie>())
                    {
                        float distance = Vector3.Distance(GameManager.npcList[i].transform.position, transform.position);

                        if (distance < 5.0f) // Compare distance
                        {
                            reaction = true;
                            transform.rotation = GameManager.npcList[i].transform.rotation;
                            transform.position += transform.forward * (info.movSpeed * Time.fixedDeltaTime);
                        }
                        else
                            reaction = false;
                    }
                }
            }

            public static implicit operator Zombie(Citizen citizen)
            { // Cast citizen to zombie
                Zombie zombie = citizen.gameObject.AddComponent<Zombie>(); // Add Zombie component to the citizen
                zombie.info.age = citizen.info.age; // Keep the age of the citizen
                Destroy(citizen); // Destroy Citizen ocmponent
                return zombie;
            }
        }
    }
}