using UnityEngine;

namespace Characters
{
    public class MoveForward : MonoBehaviour
    {
        private float speed;

        public void Setup(float speed)
        {
            this.speed = speed;
        }

        private void FixedUpdate()
        {
            transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
        }
    }
}
