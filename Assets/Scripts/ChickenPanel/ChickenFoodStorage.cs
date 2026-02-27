using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Corn :Upgradeable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;
    private bool isFilled = false;
    private Vector3 originalPosition;
    private Image currentImage;

    public Sprite fullCornSprite;
    public Sprite emptyCornSprite;

    public GameObject dragIconPrefab; // sprite ktorý nasleduje myš
    private GameObject dragIcon;
 

    void OnEnable()
    {
        CornManager.OnCornFilled += FillCorn;
        CornManager.OnCornEmptied += EmptyCorn;
    }

    void OnDisable()
    {
        CornManager.OnCornFilled -= FillCorn;
        CornManager.OnCornEmptied -= EmptyCorn;
    }

    IEnumerator Start()
{
    yield return null;
    scrollRect = FindAnyObjectByType<ScrollRect>();
    currentImage = GetComponent<Image>();
    
    // TEMP - odstráň keď dorobíš obchod
    CornManager.FillAllCorn();
}
    void FillCorn()
    {
        if (!isFilled)
        {
            isFilled = true;
            currentImage.sprite = fullCornSprite;
        }
    }

    void EmptyCorn()
    {
        if (isFilled)
        {
            isFilled = false;
            currentImage.sprite = emptyCornSprite;
        }
    }

   public void OnBeginDrag(PointerEventData eventData)
{
    originalPosition = transform.position;
    scrollRect.enabled = false;

    // vytvor ghost na pozícii myši
    dragIcon = Instantiate(dragIconPrefab, transform.parent);
    dragIcon.transform.position = eventData.position;
    dragIcon.GetComponent<Image>().raycastTarget = false; // aby neblokoval raycast
}

public void OnDrag(PointerEventData eventData)
{
    if (dragIcon != null)
        dragIcon.transform.position = eventData.position;
}

public void OnEndDrag(PointerEventData eventData)
{
    if (dragIcon != null)
    {
        Destroy(dragIcon);
        dragIcon = null;
    }

    scrollRect.enabled = true;
    transform.position = originalPosition;

    var results = new System.Collections.Generic.List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);

    Debug.Log($"Raycast hits: {results.Count}");
    foreach (var result in results)
    {
        Debug.Log($"Hit: {result.gameObject.name}");
    }

    if (isFilled)
    {
        foreach (var result in results)
        {
            ChickenFood chickenFood = result.gameObject.GetComponent<ChickenFood>();
            if (chickenFood != null)
            {
                chickenFood.AddFood(100f);
                CornManager.EmptyAllCorn();
                Debug.Log("Jedlo pridané!");
                break;
            }
        }
    }
}

  protected override void OnUpgraded(int newLevel)
    {
        
        Debug.Log($"Food upgraded! Max food: 100");
    }

}