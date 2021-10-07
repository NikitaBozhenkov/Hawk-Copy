using Damage;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "GunConfig", menuName = "GunConfig", order = 5)]
    public class GunConfig : ScriptableObject
    {
        [SerializeField] private int shotsPerSecond;
        [SerializeField] private Bullet[] bulletPrefabs;

        public int ShotsPerSecond => shotsPerSecond;
        public Bullet[] BulletPrefabs => bulletPrefabs;
    }
}