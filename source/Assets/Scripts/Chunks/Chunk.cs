using System;
using Characters;
using UnityEngine;

namespace Chunks
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Character[] enemies;

        public event Action onTrigger;

        public void Setup()
        {
            foreach (Character enemy in enemies)
            {
                enemy.Setup();
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.CompareTag("Disable")) {
                onTrigger?.Invoke();
            }
        }
    }
}