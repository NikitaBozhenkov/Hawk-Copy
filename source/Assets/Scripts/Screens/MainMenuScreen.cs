using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Screens
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI levelsFinished;
        [SerializeField] private Button startButton;

        public event UnityAction GameStarted;

        public void Setup(int levelsFinished)
        {
            SetFinishedLevels(levelsFinished);
            startButton.onClick.AddListener(GameStarted);
        }

        public void SetFinishedLevels(int value)
        {
            levelsFinished.text = value.ToString();
        }
    }
}