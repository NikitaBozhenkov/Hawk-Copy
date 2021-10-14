using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Screens
{
    public class LevelFinishScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI levelResult;
        [SerializeField] private Button confirmStatusButton;

        public event UnityAction StatusConfirmed;

        public void Setup(string levelResult)
        {
            SetLevelResult(levelResult);
            confirmStatusButton.onClick.AddListener(StatusConfirmed);
        }

        private void OnDisable() {
            confirmStatusButton.onClick.RemoveAllListeners();
        }

        public void SetLevelResult(string value)
        {
            levelResult.text = value;
        }
    }
}