using UnityEngine;

public class ChickenPanelLayout : MonoBehaviour
{
    public RectTransform chickenGrid;
    
    // percentualne hodnoty - nastav podla obrazka
    [Range(0f, 1f)] public float gridLeft = 0.05f;
    [Range(0f, 1f)] public float gridRight = 0.95f;
    [Range(0f, 1f)] public float gridTop = 0.95f;    // od spodku!
    [Range(0f, 1f)] public float gridBottom = 0.45f; // hniezda su hornych ~50%

    void Start()
    {
        RectTransform panelRect = GetComponent<RectTransform>();
        
        chickenGrid.anchorMin = new Vector2(gridLeft, gridBottom);
        chickenGrid.anchorMax = new Vector2(gridRight, gridTop);
        chickenGrid.offsetMin = Vector2.zero;
        chickenGrid.offsetMax = Vector2.zero;
    }
}