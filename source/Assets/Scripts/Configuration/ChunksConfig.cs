using Chunks;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "ChunksConfig", menuName = "ChunksConfig", order = 2)]
    public class ChunksConfig : ScriptableObject
    {
        [SerializeField] private Chunk[] startChunkPrefabs;
        [SerializeField] private int startChunksCount;
        [SerializeField] private Chunk[] enemyChunkPrefabs;
        [SerializeField] private int enemyChunksCount;
        [SerializeField] private Chunk[] finishChunkPrefabs;
        [SerializeField] private int finishChunksCount;

        public Chunk[] StartChunkPrefabs => startChunkPrefabs;
        public int StartChunksCount => startChunksCount;
        public Chunk[] EnemyChunkPrefabs => enemyChunkPrefabs;
        public int EnemyChunksCount => enemyChunksCount;
        public Chunk[] FinishChunkPrefabs => finishChunkPrefabs;
        public int FinishChunksCount => finishChunksCount;
    }
}