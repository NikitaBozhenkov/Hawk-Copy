using Configuration;
using UnityEngine;

namespace Gameplay
{
    public class GameplayCameraController
    {
        private readonly GameplayCameraConfig config;

        public Camera Camera { get; private set; }

        public GameplayCameraController(GameplayCameraConfig config)
        {
            this.config = config;
        }

        public void CreateCamera()
        {
            Camera = new GameObject("gameplayCamera").AddComponent<Camera>();
        }

        public void SetupCamera(Transform parent)
        {
            Camera.transform.position = new Vector3(0, config.PositionY, 0);
            Camera.transform.rotation = Quaternion.Euler(config.AngularRotationX, 0, 0);
            Camera.transform.parent = parent;
            Camera.orthographic = config.IsOrthographic;
            Camera.orthographicSize = config.OrthographicSize;
        }
    }
}