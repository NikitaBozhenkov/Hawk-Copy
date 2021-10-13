using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class GameplayScreen : BaseScreen
    {
        [SerializeField] private Image progressScale;
        [SerializeField] private Image progressScaleFinishedPart;
        [SerializeField] private TextMeshProUGUI levelNumber;

        private float scaleLength;

        public void Setup(int levelNumber)
        {
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