using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Screens
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private TextMeshProUGUI levelsFinished;
        [SerializeField] private Button startButton;

        public event UnityAction StartGame;

        public Canvas Canvas => canvas;

        public void Setup(Camera camera, int levelsFinished)
        {
            transform.SetParent(camera.transform);
            canvas.worldCamera = camera;
            canvas.planeDistance = distanceFromCamera;
            SetFinishedLevels(levelsFinished);
            startButton.onClick.AddListener(StartGame);
        }

        public void SetFinishedLevels(int value)
        {
            levelsFinished.text = value.ToString();
        }
    }
}