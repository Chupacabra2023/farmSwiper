using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeSnap : MonoBehaviour, IEndDragHandler
{
    public ScrollRect scrollRect;
    public int panelCount = 2;

    public void OnEndDrag(PointerEventData eventData)
    {
        float normalized = scrollRect.horizontalNormalizedPosition;
        int currentPanel = Mathf.RoundToInt(normalized * (panelCount - 1));
        float target = (panelCount > 1) ? (float)currentPanel / (panelCount - 1) : 0;
        StartCoroutine(SnapTo(target));
    }

    IEnumerator SnapTo(float target)
    {
        float elapsed = 0f;
        float duration = 0.3f;
        float start = scrollRect.horizontalNormalizedPosition;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(start, target, elapsed / duration);
            yield return null;
        }
        scrollRect.horizontalNormalizedPosition = target;
    }
}