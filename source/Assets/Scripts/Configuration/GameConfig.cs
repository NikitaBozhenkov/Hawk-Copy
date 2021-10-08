using System;
using Characters;
using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameplayCameraConfig gameplayCameraConfig;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private ScreensConfig screensConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private ChunksConfig chunksConfig;
        [SerializeField] private Vector3 gameFieldRootPosition;
        [SerializeField] private float gameplayMovementSpeed;

        public GameplayCameraConfig GameplayCameraConfig => gameplayCameraConfig;
        public GameplayUI GameplayUI => gameplayUI;
        public ScreensConfig ScreensConfig => screensConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public ChunksConfig ChunksConfig => chunksConfig;
        public Vector3 GameFieldRootPosition => gameFieldRootPosition;
        public float GameplayMovementSpeed => gameplayMovementSpeed;
    }
}