using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Screens
{
    public class LevelFinishScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI loseText;
        [SerializeField] private Button confirmStatusButton;

        public event UnityAction StatusConfirmed;

        public void Setup(GameResultStatus gameResult)
        {
            SetLevelResult(gameResult);
            confirmStatusButton.onClick.AddListener(StatusConfirmed);
        }

        public void SetLevelResult(GameResultStatus result)
        {
            if (result == GameResultStatus.Success)
            {
                winText.gameObject.SetActive(true);
                loseText.gameObject.SetActive(false);
            }
            else
            {
                winText.gameObject.SetActive(false);
                loseText.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            confirmStatusButton.onClick.RemoveAllListeners();
        }
    }
}