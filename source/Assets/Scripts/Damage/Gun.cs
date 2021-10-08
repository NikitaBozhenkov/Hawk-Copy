using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Damage
{
    public class Gun
    {
        private readonly Transform bulletsRoot;
        private readonly Transform bulletsStartRoot;
        private readonly Bullet[] bulletPrefabs;
        private readonly ObjectType objectType;

        public Gun(Transform bulletsRoot, Transform bulletsStartRoot, Bullet[] bulletPrefabs, ObjectType objectType,
            ref Action shootAction)
        {
            this.bulletsRoot = bulletsRoot;
            this.bulletsStartRoot = bulletsStartRoot;
            this.bulletPrefabs = bulletPrefabs;
            this.objectType = objectType;

            shootAction += Shoot;
        }

        protected virtual Bullet ChooseBullet()
        {
            return bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];
        }

        protected virtual void Shoot()
        {
            Bullet bulletToShootPrefab = ChooseBullet();
            Bullet shotBullet =
                Object.Instantiate(bulletToShootPrefab, bulletsStartRoot.position, bulletsStartRoot.rotation);
            shotBullet.transform.SetParent(bulletsRoot);
            shotBullet.Setup(objectType);
        }
    }
}