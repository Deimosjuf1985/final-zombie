using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;

public enum NPCStates { Idle, Moving, Rotating, Reaction }
public enum Rotations { Rigth, Left }

public struct NPCsInfo
{ // Info NPC
    public NPCStates npcStates; // variable para estados
    public float movSpeed;      //  velocidad movimiento
    public float rotSpeed;      //  velocidad rotacion
    public int age;             //  años de NPC
    public int healt;           //  nivel de vida
}

[RequireComponent(typeof(Rigidbody))]
public class NPCs : MonoBehaviour
{
    public NPCsInfo info;
    Rotations rotations;

    float stateTimer = 2.0f;
    protected bool reaction = false;
    float rotY; // Rotation in Y
    protected Rigidbody rb;

    protected Hero hero;
    
    void Awake()
    { // Random for enums
        rb = GetComponent<Rigidbody>();
        info.npcStates = (NPCStates)Random.Range(0, 3);     //  Estado aleatorio
        rotations = (Rotations)Random.Range(0, 2);          //  rotacion aleatorio
        info.age = Random.Range(15, 100);                   //  edad aleatorio
        info.rotSpeed = Random.Range(5, 8);                 //  velocidad de rotacion
        SpeedToAge();
        info.healt = 100;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
    }

    public void States()
    {
        switch (info.npcStates)
        {
            case NPCStates.Idle:
                goto Ideling;
            case NPCStates.Moving:
                goto Move;
            case NPCStates.Rotating:
                goto Rotate;
            default:
                break;
        }

        Ideling:
        if (reaction == false)
        {
            stateTimer -= Time.deltaTime;
            //Debug.Log("idle");
            if (stateTimer < 0.0f)
            {
                info.npcStates = (NPCStates)Random.Range(0, 3);
                rotations = (Rotations)Random.Range(0, 2);
                stateTimer = 3.0f;
            }
        }
        return;

        Move:
        if (reaction == false)
        {
            stateTimer -= Time.deltaTime;
            rb.transform.Translate(Vector3.forward * info.movSpeed * Time.fixedDeltaTime); // Front movement
            //Debug.Log("move");
            if (stateTimer < 0.0f)
            {
                info.npcStates = (NPCStates)Random.Range(0, 3);
                rotations = (Rotations)Random.Range(0, 2);
                stateTimer = 3.0f;
            }
        }
        return;

        Rotate:
        if (reaction == false)
        {
            stateTimer -= Time.deltaTime;
            
            Rotation();
            if (stateTimer < 0.0f)
            {
                info.npcStates = (NPCStates)Random.Range(0, 3);
                rotations = (Rotations)Random.Range(0, 2);
                stateTimer = 3.0f;
            }
        }
        return;
    }

    public void Rotation()
    {
        if (rotations == Rotations.Rigth) //  Rigth rotation
        {
            rotY += info.rotSpeed * Time.deltaTime;
            rb.MoveRotation(Quaternion.Euler(0, rotY, 0));
        }
        else if (rotations == Rotations.Left) //  Left rotation
        {
            rotY -= info.rotSpeed * Time.deltaTime;
            rb.MoveRotation(Quaternion.Euler(0, rotY, 0));
        }
    }

    public virtual void Reaction()
    {
        for (int i = 0; i < GameManager.npcList.Count; i++)
        {
            if (GameManager.npcList[i].GetComponent<Citizen>() || GameManager.npcList[i].GetComponent<Hero>())
            {
                float distance = Vector3.Distance(GameManager.npcList[i].transform.position, transform.position);

                if (GameManager.npcList[i].GetComponent<Hero>())
                {
                    hero = GameManager.npcList[i].GetComponent<Hero>();
                }

                if (distance < 5.0f) 
                {
                    
                    reaction = true;
                    transform.LookAt(GameManager.npcList[i].transform);
                    transform.position += transform.forward * (info.movSpeed * Time.fixedDeltaTime);
                }
                else
                    reaction = false;
            }
        }
    }

    public NPCsInfo InfoNPC()
    { // informacion NPC
        return info;
    }

    void SpeedToAge()
    {//movimiento deacuerdo al año
        if (info.age == 15 && info.age < 22)
            info.movSpeed = 2;

        else if (info.age >= 22 && info.age <= 35)
            info.movSpeed = 3;

        else if (info.age > 35 && info.age <= 45)
            info.movSpeed = 4;

        else if (info.age > 45 && info.age <= 65)
            info.movSpeed = 5;

        else info.movSpeed = 6;
    }
}