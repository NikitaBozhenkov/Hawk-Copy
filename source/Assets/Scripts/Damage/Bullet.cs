using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public class Bullet : MonoBehaviour, IDamageDealing
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;

        public IEnumerator OnShot()
        {
            while (true)
            {
                transform.Translate(0,0, _speed * Time.deltaTime, Space.Self);
                yield return null;
            }
        }

        protected virtual float CalculateDamage()
        {
            return _damage;
        }
    
        public void DealDamage(IDamageTaking target)
        {
            target.TakeDamage(CalculateDamage());
        }
    }
}