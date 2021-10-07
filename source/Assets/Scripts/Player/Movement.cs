using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Touch touch;

        [SerializeField] private float speedModifier;

        private void FixedUpdate()
        {   
            ProcessTouches(InputHelper.GetTouches());
        }

        private void ProcessTouches(List<Touch> touches)
        {
            if (touches.Count == 0) return;
            touch = touches[0];

            Move(touch.deltaPosition * speedModifier);
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