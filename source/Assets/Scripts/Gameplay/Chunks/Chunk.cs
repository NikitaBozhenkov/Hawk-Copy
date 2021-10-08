using System;
using Characters;
using UnityEngine;

namespace Chunks
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Character[] enemies;

        public void Setup(Transform bulletsRoot)
        {
            foreach (Character enemy in enemies)
            {
                enemy.Setup(bulletsRoot);
            }
        }
    }
}