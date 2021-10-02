using UnityEngine;

namespace Characters
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            transform.Translate(0, 0, _speed * Time.deltaTime, Space.World);
        }
    }
}
