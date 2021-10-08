using UnityEngine;

namespace Gameplay
{
    public static class GameStats
    {
        private const string levelsFinishedKey = "levelsFinishedKey";

        public static void IncreaseFinishedLevelsByOne()
        {
            PlayerPrefs.SetInt(levelsFinishedKey, GetFinishedLevels() + 1);
        }

        public static int GetFinishedLevels()
        {
            return PlayerPrefs.GetInt(levelsFinishedKey, 0);
        }
    }
}