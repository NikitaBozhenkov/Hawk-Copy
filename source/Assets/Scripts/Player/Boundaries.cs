using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Boundaries : MonoBehaviour
    {
        private Camera _mainCamera;
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

        public void Setup(Camera mainCamera)
        {
            _mainCamera = mainCamera;
            CacheValues();
            CalculateScreenBounds();
        }

        private void LateUpdate()
        {
            Bound();
        }

        private void CalculateScreenBounds()
        {
            _screenSizeX = Constants.GameFieldSizeX;
            _screenBoundX = _screenSizeX / 2;

            float height = _mainCamera.transform.position.y - transform.position.y;
            _lowerBound = Constants.GetGameFieldLowerBoundZ(height);
            _upperBound = Constants.GetGameFieldUpperBoundZ(height);
        }

        private void CacheValues()
        {
            _objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x;
            _objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.z;
            _mainCameraTransform = _mainCamera.transform;
            _objectTransform = transform;
        }

        private void Bound()
        {
            float tempLower = _lowerBound;
            float tempUpper = _upperBound;

            float cameraOffset = _mainCameraTransform.position.z;
            _lowerBound += cameraOffset;
            _upperBound += cameraOffset;

            Vector3 viewPos = _objectTransform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, _screenBoundX - _screenSizeX + _objectWidth, _screenBoundX - _objectWidth);
            viewPos.z = Mathf.Clamp(viewPos.z, _lowerBound + _objectHeight, _upperBound - _objectHeight);
            _objectTransform.position = viewPos;

            _lowerBound = tempLower;
            _upperBound = tempUpper;
        }
    }
}