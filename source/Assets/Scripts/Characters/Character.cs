using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Gun[] guns;
    [SerializeField] private float shotsPerSecond;
    [SerializeField] private int framesPerShot;
    private int counter;

    private Action shoot;

    public void Setup()
    {
        foreach (var gun in guns)
        {
            gun.Setup(ref shoot);
        }

        framesPerShot = (int)(1 / (Time.fixedDeltaTime * shotsPerSecond));
        counter = 0;
    }

    private void FixedUpdate()
    {
        CheckForShot();
    }

    private void CheckForShot()
    {
        ++counter;
        if (counter != framesPerShot) return;
        shoot?.Invoke();
        counter = 0;
    }
}