using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "ScreensConfig", menuName = "ScreensConfig", order = 3)]
    public class ScreensConfig : ScriptableObject
    {
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private GameplayScreen gameplayScreen;
        [SerializeField] private LevelFinishMenuConfig levelFinishMenuConfig;

        public MainMenuScreen MainMenuScreen => mainMenuScreen;
        public GameplayScreen GameplayScreen => gameplayScreen;
        public LevelFinishMenuConfig LevelFinishMenuConfig => levelFinishMenuConfig;
    }
}