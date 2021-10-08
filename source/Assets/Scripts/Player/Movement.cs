using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private Joystick joystick;

        private void FixedUpdate()
        {
            Move(joystick.MoveDelta * speedModifier);
        }

        private void Move(Vector2 additionalPosition)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += additionalPosition.x;
            newPosition.z += additionalPosition.y;
            transform.position = newPosition;
        }
    }
}