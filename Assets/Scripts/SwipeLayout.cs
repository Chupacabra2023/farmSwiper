using System.Collections;
using UnityEngine;

public class SwipeLayout : MonoBehaviour
{
    public RectTransform content;
    public RectTransform[] panels;

   IEnumerator Start()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        
        RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        float width = canvasRect.rect.width;
        float height = canvasRect.rect.height;
        
        content.sizeDelta = new Vector2(width * panels.Length, height);
        content.anchoredPosition = new Vector2(0, 0);
        content.anchorMin = new Vector2(0, 0);
        content.anchorMax = new Vector2(0, 0);
        content.pivot = new Vector2(0, 0);

content.sizeDelta = new Vector2(width * panels.Length, height);
        
        for (int i = 0; i < panels.Length; i++)
        {
              panels[i].anchorMin = new Vector2(0, 0);
    panels[i].anchorMax = new Vector2(0, 0);
    panels[i].pivot = new Vector2(0, 0);
    panels[i].sizeDelta = new Vector2(width, height);
    panels[i].anchoredPosition = new Vector2(width * i, 0);
            panels[i].sizeDelta = new Vector2(width, height);
            panels[i].anchoredPosition = new Vector2(width * i, 0);
        }
    }
}
