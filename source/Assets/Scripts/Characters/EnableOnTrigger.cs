using UnityEngine;

namespace Characters
{
    public class EnableOnTrigger : MonoBehaviour
    {
        [SerializeField] private Character character;

        private bool wasEnabled = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("GameField")) return;
            character.EnableShooting();
            wasEnabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("GameField") || !wasEnabled) return;
            character.DisableShooting();
        }
    }
}