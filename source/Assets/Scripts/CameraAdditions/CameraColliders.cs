using UnityEngine;

namespace CameraAdditions
{
    public class CameraColliders : MonoBehaviour
    {
        [SerializeField] private float gameFieldY;
        [SerializeField] private float collidersHeight;
        [SerializeField] private float colDepth;

        private Transform topCollider;
        private Transform bottomCollider;
        private Transform leftCollider;
        private Transform rightCollider;

        private void Start() {
            bottomCollider = new GameObject().transform;
            rightCollider = new GameObject().transform;
            leftCollider = new GameObject().transform;
            topCollider = new GameObject().transform;

            bottomCollider.name = "BottomCollider";
            bottomCollider.tag = "Disable";
            rightCollider.name = "RightCollider";
            leftCollider.name = "LeftCollider";
            topCollider.name = "TopCollider";
            topCollider.tag = "Enable";

            bottomCollider.gameObject.AddComponent<BoxCollider>();
            topCollider.gameObject.AddComponent<BoxCollider>();
            rightCollider.gameObject.AddComponent<BoxCollider>();
            leftCollider.gameObject.AddComponent<BoxCollider>();

            bottomCollider.parent = transform;
            rightCollider.parent = transform;
            leftCollider.parent = transform;
            topCollider.parent = transform; 

            //float GameFieldSizeX = Constants.GameFieldSizeX;
            //float GameFieldSizeZ = Constants.GameFieldSizeZ;
            //float GameFieldLowerBoundZ = Constants.GetGameFieldLowerBoundZ(gameFieldY);
            //float GameFieldUpperBoundZ = Constants.GetGameFieldUpperBoundZ(gameFieldY);
            //print("X: " + GameFieldSizeX);
            //print("Z: " + GameFieldSizeZ);
            //print("Z lower: " + GameFieldSizeZ);
            //print("Z upper: " + GameFieldSizeZ);
            //
            //rightCollider.localScale = new Vector3(colDepth, collidersHeight, GameFieldSizeZ);
            //rightCollider.position = new Vector3(GameFieldSizeX * 0.5f + (rightCollider.localScale.x * 0.5f), 
            //    gameFieldY,
            //    GameFieldLowerBoundZ + Constants.GameFieldSizeZ);
            //leftCollider.localScale = new Vector3(colDepth, collidersHeight, GameFieldSizeZ);
            //leftCollider.position = new Vector3(-GameFieldSizeX * 0.5f - (leftCollider.localScale.x * 0.5f),
            //    gameFieldY,
            //    GameFieldLowerBoundZ + Constants.GameFieldSizeZ);
            //topCollider.localScale = new Vector3(GameFieldSizeX, collidersHeight, colDepth);
            //topCollider.position = new Vector3(0, 
            //    gameFieldY,
            //    GameFieldUpperBoundZ + Constants.GameFieldSizeZ / 2 + topCollider.localScale.z * 0.5f);
            //bottomCollider.localScale = new Vector3(GameFieldSizeX, collidersHeight, colDepth);
            //bottomCollider.position = new Vector3(0, 
            //    gameFieldY, 
            //    GameFieldLowerBoundZ + Constants.GameFieldSizeZ / 2 - topCollider.localScale.z * 0.5f );
        }

    }
}
