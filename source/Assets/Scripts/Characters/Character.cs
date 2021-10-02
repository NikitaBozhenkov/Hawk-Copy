using System;
using Damage;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Gun[] guns;
        [SerializeField] private float shotsPerSecond;
        private int framesPerShot;
        private int counter;

        private bool isAbleToShoot = false;

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

        public void EnableShooting()
        {
            isAbleToShoot = true;
        }

        public void DisableShooting()
        {
            isAbleToShoot = false;
            counter = 0;
        }

        private void FixedUpdate()
        {
            if (!isAbleToShoot) return;
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
}