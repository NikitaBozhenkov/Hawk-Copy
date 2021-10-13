using System;
using System.Collections;
using Characters;
using Chunks;
using Configuration;
using Player;
using Screens;
using Tools;
using UnityEngine;

namespace Gameplay
{
    public class Game : MonoBehaviour
    {
        private GameConfig gameConfig;
        private ChunksManager chunksManager;
        private Character player;
        private BoxCollider gameFieldCollider;
        private Camera gameplayCamera;

        private float gameLength;
        private Transform bulletsRoot;

        private Transform gameFieldRoot;
        private GameplayScreen gameplayScreen;

        public event Action<GameResultStatus> GameFinished;

        public void Setup(GameConfig gameConfig, Camera gameplayCamera, GameplayScreen gameplayScreen)
        {
            this.gameConfig = gameConfig;
            this.gameplayCamera = gameplayCamera;
            this.gameplayScreen = gameplayScreen;
        }

        public void StartGame()
        {
            if (gameConfig == null)
            {
                throw new NullReferenceException(nameof(gameConfig));
            }

            CreateEnvironment();
            StartCoroutine(ProgressCounter());
        }

        public void FinishGame(GameResultStatus result)
        {
            gameplayCamera.transform.SetParent(null);
            GameFinished?.Invoke(result);
            Destroy(gameObject);
        }

        private IEnumerator ProgressCounter()
        {
            Transform gameplayCameraTransform = gameplayCamera.transform;
            float startPosition = gameplayCameraTransform.position.z;
            while (true)
            {
                if (player == null)
                {
                    FinishGame(GameResultStatus.Fail);
                }

                float finishedPart = (gameplayCameraTransform.position.z - startPosition) / gameLength;
                gameplayScreen.SetLevelProgress(finishedPart);
                if (finishedPart < 1)
                {
                    yield return null;
                    continue;
                }

                FinishGame(GameResultStatus.Success);
                yield break;
            }
        }

        #region Environment

        private void CreateEnvironment()
        {
            CreateGameFieldRoot();
            CreateBulletsRoot();
            SetupCamera(gameFieldRoot);
            Rect gameField = ProjectionCalculator.GetCameraProjectionOnPlaneXZ(gameplayCamera,
                gameplayCamera.transform.position.y - gameFieldRoot.position.y);
            CreateGameFieldCollider(gameField);
            CreatePlayer(gameField);
            CreateChunks(gameField);
            gameLength = chunksManager.GetGameLength();
        }

        private void CreateChunks(Rect gameField)
        {
            chunksManager = new ChunksManager(gameConfig.ChunksConfig, gameField, gameFieldRoot.position.y, transform);
            chunksManager.MakeSpawns(bulletsRoot);
        }

        public void SetupCamera(Transform parent) {
            gameplayCamera.transform.position = new Vector3(0, gameConfig.GameplayCameraConfig.PositionY, 0);
            gameplayCamera.transform.rotation = Quaternion.Euler(gameConfig.GameplayCameraConfig.AngularRotationX, 0, 0);
            gameplayCamera.transform.parent = parent;
            gameplayCamera.orthographic = gameConfig.GameplayCameraConfig.IsOrthographic;
            gameplayCamera.orthographicSize = gameConfig.GameplayCameraConfig.OrthographicSize;
        }

        private void CreateGameFieldRoot()
        {
            gameFieldRoot = new GameObject("gameFieldRoot").transform;
            gameFieldRoot.transform.position = gameConfig.GameFieldRootPosition;
            gameFieldRoot.SetParent(transform);
            gameFieldRoot.gameObject.AddComponent<MoveForward>().Setup(gameConfig.GameplayMovementSpeed);
        }

        private void CreateBulletsRoot()
        {
            bulletsRoot = new GameObject("bulletsRoot").transform;
            bulletsRoot.transform.position = gameConfig.GameFieldRootPosition;
            bulletsRoot.SetParent(gameFieldRoot);
        }

        private void CreateGameFieldCollider(Rect gameField)
        {
            gameFieldCollider = new GameObject("gameFieldCollider").AddComponent<BoxCollider>();
            gameFieldCollider.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameFieldCollider.isTrigger = true;
            gameFieldCollider.gameObject.tag = "GameField";
            gameFieldCollider.transform.SetParent(gameFieldRoot);
            gameFieldCollider.size = new Vector3(gameField.width, 2, gameField.height);
            gameFieldCollider.transform.position =
                new Vector3(gameField.center.x, gameFieldRoot.position.y, gameField.center.y);
        }

        private void CreatePlayer(Rect gameField)
        {
            player = Instantiate(gameConfig.PlayerConfig.Prefab);
            Vector3 temp = player.transform.position;
            temp.x = gameField.center.x;
            temp.z = gameField.yMin + gameConfig.PlayerConfig.SpawnOffsetFromBottomCentre +
                     player.MeshRenderer.bounds.size.z;
            player.transform.position = temp;
            player.transform.parent = gameFieldRoot;
            player.gameObject.AddComponent<Boundaries>().Setup(player.MeshRenderer, gameFieldCollider);
            player.Setup(bulletsRoot);
        }

        #endregion
    }
}