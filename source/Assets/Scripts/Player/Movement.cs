using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Touch _touch;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedModifier;

    private void FixedUpdate()
    {   
        ProcessTouches(InputHelper.GetTouches());
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void ProcessTouches(List<Touch> touches)
    {
        if (touches.Count == 0) return;
        _touch = touches[0];

        Move(_touch.deltaPosition * _speedModifier);
    }

    private void Move(Vector2 additionalPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.x += additionalPosition.x;
        newPosition.z += additionalPosition.y;
        transform.position = newPosition;
    }

}
