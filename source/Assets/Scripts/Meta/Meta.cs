using System;
using Configuration;
using Gameplay;
using Screens;
using UnityEngine;
using UnityEngine.Events;

namespace Meta
{
    public class Meta : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera gameplayCamera;
        [SerializeField] private Canvas uiCanvas;

        private Game game;
        private ScreenManager screenManager;
        private MainMenuScreen mainMenuScreen;
        private GameplayScreen gameplayScreen;
        private LevelFinishScreen levelFinishScreen;

        private void Start()
        {
            screenManager = new ScreenManager(gameConfig.ScreensConfig, uiCanvas);
            ChangeToScreen(GameStatus.InMenu);
        }

        private void ChangeToScreen(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.InMenu:
                    mainMenuScreen = ((MainMenuScreen) screenManager.ShowScreen(ScreenType.MainMenu));
                    mainMenuScreen.GameStarted -= StartGame;
                    mainMenuScreen.GameStarted += StartGame;
                    mainMenuScreen.Setup(GameStats.GetFinishedLevels());
                    break;
                case GameStatus.InGame:
                    gameplayScreen = ((GameplayScreen) screenManager.ShowScreen(ScreenType.Gameplay));
                    gameplayScreen.Setup(GameStats.GetFinishedLevels() + 1);
                    break;
                case GameStatus.LevelFailed:
                    levelFinishScreen = ((LevelFinishScreen) screenManager.ShowScreen(ScreenType.LevelFinish));
                    levelFinishScreen.StatusConfirmed -= OpenMainMenu;
                    levelFinishScreen.StatusConfirmed += OpenMainMenu;
                    levelFinishScreen.Setup(gameConfig.ScreensConfig.LevelFinishMenuConfig.LevelFailText);

                    break;
                case GameStatus.LevelFinished:
                    levelFinishScreen = ((LevelFinishScreen) screenManager.ShowScreen(ScreenType.LevelFinish));
                    levelFinishScreen.StatusConfirmed -= OpenMainMenu;
                    levelFinishScreen.StatusConfirmed += OpenMainMenu;
                    levelFinishScreen.Setup(gameConfig.ScreensConfig.LevelFinishMenuConfig.LevelSuccessText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void OpenMainMenu()
        {
            ChangeToScreen(GameStatus.InMenu);
        }

        private void SetupGame()
        {
            ChangeToScreen(GameStatus.InGame);
            game = new GameObject("game").AddComponent<Game>();
            game.Setup(gameConfig, gameplayCamera, gameplayScreen);
            game.GameFinished += OnGameFinished;
        }

        private void StartGame()
        {
            SetupGame();
            game.StartGame();
        }

        private void OnGameFinished(GameResultStatus result)
        {
            if (result == GameResultStatus.Success)
            {
                ChangeToScreen(GameStatus.LevelFinished);
                GameStats.IncreaseFinishedLevelsByOne();
            }
            else
            {
                ChangeToScreen(GameStatus.LevelFailed);
            }
        }
    }
}