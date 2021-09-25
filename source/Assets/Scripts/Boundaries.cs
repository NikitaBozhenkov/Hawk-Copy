using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private Transform _mainCameraTransform;
    private float _cameraRotation;
    private float _boundariesDistanceFromCamera;
    private float _cameraSize;

    private float _screenBoundX;
    private float _screenSizeX;

    private float _objectWidth;
    private float _objectHeight;
    
    private float _lowerBound;
    private float _upperBound;

    private Transform _objectTransform;

    private void Start()
    {
        CacheValues();
        CalculateScreenBounds();
    }

    private void LateUpdate()
    {
        Bound();
    }

    private void CalculateScreenBounds() {
        _screenSizeX = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)),
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        _screenBoundX =
            _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z)).x;
        _cameraRotation = Mathf.Deg2Rad * _mainCamera.transform.rotation.eulerAngles.x;
        _boundariesDistanceFromCamera = _mainCamera.transform.position.y - transform.position.y;
        _cameraSize = _mainCamera.orthographicSize * 2;
        _lowerBound =
            (_boundariesDistanceFromCamera - Mathf.Cos(_cameraRotation) * _cameraSize / 2f) / Mathf.Tan(_cameraRotation) -
            (Mathf.Sin(_cameraRotation) * _cameraSize / 2f);
        _upperBound = _lowerBound + _cameraSize / Mathf.Sin(_cameraRotation);
    }

    private void CacheValues() {
        _objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x;
        _objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.z;
        _mainCameraTransform = _mainCamera.transform;
        _objectTransform = transform;
    }

    private void Bound() {
        float tempLower = _lowerBound;
        float tempUpper = _upperBound;

        float cameraOffset = _mainCameraTransform.position.z;
        _lowerBound += cameraOffset;
        _upperBound += cameraOffset;

        Vector3 viewPos = _objectTransform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBoundX - _screenSizeX * 2 + _objectWidth, _screenBoundX - _objectWidth);
        viewPos.z = Mathf.Clamp(viewPos.z, _lowerBound + _objectHeight, _upperBound - _objectHeight);
        _objectTransform.position = viewPos;

        _lowerBound = tempLower;
        _upperBound = tempUpper;
    }
}
