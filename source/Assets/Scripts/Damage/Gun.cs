using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Damage
{
    public class Gun : MonoBehaviour
    {
        [Header("Bullets Settings")]
        [SerializeField] private Transform _bulletStartPosition;
        [SerializeField] private Bullet[] _bulletPrefabs;

        public void Setup(ref Action shootAction)
        {
            shootAction += Shoot;
        }

        protected virtual Bullet ChooseBullet()
        {
            return _bulletPrefabs[Random.Range(0, _bulletPrefabs.Length)];
        }

        protected virtual void Shoot()
        {
            Bullet bulletToShootPrefab = ChooseBullet();
            Bullet shotBullet = Instantiate(bulletToShootPrefab, _bulletStartPosition.position, _bulletStartPosition.rotation);
            StartCoroutine(shotBullet.OnShot());
        }
    }
}