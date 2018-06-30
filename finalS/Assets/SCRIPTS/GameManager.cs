using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using NPC.Enemy;
using NPC.Ally;

public class GameManager : MonoBehaviour
{
    GameObject go;                      // Object for the primitives
    int xCor;                           // Random for the X coordinate
    int zCor;                           // Random for the Z coordinate
    int characters;                     // Number of characters that will be created
    int character;                      // Creation case for the swtich
    readonly int minPrimitive;          // Minimum value for creation objects
    const int MAXPRIMITIVE = 25;        // Maximum value for creation objects

    public Text zombieDialogue; // npc dialogue (zombie)
    public Animator zomDiagAnimator;

    public Text citizenDialogue; // npc dialogue (citizen)
    public Animator citDiagAnimator;

    public Text gameOver;

    public static List<GameObject> npcList = new List<GameObject>(); // Object list on npc
    public int zomCount;                   // Zombie count
    public int citCount;                   // Citizen count
    public Text zomText;            // Zombie text in screen
    public Text citText;            // Citizen text in scren

    public GameObject player;

    public GameManager()
    { // Constructor
        minPrimitive = Initializer.minimumRange; // Initialize
    }

	void Start ()
    {
        CreateHero();

        characters = Random.Range(minPrimitive, MAXPRIMITIVE); // Random value of primitive creations
        if (characters < 5)
            characters = 5; //  if readonly don't initialize

        for (int i = 0; i < characters; i++)
        {
            xCor = Random.Range(-70, 70); // Random number for X coordinate
            zCor = Random.Range(-70, 70); // Random number for Z coordinate

            character = Random.Range(1, 3); // Random case
            switch (character)
            {
                case 1: // Case Zombie
                    CreateZombie();
                    break;
                case 2: // Case Citizen
                    CreateCitizen();
                    break;
            }

            if (go.GetComponent<Zombie>())
            {
                npcList.Add(go);
                zomCount += 1; // Zombie count
            }
            if (go.GetComponent<Citizen>())
            {
                npcList.Add(go);
                citCount += 1; // Citizen count
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            if (npcList[i].name == "Zombie")
            {
                zomText.text = "Zombies : " + zomCount.ToString(); // Text in screen
            }
            else if (npcList[i].name == "Citizen")
            {
                citText.text = "Citizens : " + citCount.ToString(); // Text in scren
            }
        }
    }

    void CreateHero()
    { // Hero Creation
        Instantiate(player, new Vector3(xCor, 0.5f, zCor), Quaternion.identity);
        player.AddComponent<Hero>();                                //  Add the Hero component
        player.name = "Hero";                                       //  Name in Hierarchy
        player.tag = "Player";                                      //  Object tag
        npcList.Add(player);
    }

    void CreateZombie()
    { // Zombie Creation
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);    //  Create primitive
        go.transform.position = new Vector3(xCor, 0.5f, zCor);  //  It positions it randomly
        go.AddComponent<Zombie>();                              //  Add the Zombie component
        go.name = "Zombie";                                     //  Name in Hierarchy
        go.tag = "Zombie";                                      //  Object tag
    }

    void CreateCitizen()
    { // Citizen Creation
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);    //  Create primitive
        go.transform.position = new Vector3(xCor, 0.5f, zCor);  //  It positions it randomly
        go.AddComponent<Citizen>();                             //  Add the Citizen component
        go.name = "Citizen";                                    //  Name in Hierarchy
        go.tag = "Citizen";                                     //  Object tag
    }
}