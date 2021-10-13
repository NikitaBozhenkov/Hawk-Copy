using System;
using System.Collections.Generic;
using System.Linq;
using Configuration;
using Gameplay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Screens
{
    public enum ScreenType
    {
        MainMenu,
        Gameplay,
        LevelFinish
    }

    public class ScreenManager
    {
        private readonly ScreensConfig screensConfig;
        private readonly Canvas canvas;
        private readonly List<BaseScreen> screens;

        public ScreenManager(ScreensConfig screensConfig, Canvas canvas)
        {
            this.screensConfig = screensConfig;
            this.canvas = canvas;
            screens = new List<BaseScreen>();

            //levelFinishScreen = Object.Instantiate(screensConfig.LevelFinishMenuConfig.LevelFinishScreen);
            //levelFinishScreen.StatusConfirmed += () => { FinishStatusConfirmed?.Invoke(); };
        }

        private void CloseAllScreens()
        {
            screens.ForEach(screen => screen.gameObject.SetActive(false));
        }

        private BaseScreen FindScreen<T>() where T : BaseScreen
        {
            return screens.FirstOrDefault((BaseScreen baseScreen) => baseScreen.GetType() == typeof(T));
        }

        public BaseScreen ShowScreen(ScreenType type)
        {
            CloseAllScreens();
            BaseScreen screen;
            switch (type)
            {
                case ScreenType.MainMenu:
                    screen = FindScreen<MainMenuScreen>();
                    if(screen == null) {
                        screen = Object.Instantiate(screensConfig.MainMenuScreen, canvas.transform);
                        screens.Add(screen);
                    }
                    else
                    {
                        screen.gameObject.SetActive(true);
                    }
                    break;
                case ScreenType.Gameplay:
                    screen = FindScreen<GameplayScreen>();
                    if(screen == null) {
                        screen = Object.Instantiate(screensConfig.GameplayScreen, canvas.transform);
                        screens.Add(screen);
                    } 
                    else {
                        screen.gameObject.SetActive(true);
                    }
                    break;
                case ScreenType.LevelFinish:
                    screen = FindScreen<LevelFinishScreen>();
                    if(screen == null) {
                        screen = Object.Instantiate(screensConfig.LevelFinishMenuConfig.LevelFinishScreen, canvas.transform);
                        screens.Add(screen);
                    } 
                    else {
                        screen.gameObject.SetActive(true);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return screen;
        }
    }
}