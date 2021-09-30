using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;

    [Header("Player")]
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Transform playerRoot;

    [Header("Chunk Settings")] 
    [SerializeField] private Chunk[] startChunkPrefabs;
    [SerializeField] private int startChunksCount;
    [SerializeField] private Chunk[] enemyChunkPrefabs;
    [SerializeField] private int enemyChunksCount;
    [SerializeField] private Transform chunksRoot;

    private Spawner spawner;
    private Character player;
    private List<Chunk> chunks = new List<Chunk>();

    private Action<GameStatus> gameStatusChanged;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        spawner = new Spawner(chunksRoot, playerRoot, ref gameStatusChanged);
        MakeSpawns();
        SetupPlayer();
        SetupChunks();
    }

    private void MakeSpawns()
    {
        player = spawner.SpawnPlayer(playerPrefab);
        chunks.AddRange(spawner.SpawnChunks(startChunkPrefabs, startChunksCount));
        chunks.AddRange(spawner.SpawnChunks(enemyChunkPrefabs, enemyChunksCount));
    }

    private void SetupPlayer()
    {
        player.GetComponent<Boundaries>().Setup(mainCamera);
        player.Setup();
    }

    private void SetupChunks()
    {
        foreach (Chunk chunk in chunks)
        {
            chunk.Setup();
        }
    }
}