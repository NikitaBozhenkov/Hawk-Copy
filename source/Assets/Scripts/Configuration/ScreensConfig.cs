using Screens;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "ScreensConfig", menuName = "ScreensConfig", order = 3)]
    public class ScreensConfig : ScriptableObject
    {
        [SerializeField] private BaseScreen[] screens;

        public BaseScreen[] Screens => screens;
    }
}