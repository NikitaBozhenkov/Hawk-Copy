using UnityEngine;

namespace Characters
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform maxHpBar;
        [SerializeField] private Transform currentHpBar;

        private void Start()
        {
            SetHpPart(1);
            Quaternion temp = transform.localRotation;
            temp.y = 0;
            transform.rotation = temp;
        }

        public void SetHpPart(float part)
        {
            part = Mathf.Clamp(part, 0, 1);
            Vector3 temp = currentHpBar.localScale;
            temp.x = part;
            currentHpBar.localScale = temp;
            temp = currentHpBar.localPosition;
            temp.x = -(1 - part) / 2;
            currentHpBar.localPosition = temp;
        }
    }
}