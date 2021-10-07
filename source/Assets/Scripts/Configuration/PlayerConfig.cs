using Player;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 4)]
    public class PlayerConfig : CharacterConfig
    {
        [SerializeField] private float spawnOffsetFromBottomCentre;

        public float SpawnOffsetFromBottomCentre => spawnOffsetFromBottomCentre;
    }
}