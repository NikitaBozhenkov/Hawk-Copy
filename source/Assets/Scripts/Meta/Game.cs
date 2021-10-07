using System;
using System.Collections;
using Characters;
using Configuration;
using Player;
using UnityEngine;
using Object = UnityEngine.Object;

public class Game : MonoBehaviour
{
    private GameConfig gameConfig;
    private ChunksManager chunksManager;
    private Character player;
    private BoxCollider gameFieldCollider;
    private Camera gameplayCamera;

    private GameStatus currentGameStatus;
    private float gameLength;

    private Action<GameStatus> onGameStatusChanged;
    private Transform gameFieldRoot;

    public void Setup(GameConfig gameConfig)
    {
        this.gameConfig = gameConfig;
    }

    public void StartGame()
    {
        if (gameConfig == null)
        {
            throw new NullReferenceException(nameof(gameConfig));
        }

        gameFieldRoot = new GameObject("gameFieldRoot").transform;
        gameFieldRoot.transform.position = gameConfig.GameFieldRootPosition;
        gameFieldRoot.SetParent(transform);
        gameFieldRoot.gameObject.AddComponent<MoveForward>().Setup(gameConfig.GameplayMovementSpeed);

        gameplayCamera = new GameObject("gameplayCamera").AddComponent<Camera>();
        SetupCamera(gameplayCamera, gameFieldRoot);

        Rect gameField = Constants.GetCameraProjectionOnPlaneXZ(gameplayCamera,
        gameplayCamera.transform.position.y - gameFieldRoot.position.y);
        
        CreateGameFieldCollider(gameFieldRoot, gameField);

        SpawnPlayer(gameField, gameFieldRoot, gameFieldCollider);

        chunksManager = new ChunksManager(gameConfig.ChunksConfig, gameField, gameFieldRoot.position.y, transform, ref onGameStatusChanged);
        chunksManager.MakeSpawns();
        gameLength = chunksManager.GetGameLength();

        StartCoroutine(CheckIsGameFinished());
    }

    private IEnumerator CheckIsGameFinished()
    {
        Transform gameplayCameraTransform = gameplayCamera.transform;
        float startPosition = gameplayCameraTransform.position.z;
        yield return new WaitUntil(() => Math.Abs(gameLength - (gameplayCameraTransform.position.z - startPosition)) < 1e-2);
        FinishGame();
    }

    private BoxCollider CreateGameFieldCollider(Transform gameFieldRoot, Rect gameField)
    {
        gameFieldCollider = new GameObject("gameFieldCollider").AddComponent<BoxCollider>();
        gameFieldCollider.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        gameFieldCollider.isTrigger = true;
        gameFieldCollider.gameObject.tag = "GameField";
        gameFieldCollider.transform.SetParent(gameFieldRoot);
        gameFieldCollider.size = new Vector3(gameField.width, 2, gameField.height);
        gameFieldCollider.transform.position =
            new Vector3(gameField.center.x, gameFieldRoot.position.y, gameField.center.y);
        return gameFieldCollider;
    }

    public void FinishGame() {
        print("game finished!!!");
    }

    private void SpawnPlayer(Rect gameField, Transform gameFieldRoot, BoxCollider gameFieldCollider)
    {
        player = Object.Instantiate(gameConfig.PlayerConfig.Prefab);
        Vector3 temp = player.transform.position;
        temp.x = gameField.center.x;
        temp.z = gameField.yMin + gameConfig.PlayerConfig.SpawnOffsetFromBottomCentre + player.MeshRenderer.bounds.size.z;
        player.transform.position = temp;
        player.transform.parent = gameFieldRoot;
        player.gameObject.AddComponent<Boundaries>().Setup(player.MeshRenderer, gameFieldCollider);
        player.Setup();
    }

    private void SetupCamera(Camera camera, Transform parent)
    {
        camera.transform.position = new Vector3(0, gameConfig.GameplayCameraConfig.PositionY, 0);
        camera.transform.rotation = Quaternion.Euler(gameConfig.GameplayCameraConfig.AngularRotationX, 0, 0);
        camera.transform.parent = parent;
        camera.orthographic = gameConfig.GameplayCameraConfig.IsOrthographic;
        camera.orthographicSize = gameConfig.GameplayCameraConfig.OrthographicSize;
    }
}