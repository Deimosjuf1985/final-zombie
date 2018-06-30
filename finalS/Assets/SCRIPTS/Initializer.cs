using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public static int minimumRange; //  Minimum value for creation of primitives
    public static float heroSpeed;  //  Float value for the speed movement

    void Awake()
    {
        minimumRange = Random.Range(5, 15); //  Random value between 5 and 15
        heroSpeed = Random.Range(10f, 15f);  //  Random value
    }
}