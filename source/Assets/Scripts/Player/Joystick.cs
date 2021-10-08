using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Player
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private float maxDeltaPerFrame;

        public Vector2 MoveDelta { get; private set; }

        private void FixedUpdate()
        {
            ProcessTouches(InputHelper.GetTouches());
        }

        private void ProcessTouches(List<Touch> touches)
        {
            if (touches.Count == 0)
            {
                MoveDelta = Vector2.zero;
                return;
            }

            Touch touch = touches[0];

            Vector2 newDelta = new Vector2(
                Mathf.Clamp(touch.deltaPosition.x, maxDeltaPerFrame * -1, maxDeltaPerFrame),
                Mathf.Clamp(touch.deltaPosition.y, maxDeltaPerFrame * -1, maxDeltaPerFrame)
            );

            MoveDelta = newDelta;
        }
    }
}