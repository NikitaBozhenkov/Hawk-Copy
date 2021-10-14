using System;
using System.Collections.Generic;
using Tools;
using Configuration;
using Damage;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour, IDamageTaking
    {
        [SerializeField] private GunConfig[] gunConfigs;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private bool isAbleToShootAtStart;
        [SerializeField] private float maxHealth;
        [SerializeField] private ObjectType objectType;
        [SerializeField] private HealthBar healthBar;

        private List<Gun> guns;
        private List<Pair<int, int>> shootCounters;
        private List<Action> shootActions;
        private bool isAbleToShoot;
        private float currentHealth;

        public MeshRenderer MeshRenderer => meshRenderer;
        public ObjectType ObjectType => objectType;

        public void Setup(Transform bulletsRoot)
        {
            guns = new List<Gun>();
            shootCounters = new List<Pair<int, int>>();
            shootActions = new List<Action>();
            isAbleToShoot = isAbleToShootAtStart;
            currentHealth = maxHealth;

            foreach (GunConfig gunConfig in gunConfigs)
            {
                Action action = delegate { };

                Gun gun = new Gun(bulletsRoot, transform, gunConfig.BulletPrefabs, objectType, ref action);

                guns.Add(gun);
                shootCounters.Add(new Pair<int, int>(
                    0,
                    (int) (1 / (Time.fixedDeltaTime * gunConfig.ShotsPerSecond)))
                );
                shootActions.Add(action);
            }
        }

        public virtual void EnableShooting()
        {
            isAbleToShoot = true;
        }

        public virtual void DisableShooting()
        {
            isAbleToShoot = false;
        }

        private void FixedUpdate()
        {
            if (!isAbleToShoot) return;
            CheckForShot();
        }

        protected virtual void CheckForShot()
        {
            for (int i = 0; i < guns.Count; ++i)
            {
                ++shootCounters[i].First;
                if (shootCounters[i].First != shootCounters[i].Second) continue;
                shootActions[i]?.Invoke();
                shootCounters[i].First = 0;
            }
        }

        public void TakeDamage(float value)
        {
            currentHealth -= value;
            healthBar.SetHpPart(currentHealth / maxHealth);
            if (currentHealth < 0) Destroy(gameObject);
        }
    }
}