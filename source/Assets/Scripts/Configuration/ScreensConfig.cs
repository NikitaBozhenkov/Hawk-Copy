using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "ScreensConfig", menuName = "ScreensConfig", order = 3)]
    public class ScreensConfig : ScriptableObject
    {
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private LevelFinishMenuConfig levelFinishMenuConfig;

        public MainMenu MainMenu => mainMenu;
        public LevelFinishMenuConfig LevelFinishMenuConfig => levelFinishMenuConfig;
    }
}