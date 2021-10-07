using Characters;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "CharacterConfig", order = 3)]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] protected Character prefab;

        public Character Prefab => prefab;
    }
}