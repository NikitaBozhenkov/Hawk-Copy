using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "LevelFinishMenuConfig", menuName = "LevelFinishMenuConfig", order = 3)]
    public class LevelFinishMenuConfig : ScriptableObject
    {
        [SerializeField] private LevelFinishMenu levelFinishMenu;
        [SerializeField] private string levelFailText;
        [SerializeField] private string levelSuccessText;

        public LevelFinishMenu LevelFinishMenu => levelFinishMenu;
        public string LevelFailText => levelFailText;
        public string LevelSuccessText => levelSuccessText;
    }
}