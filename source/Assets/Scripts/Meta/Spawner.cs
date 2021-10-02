using System;
using Characters;
using Chunks;
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
            ScaleChunk(spawnedChunk.transform);
            currentChunkSpawnPosition.z += spawnedChunk.GetComponent<MeshRenderer>().bounds.size.z;
            spawnedChunks[i] = spawnedChunk;
        }

        return spawnedChunks;
    }

    private void ScaleChunk(Transform chunkTransform)
    {
        Vector3 scaleTemp = chunkTransform.localScale;
        scaleTemp.x = Constants.GameFieldSizeX * .1f;
        scaleTemp.z = Constants.GameFieldSizeZ * .1f;
        chunkTransform.localScale = scaleTemp;
    }

    public Character SpawnPlayer(Character playerPrefab)
    {
        Character player = Object.Instantiate(playerPrefab);
        player.transform.SetParent(playerRoot);
        player.transform.position = playerRoot.transform.position;
        return player;
    }
    private void OnGameStatusChanged(GameStatus newGameStatus) {
        switch(newGameStatus) {
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