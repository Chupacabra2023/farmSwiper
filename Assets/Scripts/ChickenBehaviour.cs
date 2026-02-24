using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChickenMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float minY, maxY;
    public float moveSpeed = 500f;
    public bool isSitting = false;

    [Header("Small Chickens")]
    public Sprite smallYellow;
    public Sprite smallBrown;
    public Sprite smallBlack;

    [Header("Adult Chickens")]
    public Sprite adultYellow;
    public Sprite adultBrown;
    public Sprite adultBlack;

    private Image chickenImage;
    private RectTransform rectTransform;
    private float halfWidth;
    private bool isAdult = false;
    private Sprite adultSprite;

    private bool isDragging = false;
    private Vector2 originalPosition;
    private Canvas canvas;
    private Coroutine moveCoroutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        chickenImage = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>();
        halfWidth = canvas.GetComponent<RectTransform>().rect.width / 2f;

        AssignRandomColor();

        moveCoroutine = StartCoroutine(MoveRandomly());
        StartCoroutine(GrowUp());
    }

    public void OnBeginDrag(PointerEventData eventData)
{
    if (!isAdult) return;
    isDragging = true;
    originalPosition = transform.position;
    
    // pivot na stred
    rectTransform.pivot = new Vector2(0.5f, 0.5f);
    
    if (moveCoroutine != null)
        StopCoroutine(moveCoroutine);
}

    public void OnDrag(PointerEventData eventData)
{
    if (!isDragging) return;
    transform.position = eventData.position; 
}

    public void OnEndDrag(PointerEventData eventData)
{
    if (!isDragging) return;
    isDragging = false;

    rectTransform.pivot = new Vector2(0.5f, 0.5f);

    ChickenSlot slot = FindSlotUnderPointer(eventData);

    if (slot != null && slot.TryPlaceChicken(adultSprite, gameObject))
{
     isSitting = true; 
    StopAllCoroutines();
    gameObject.SetActive(false);
}
    else
    {
        transform.position = originalPosition;
        rectTransform.pivot = new Vector2(0f, 0f);
        moveCoroutine = StartCoroutine(MoveRandomly());
    }
}
public void ReturnToYard()
{
    isSitting = false;  //
    rectTransform.pivot = new Vector2(0.5f, 0.5f);
    // vrat na nahodnu poziciu v dvore
    rectTransform.anchoredPosition = new Vector2(
        Random.Range(-halfWidth, halfWidth),
        Random.Range(minY, maxY)
    );
    moveCoroutine = StartCoroutine(MoveRandomly());
}

    private ChickenSlot FindSlotUnderPointer(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            ChickenSlot slot = result.gameObject.GetComponent<ChickenSlot>();
            if (slot != null) return slot;
        }

        return null;
    }

    private void AssignRandomColor()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0: chickenImage.sprite = smallYellow; adultSprite = adultYellow; break;
            case 1: chickenImage.sprite = smallBrown; adultSprite = adultBrown; break;
            case 2: chickenImage.sprite = smallBlack; adultSprite = adultBlack; break;
        }
    }

    private IEnumerator GrowUp()
    {
        yield return new WaitForSeconds(10f);
        isAdult = true;
        chickenImage.sprite = adultSprite;
        Debug.Log("Chicken is now adult!");
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            Vector2 target = new Vector2(
                Random.Range(-halfWidth, halfWidth),
                Random.Range(minY, maxY)
            );

            while (Vector2.Distance(rectTransform.anchoredPosition, target) > 5f)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(
                    rectTransform.anchoredPosition,
                    target,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(2f, 6f));
        }
    }

    public bool IsAdult => isAdult;
}