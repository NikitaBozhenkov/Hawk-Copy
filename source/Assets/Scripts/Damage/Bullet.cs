using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Damage
{
    public class Bullet : MonoBehaviour, IDamageDealing
    {
        [SerializeField] private float damage;
        [SerializeField] private float speed;
        private ObjectType objectType;

        public void Setup(ObjectType objectType)
        {
            this.objectType = objectType;
        }

        private void Start()
        {
            StartCoroutine(OnShot());
        }

        protected IEnumerator OnShot()
        {
            while (true)
            {
                transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
                yield return null;
            }
        }

        protected virtual float CalculateDamage()
        {
            return damage;
        }

        public void DealDamage(IDamageTaking target)
        {
            target.TakeDamage(CalculateDamage());
        }

        private void OnTriggerEnter(Collider other)
        {
            Character target = other.GetComponent<Character>();
            if (target == null || target.ObjectType == objectType) return;
            DealDamage(target);
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("GameField")) return;
            Destroy(gameObject);
        }
    }
}