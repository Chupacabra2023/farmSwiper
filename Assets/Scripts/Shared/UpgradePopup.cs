using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePopup : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public Button upgradeButton;

    private System.Action onUpgrade;

    public void Setup(int price, System.Action onUpgradeCallback)
    {
        priceText.text = $"{price}";
        onUpgrade = onUpgradeCallback;
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => onUpgrade?.Invoke());
    }
}