using System;
using System.Collections.Generic;
using System.Linq;
using Configuration;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Screens
{
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
        }

        public BaseScreen ShowScreen<T>() where T : BaseScreen
        {
            CloseAllScreens();

            BaseScreen screen = FindScreen<T>(screens);
            if (screen != null)
            {
                screen.gameObject.SetActive(true);
                return screen;
            }

            screen = Object.Instantiate(FindScreen<T>(screensConfig.Screens), canvas.transform);
            if (screen == null) throw new ArgumentException(nameof(T));
            screens.Add(screen);
            return screen;
        }

        private void CloseAllScreens()
        {
            screens.ForEach(screen => screen.gameObject.SetActive(false));
        }

        private BaseScreen FindScreen<T>(IEnumerable<BaseScreen> baseScreens) where T : BaseScreen
        {
            return baseScreens.FirstOrDefault(baseScreen => baseScreen.GetType() == typeof(T));
        }
    }
}