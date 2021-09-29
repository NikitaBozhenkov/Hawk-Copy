using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Spawner
{
    private Vector3 currentChunkSpawnPosition;
    private Vector3 playerSpawnPosition;

    public Spawner(Transform chunksRoot, Transform playerRoot, ref Action<GameStatus> gameStatusChanged)
    {
        currentChunkSpawnPosition = chunksRoot.position;
        playerSpawnPosition = playerRoot.position;
        gameStatusChanged += OnGameStatusChanged;
    }

    private void OnGameStatusChanged(GameStatus newGameStatus)
    {
        switch (newGameStatus)
        {
            case GameStatus.InMenu:
                Debug.Log("status changed to: inMenu");
                break;
            case GameStatus.InGame:
                Debug.Log("status changed to: inGame");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameStatus), newGameStatus, null);
        }
    }

    public Chunk[] SpawnChunks(Chunk[] chunkPrefabs, int chunksCount)
    {
        Chunk[] spawnedChunks = new Chunk[chunksCount];

        for(int i = 0; i < chunksCount; ++i) {
            Chunk chunk = chunkPrefabs[i % chunkPrefabs.Length];
            Object.Instantiate(chunk, currentChunkSpawnPosition, chunk.transform.rotation);
            currentChunkSpawnPosition.z += chunk.GetComponent<MeshRenderer>().bounds.size.z;
            spawnedChunks[i] = chunk;
        }

        return spawnedChunks;
    }

    public Character SpawnPlayer(Character playerPrefab)
    {
        Character player = Object.Instantiate(playerPrefab);
        player.transform.position = playerSpawnPosition;
        return player;
    }
}