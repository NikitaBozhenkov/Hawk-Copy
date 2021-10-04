using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Boundaries : MonoBehaviour
    {
        private Camera mainCamera;
        private Transform mainCameraTransform;

        private float screenBoundX;
        private float screenSizeX;

        private float objectWidth;
        private float objectHeight;

        private float lowerBound;
        private float upperBound;

        private Transform objectTransform;

        public void Setup(Camera mainCamera)
        {
            this.mainCamera = mainCamera;
            CacheValues();
            CalculateScreenBounds();
        }

        private void LateUpdate()
        {
            Bound();
        }

        private void CalculateScreenBounds()
        {
            screenSizeX = Constants.GameFieldSizeX;
            screenBoundX = screenSizeX / 2;

            float height = mainCamera.transform.position.y - transform.position.y;
            lowerBound = Constants.GetGameFieldLowerBoundZ(height);
            upperBound = Constants.GetGameFieldUpperBoundZ(height);
        }

        private void CacheValues()
        {
            objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x;
            objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.z;
            mainCameraTransform = mainCamera.transform;
            objectTransform = transform;
        }

        private void Bound()
        {
            float tempLower = lowerBound;
            float tempUpper = upperBound;

            float cameraOffset = mainCameraTransform.position.z;
            lowerBound += cameraOffset;
            upperBound += cameraOffset;

            Vector3 viewPos = objectTransform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBoundX - screenSizeX + objectWidth, screenBoundX - objectWidth);
            viewPos.z = Mathf.Clamp(viewPos.z, lowerBound + objectHeight, upperBound - objectHeight);
            objectTransform.position = viewPos;

            lowerBound = tempLower;
            upperBound = tempUpper;
        }
    }
}