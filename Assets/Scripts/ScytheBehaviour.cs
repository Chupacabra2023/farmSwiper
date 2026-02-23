using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScytheBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image currentImage;
    public Sprite sharpSprite;
    public Sprite dullSprite;
    private bool isSharp = true;
    private Vector3 originalPosition;
    private ScrollRect scrollRect;

    IEnumerator Start()
    {
        yield return null;
        scrollRect = FindAnyObjectByType<ScrollRect>();
        currentImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        scrollRect.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        if (!isSharp) return;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            var grass = result.gameObject.GetComponent<GrassBehaviour>();
            if (grass != null && !grass.IsCut)
            {
                grass.Cut();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.enabled = true;
        transform.position = originalPosition;
    }

    public void Sharpen()
    {
        isSharp = true;
        currentImage.sprite = sharpSprite;
    }

    public void Dull()
    {
        isSharp = false;
        currentImage.sprite = dullSprite;
    }
}