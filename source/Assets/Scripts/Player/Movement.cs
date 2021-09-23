using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Touch _touch;
    [SerializeField] private float _speedModifier;

    private void FixedUpdate()
    {
        ProcessTouches();
    }

    private void ProcessTouches()
    {
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count == 0) return;

        _touch = touches[0];
        if (_touch.phase != TouchPhase.Moved) return;

        Vector3 newPosition = transform.position;
        Vector2 additionalPosition = _touch.deltaPosition * _speedModifier;
        newPosition.x += additionalPosition.x;
        newPosition.z += additionalPosition.y;
        transform.position = newPosition;
    }

}
