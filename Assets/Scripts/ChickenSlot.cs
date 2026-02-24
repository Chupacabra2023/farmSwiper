using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChickenSlot : MonoBehaviour, IPointerClickHandler
{
    public bool isOccupied = false;
    private Image slotImage;
    private GameObject chickenObject; // drzi referenciu na kurku

    void Awake()
    {
        slotImage = GetComponent<Image>();
        if (slotImage == null)
            slotImage = gameObject.AddComponent<Image>();
        
        slotImage.color = new Color(0, 0, 0, 0);
        slotImage.raycastTarget = true;
    }

    public bool TryPlaceChicken(Sprite chickenSprite, GameObject chicken)
    {
        if (isOccupied) return false;

        isOccupied = true;
        chickenObject = chicken;
        slotImage.sprite = chickenSprite;
        slotImage.color = Color.white;
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOccupied) return;

        // vrat kurku von
        chickenObject.SetActive(true);
        chickenObject.GetComponent<ChickenMovement>().ReturnToYard();

        // vycisti slot
        isOccupied = false;
        chickenObject = null;
        slotImage.sprite = null;
        slotImage.color = new Color(0, 0, 0, 0);
    }
}