using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Chunks;
using Player;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform mainCameraRoot;
    [SerializeField] private Camera mainCamera;

    [Header("Player Settings")]
    [SerializeField] private Transform playerRoot;
    [SerializeField] private Character playerPrefab;

    [Header("Chunk Settings")]
    [SerializeField] private Transform chunksRoot;
    [SerializeField] private Chunk[] startChunkPrefabs;
    [SerializeField] private int startChunksCount;
    [SerializeField] private Chunk[] enemyChunkPrefabs;
    [SerializeField] private int enemyChunksCount;
    [SerializeField] private Chunk[] finishChunkPrefabs;
    [SerializeField] private int finishChunksCount;

    [Header("Main Menu Settings")]
    [SerializeField] private Canvas mainMenuCanvas;

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

    private void FinishGame()
    {
        Destroy(player.gameObject);
        foreach (Chunk chunk in chunks)
        {
            Destroy(chunk.gameObject);
        }

        mainCamera.transform.position = mainCameraRoot.position;

        mainMenuCanvas.gameObject.SetActive(true);
    }

    private void MakeSpawns()
    {
        player = spawner.SpawnPlayer(playerPrefab);
        chunks.AddRange(spawner.SpawnChunks(startChunkPrefabs, startChunksCount));
        chunks.AddRange(spawner.SpawnChunks(enemyChunkPrefabs, enemyChunksCount));
        chunks.AddRange(spawner.SpawnChunks(finishChunkPrefabs, finishChunksCount));
    }

    private void SetupPlayer()
    {
        player.GetComponent<Boundaries>().Setup(mainCamera);
        player.Setup();
        player.EnableShooting();
    }

    private void SetupChunks()
    {
        foreach (Chunk chunk in chunks)
        {
            chunk.Setup();
        }

        chunks.Last().onTrigger += FinishGame;
    }
}