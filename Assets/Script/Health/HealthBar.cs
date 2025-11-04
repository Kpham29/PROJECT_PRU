using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    // Không có SetTarget / LateUpdate — UI tĩnh
    public void UpdateBar(int currentValue, int maxValue)
    {
        if (fillBar != null)
            fillBar.fillAmount = (float)currentValue / Mathf.Max(1, maxValue);
        if (valueText != null)
            valueText.text = $"{currentValue} / {maxValue}";
    }
}
