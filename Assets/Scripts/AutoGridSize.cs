using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AutoGridSize : MonoBehaviour
{
    public int columns = 3;
    public int rows = 2;

    void Update()
    {
        var rect = GetComponent<RectTransform>();
        var grid = GetComponent<GridLayoutGroup>();

        float w = rect.rect.width / columns;
        float h = rect.rect.height / rows;

        grid.cellSize = new Vector2(w, h);
    }
}
