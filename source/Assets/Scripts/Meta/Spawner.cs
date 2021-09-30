using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Spawner
{
    private Transform chunksRoot;
    private Vector3 currentChunkSpawnPosition;
    private Transform playerRoot;

    public Spawner(Transform chunksRoot, Transform playerRoot, ref Action<GameStatus> gameStatusChanged)
    {
        this.chunksRoot = chunksRoot;
        currentChunkSpawnPosition = chunksRoot.position;
        this.playerRoot = playerRoot;
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

        for (int i = 0; i < chunksCount; ++i)
        {
            Chunk chunkToSpawn = chunkPrefabs[i % chunkPrefabs.Length];
            Chunk spawnedChunk = Object.Instantiate(chunkToSpawn,
                currentChunkSpawnPosition,
                chunkToSpawn.transform.rotation);
            spawnedChunk.transform.SetParent(chunksRoot);
            currentChunkSpawnPosition.z += spawnedChunk.GetComponent<MeshRenderer>().bounds.size.z;
            spawnedChunks[i] = spawnedChunk;
        }

        return spawnedChunks;
    }

    public Character SpawnPlayer(Character playerPrefab)
    {
        Character player = Object.Instantiate(playerPrefab);
        player.transform.SetParent(playerRoot);
        return player;
    }
}