using UnityEngine;

public class UpgradePopupManager : MonoBehaviour
{
    public static UpgradePopupManager Instance { get; private set; }

    private GameObject currentPopup;
    private Upgradeable currentOwner;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Returns true = popup opened, false = popup closed
    public bool Toggle(Upgradeable owner, GameObject popupPrefab, Transform parent, RectTransform ownerRect, int price, System.Action onUpgrade)
    {
        if (currentOwner == owner)
        {
            ClosePopup();
            return false;
        }

        ClosePopup();

        currentPopup = Instantiate(popupPrefab, parent);

        RectTransform popupRect = currentPopup.GetComponent<RectTransform>();

Vector3 worldPos = ownerRect.position;
worldPos.y += ownerRect.rect.height * ownerRect.lossyScale.y / 2f + popupRect.rect.height * popupRect.lossyScale.y / 2f;
popupRect.position = worldPos;

        currentPopup.GetComponent<UpgradePopup>().Setup(price, onUpgrade);
        currentOwner = owner;
        return true;
    }

    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
        currentOwner = null;
    }
}
