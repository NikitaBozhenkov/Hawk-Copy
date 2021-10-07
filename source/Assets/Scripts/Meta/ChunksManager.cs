using System;
using System.Collections.Generic;
using Chunks;
using Configuration;
using UnityEngine;
using Object = UnityEngine.Object;

public class ChunksManager
{
    private readonly ChunksConfig chunksConfig;
    private readonly Rect gameField;
    private readonly Transform chunksRoot;
    private readonly List<Chunk> chunks;

    private Vector3 currentChunkSpawnPosition;
    
    public ChunksManager(ChunksConfig chunksConfig, 
        Rect gameField, 
        float spawnY, 
        Transform root,
        ref Action<GameStatus> gameStatusChanged)
    {
        this.chunksConfig = chunksConfig;
        this.gameField = gameField;
        currentChunkSpawnPosition = new Vector3(
            gameField.center.x,
            spawnY,
            gameField.center.y
        );
        gameStatusChanged += OnGameStatusChanged;
        chunks = new List<Chunk>();

        chunksRoot = new GameObject().transform;
        chunksRoot.name = "chunksRoot";
        chunksRoot.position = new Vector3(gameField.center.x, spawnY,gameField.center.y);
        chunksRoot.SetParent(root);
    }

    public float GetGameLength()
    {
        return (chunksConfig.StartChunksCount + chunksConfig.EnemyChunksCount) * gameField.height;
    } 

    private Chunk[] SpawnChunks(Chunk[] chunkPrefabs, int chunksCount)
    {
        Chunk[] spawnedChunks = new Chunk[chunksCount];

        for (int i = 0; i < chunksCount; ++i)
        {
            Chunk chunkToSpawn = chunkPrefabs[i % chunkPrefabs.Length];
            Chunk spawnedChunk = Object.Instantiate(chunkToSpawn,
                currentChunkSpawnPosition,
                chunkToSpawn.transform.rotation);
            spawnedChunk.transform.SetParent(chunksRoot);
            spawnedChunk.transform.localScale =  new Vector3(gameField.width / 10, 1, gameField.height / 10);
            currentChunkSpawnPosition.z += spawnedChunk.GetComponent<MeshRenderer>().bounds.size.z;
            spawnedChunks[i] = spawnedChunk;
        }

        return spawnedChunks;
    }

    public void MakeSpawns()
    {
        chunks.AddRange(SpawnChunks(chunksConfig.StartChunkPrefabs, chunksConfig.StartChunksCount));
        chunks.AddRange(SpawnChunks(chunksConfig.EnemyChunkPrefabs, chunksConfig.EnemyChunksCount));
        chunks.AddRange(SpawnChunks(chunksConfig.FinishChunkPrefabs, chunksConfig.FinishChunksCount));

        foreach (Chunk chunk in chunks)
        {
            chunk.Setup();
        }
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
}