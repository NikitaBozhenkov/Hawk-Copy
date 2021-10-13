using System.Collections.Generic;
using Configuration;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Chunks
{
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
            Transform root)
        {
            this.chunksConfig = chunksConfig;
            this.gameField = gameField;
            currentChunkSpawnPosition = new Vector3(
                gameField.center.x,
                spawnY,
                gameField.center.y
            );
            chunks = new List<Chunk>();

            chunksRoot = new GameObject().transform;
            chunksRoot.name = "chunksRoot";
            chunksRoot.position = new Vector3(gameField.center.x, spawnY, gameField.center.y);
            chunksRoot.SetParent(root);
        }

        public float GetGameLength()
        {
            return (chunksConfig.StartChunksCount + chunksConfig.EnemyChunksCount) * gameField.height;
        }

        private Chunk[] SpawnChunks(Chunk[] chunkPrefabs, int chunksCount, bool spawnRandomly = false)
        {
            Chunk[] spawnedChunks = new Chunk[chunksCount];

            for (int i = 0; i < chunksCount; ++i)
            {
                Chunk chunkToSpawn = !spawnRandomly
                    ? chunkPrefabs[i % chunkPrefabs.Length]
                    : chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

                Chunk spawnedChunk = Object.Instantiate(chunkToSpawn,
                    currentChunkSpawnPosition,
                    chunkToSpawn.transform.rotation);
                spawnedChunk.transform.SetParent(chunksRoot);
                spawnedChunk.transform.localScale = new Vector3(gameField.width / 10, 1, gameField.height / 10);
                currentChunkSpawnPosition.z += spawnedChunk.GetComponent<MeshRenderer>().bounds.size.z;
                spawnedChunks[i] = spawnedChunk;
            }

            return spawnedChunks;
        }

        public void MakeSpawns(Transform bulletsRoot)
        {
            chunks.AddRange(SpawnChunks(chunksConfig.StartChunkPrefabs, chunksConfig.StartChunksCount));
            chunks.AddRange(SpawnChunks(chunksConfig.EnemyChunkPrefabs, chunksConfig.EnemyChunksCount, true));
            chunks.AddRange(SpawnChunks(chunksConfig.FinishChunkPrefabs, chunksConfig.FinishChunksCount));

            foreach (Chunk chunk in chunks)
            {
                chunk.Setup(bulletsRoot);
            }
        }
    }
}