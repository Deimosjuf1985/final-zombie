using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int _damage;
    int _bullets;

    public Gun()
    {
        _damage = 10;
        _bullets = 10;
    }

    public Gun(int damage, int bullets)
    {
        _damage = damage;
        _bullets = bullets;
    }


}
