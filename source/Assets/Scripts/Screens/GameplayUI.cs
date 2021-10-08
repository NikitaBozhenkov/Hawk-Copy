using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private float distanceFromCamera;
        [SerializeField] private Image progressScale;
        [SerializeField] private Image progressScaleFinishedPart;
        [SerializeField] private TextMeshProUGUI levelNumber;

        private float scaleLength;

        public void Setup(Camera camera, int levelNumber)
        {
            transform.SetParent(camera.transform);
            canvas.worldCamera = camera;
            canvas.planeDistance = distanceFromCamera;
            this.levelNumber.text = levelNumber.ToString();
            scaleLength = progressScale.rectTransform.rect.width;
            SetLevelProgress(0);
        }

        public void SetLevelProgress(float finishedPart)
        {
            if (finishedPart < 0)
            {
                throw new ArgumentException(nameof(finishedPart));
            }

            float width = Mathf.Clamp(finishedPart * scaleLength, 0, scaleLength);
            progressScaleFinishedPart.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
    }
}