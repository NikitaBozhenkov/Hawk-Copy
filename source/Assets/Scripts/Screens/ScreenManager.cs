using System;
using Configuration;
using Gameplay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Screens
{
    public class ScreenManager
    {
        private readonly ScreensConfig screensConfig;
        private readonly MainMenu mainMenu;
        private readonly LevelFinishMenu levelFinishMenu;
        private readonly Camera camera;


        public event Action GameplayStarted;
        public event Action FinishStatusConfirmed;

        public ScreenManager(ScreensConfig screensConfig, GameStatus startGameStatus,
            ref Action<GameStatus> gameStatusChanged)
        {
            this.screensConfig = screensConfig;

            camera = new GameObject("mainCamera").AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 5;

            mainMenu = Object.Instantiate(this.screensConfig.MainMenu);
            mainMenu.StartGame += () => { GameplayStarted?.Invoke(); };
            mainMenu.Setup(camera, GameStats.GetFinishedLevels());
            levelFinishMenu = Object.Instantiate(screensConfig.LevelFinishMenuConfig.LevelFinishMenu);
            levelFinishMenu.StatusConfirmed += () => { FinishStatusConfirmed?.Invoke(); };
            levelFinishMenu.Setup(camera, "");

            OnGameStatusChanged(startGameStatus);
            gameStatusChanged += OnGameStatusChanged;
        }

        private void OnGameStatusChanged(GameStatus newStatus)
        {
            switch (newStatus)
            {
                case GameStatus.InMenu:
                    camera.gameObject.SetActive(true);
                    SetScreenActiveness(mainMenu.Canvas, true);
                    SetScreenActiveness(levelFinishMenu.Canvas, false);
                    mainMenu.SetFinishedLevels(GameStats.GetFinishedLevels());
                    break;
                case GameStatus.InGame:
                    camera.gameObject.SetActive(false);
                    break;
                case GameStatus.LevelFailed:
                    camera.gameObject.SetActive(true);
                    SetScreenActiveness(mainMenu.Canvas, false);
                    SetScreenActiveness(levelFinishMenu.Canvas, true);
                    levelFinishMenu.SetLevelResult(screensConfig.LevelFinishMenuConfig.LevelFailText);
                    break;
                case GameStatus.LevelFinished:
                    camera.gameObject.SetActive(true);
                    SetScreenActiveness(mainMenu.Canvas, false);
                    SetScreenActiveness(levelFinishMenu.Canvas, true);
                    levelFinishMenu.SetLevelResult(screensConfig.LevelFinishMenuConfig.LevelSuccessText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private void SetScreenActiveness(Canvas canvas, bool isActive)
        {
            canvas.gameObject.SetActive(isActive);
        }
    }
}