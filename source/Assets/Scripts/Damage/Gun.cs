using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Damage
{
    public class Gun
    {
        private readonly Transform bulletsRoot;
        private readonly Bullet[] bulletPrefabs;

        public Gun(Transform bulletsRoot, Bullet[] bulletPrefabs, ref Action shootAction)
        {
            this.bulletsRoot = bulletsRoot;
            this.bulletPrefabs = bulletPrefabs;

            shootAction += Shoot;
        }

        protected virtual Bullet ChooseBullet()
        {
            return bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];
        }

        protected virtual void Shoot()
        {
            Bullet bulletToShootPrefab = ChooseBullet();
            Bullet shotBullet = Object.Instantiate(bulletToShootPrefab, bulletsRoot.position, bulletsRoot.rotation);
        }
    }
}