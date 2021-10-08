using System;
using Configuration;
using Gameplay;
using Screens;
using UnityEngine;

namespace Meta
{
    public class Meta : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;

        private Game game;
        private ScreenManager screenManager;
        private GameStatus currentGameStatus;

        public GameStatus CurrentGameStatus
        {
            get => currentGameStatus;
            private set
            {
                currentGameStatus = value;
                gameStatusChanged?.Invoke(currentGameStatus);
            }
        }

        private Action<GameStatus> gameStatusChanged;

        private void Start()
        {
            screenManager = new ScreenManager(gameConfig.ScreensConfig, GameStatus.InMenu, ref gameStatusChanged);
            screenManager.GameplayStarted += StartGame;
            screenManager.FinishStatusConfirmed += () =>
            {
                if (CurrentGameStatus == GameStatus.LevelFinished)
                {
                    GameStats.IncreaseFinishedLevelsByOne();
                }

                CurrentGameStatus = GameStatus.InMenu;
            };
        }

        private void SetupGame()
        {
            game = new GameObject("game").AddComponent<Game>();
            game.Setup(gameConfig, GameStats.GetFinishedLevels() + 1);
            game.GameFinished += OnGameFinished;
        }

        private void StartGame()
        {
            SetupGame();
            CurrentGameStatus = GameStatus.InGame;
            game.StartGame();
        }

        private void OnGameFinished(GameResultStatus result)
        {
            CurrentGameStatus = (result == GameResultStatus.Success)
                ? GameStatus.LevelFinished
                : GameStatus.LevelFailed;
        }
    }
}