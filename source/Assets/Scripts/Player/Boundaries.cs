using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Boundaries : MonoBehaviour
    {
        private MeshRenderer objectMeshRenderer;
        private Transform objectTransform;
        private float objectWidth;
        private float objectHeight;
        private BoxCollider gameField;
        private Transform gameFieldTransform;
        private Vector3 gameFieldSize;

        public void Setup(MeshRenderer objectMeshRenderer, BoxCollider gameField)
        {
            this.objectMeshRenderer = objectMeshRenderer;
            this.gameField = gameField;
            CacheValues();
        }

        private void LateUpdate()
        {
            Bound();
        }

        private void CacheValues()
        {
            objectTransform = transform;
            objectWidth = objectMeshRenderer.bounds.extents.x;
            objectHeight = objectMeshRenderer.bounds.extents.z;
            gameFieldTransform = gameField.transform;
            gameFieldSize = gameField.size;
        }

        private void Bound()
        {
            Vector3 newPosition = objectTransform.position;
            Vector3 gameFieldPosition = gameFieldTransform.position;

            newPosition.x = Mathf.Clamp(
                newPosition.x, 
                gameFieldPosition.x - gameFieldSize.x / 2 + objectWidth,
                gameFieldPosition.x + gameFieldSize.x / 2 - objectWidth);
            newPosition.z = Mathf.Clamp(
                newPosition.z, 
                gameFieldPosition.z - gameFieldSize.z / 2 + objectHeight,
                gameFieldPosition.z + gameFieldSize.z / 2 - objectHeight);
            objectTransform.position = newPosition;
        }
    }
}