using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "GameplayCameraConfig", menuName = "GameplayCameraConfig", order = 1)]
    public class GameplayCameraConfig : ScriptableObject
    {
        [SerializeField] private float positionY;
        [SerializeField] private float angularRotationX;
        [SerializeField] private float orthographicSize;
        [SerializeField] private bool isOrthographic;

        public float PositionY => positionY;
        public float AngularRotationX => angularRotationX;
        public float OrthographicSize => orthographicSize;
        public bool IsOrthographic => isOrthographic;
    }
}