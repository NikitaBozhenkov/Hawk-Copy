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
        if (!ProcessTouches())
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);
        }
    }

    private bool ProcessTouches()
    {
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count == 0) return false;

        _touch = touches[0];
        if (_touch.phase != TouchPhase.Moved) return false;

        Vector3 newPosition = transform.position;
        Vector2 additionalPosition = _touch.deltaPosition * _speedModifier;
        newPosition.x += additionalPosition.x;
        newPosition.z += additionalPosition.y;
        transform.position = newPosition;
        return true;
    }

}
