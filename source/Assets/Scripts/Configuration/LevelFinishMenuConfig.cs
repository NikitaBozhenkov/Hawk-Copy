using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "LevelFinishMenuConfig", menuName = "LevelFinishMenuConfig", order = 3)]
    public class LevelFinishMenuConfig : ScriptableObject
    {
        [SerializeField] private LevelFinishScreen _levelFinishScreen;
        [SerializeField] private string levelFailText;
        [SerializeField] private string levelSuccessText;

        public LevelFinishScreen LevelFinishScreen => _levelFinishScreen;
        public string LevelFailText => levelFailText;
        public string LevelSuccessText => levelSuccessText;
    }
}