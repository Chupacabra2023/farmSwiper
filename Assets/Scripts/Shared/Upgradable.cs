using UnityEngine;
using UnityEngine.EventSystems;

public class Upgradeable : MonoBehaviour, IPointerClickHandler
{
    [Header("Upgrade")]
    public GameObject upgradePopupPrefab;
    public int upgradePrice = 50;
    public int upgradeLevel = 0;
    public int maxUpgradeLevel = 3;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (upgradeLevel >= maxUpgradeLevel) return;

        UpgradePopupManager.Instance.Toggle(
            this,
            upgradePopupPrefab,
            transform.parent,
            GetComponent<RectTransform>(),
            upgradePrice,
            OnUpgrade
        );
    }

    protected virtual void OnUpgrade()
    {
        upgradeLevel++;
        upgradePrice = Mathf.RoundToInt(upgradePrice * 1.5f);

        UpgradePopupManager.Instance.ClosePopup();

        OnUpgraded(upgradeLevel);
    }

    protected virtual void OnUpgraded(int newLevel)
    {
        Debug.Log($"{gameObject.name} upgraded to level {newLevel}");
    }
}
