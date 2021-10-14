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
        private bool wasMainMenuSet;
        private GameplayScreen gameplayScreen;
        private LevelFinishScreen levelFinishScreen;
        private bool wasLevelFinishMenuSet;

        private void Start()
        {
            screenManager = new ScreenManager(gameConfig.ScreensConfig, uiCanvas);
            wasMainMenuSet = false;
            wasLevelFinishMenuSet = false;
            ChangeToScreen(GameStatus.InMenu);
        }

        private void ChangeToScreen(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.InMenu:
                    mainMenuScreen = ((MainMenuScreen) screenManager.ShowScreen(ScreenType.MainMenu));
                    if (!wasMainMenuSet)
                    {
                        mainMenuScreen.GameStarted += StartGame;
                        mainMenuScreen.Setup(GameStats.GetFinishedLevels());
                        wasMainMenuSet = true;
                    }
                    mainMenuScreen.SetFinishedLevels(GameStats.GetFinishedLevels());
                    break;
                case GameStatus.InGame:
                    gameplayScreen = ((GameplayScreen) screenManager.ShowScreen(ScreenType.Gameplay));
                    gameplayScreen.Setup(GameStats.GetFinishedLevels() + 1);
                    break;
                case GameStatus.LevelFailed:
                    levelFinishScreen = ((LevelFinishScreen) screenManager.ShowScreen(ScreenType.LevelFinish));
                    if (!wasLevelFinishMenuSet)
                    {
                        levelFinishScreen.StatusConfirmed += OpenMainMenu;
                        levelFinishScreen.Setup(gameConfig.ScreensConfig.LevelFinishMenuConfig.LevelFailText);
                        wasLevelFinishMenuSet = true;
                    }

                    break;
                case GameStatus.LevelFinished:
                    levelFinishScreen = ((LevelFinishScreen) screenManager.ShowScreen(ScreenType.LevelFinish));
                    if (!wasLevelFinishMenuSet)
                    {
                        levelFinishScreen.StatusConfirmed += OpenMainMenu;
                        levelFinishScreen.Setup(gameConfig.ScreensConfig.LevelFinishMenuConfig.LevelSuccessText);
                        wasLevelFinishMenuSet = true;
                    }

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