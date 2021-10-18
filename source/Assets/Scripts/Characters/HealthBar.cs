using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image maskImage;

        private void Start()
        {
            SetHpPart(1);
        }

        public void SetHpPart(float part)
        {
            part = Mathf.Clamp(part, 0, 1);
            maskImage.fillAmount = part;
        }
    }
}