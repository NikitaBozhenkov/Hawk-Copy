using System;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public void Setup(ref Action shootAction)
    {
        shootAction += Shoot;
    }

    protected virtual void Shoot()
    {
        print("shot!");
    }
}