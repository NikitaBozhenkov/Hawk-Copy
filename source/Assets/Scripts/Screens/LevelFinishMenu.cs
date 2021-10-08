using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Screens
{
    public class LevelFinishMenu : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private TextMeshProUGUI levelResult;
        [SerializeField] private Button startButton;

        public Canvas Canvas => canvas;

        public event UnityAction StatusConfirmed;

        public void Setup(Camera camera, string levelResult)
        {
            transform.SetParent(camera.transform);
            canvas.worldCamera = camera;
            canvas.planeDistance = distanceFromCamera;
            SetLevelResult(levelResult);
            startButton.onClick.AddListener(StatusConfirmed);
        }

        public void SetLevelResult(string value)
        {
            levelResult.text = value;
        }
    }
}