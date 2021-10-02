using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class EnableOnTrigger : MonoBehaviour
    {
        [SerializeField] private Character character;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enable"))
            {
                character.EnableShooting();
            } else if (other.gameObject.CompareTag("Disable"))
            {
                character.DisableShooting();
            }
        }
    }
}