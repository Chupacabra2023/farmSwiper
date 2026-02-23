using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections; 

public class Bucket : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;
    public RectTransform wellTransform;
    public RectTransform cowTransform;
    private bool isFilled = false;
    private Vector3 originalPosition;
    private Image currentImage;
    public Sprite filledBucketSprite;
    public Sprite emptyBucketSprite;

    void OnEnable()
    {
        BucketManager.OnBucketFilled += FillBucket;
        BucketManager.OnBucketEmptied += EmptyBucket;
    }

    void OnDisable()
    {
        BucketManager.OnBucketFilled -= FillBucket;
          BucketManager.OnBucketEmptied -= EmptyBucket;
    }

   IEnumerator Start()
{
    yield return null; // počkaj kým SwipeLayout nastaví pozície
    scrollRect = FindAnyObjectByType<ScrollRect>();
 
    currentImage = GetComponent<Image>();
}

    void FillBucket()
    {
        if (!isFilled)
        {
            isFilled = true;
            Debug.Log("Bucket filled!");
            currentImage.sprite = filledBucketSprite;
        }
    }
    void EmptyBucket()
    {
        if (isFilled)
        {
            isFilled = false;
            Debug.Log("Bucket emptied!");
            currentImage.sprite = emptyBucketSprite;
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
         originalPosition = this.transform.position;
        scrollRect.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
{
    this.transform.position = eventData.position;
    
    if(RectTransformUtility.RectangleContainsScreenPoint(wellTransform, eventData.position) && !isFilled)
    {
        BucketManager.FillAllBuckets();
    }
    
    
}

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.enabled = true;
        this.transform.position = originalPosition;

        if(isFilled)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        foreach(var result in results)
        {
            if(result.gameObject.GetComponent<CowBehaviour>() != null)
            {
                Debug.Log("Cow watered!");
                BucketManager.EmptyAllBuckets();
                break;
            }
        }
    }

        
    }
}