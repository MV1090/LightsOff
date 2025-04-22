using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;
    private GridLayoutGroup grid;

    void Update()
    {
        if (grid == null) grid = GetComponent<GridLayoutGroup>();
        RectTransform rt = GetComponent<RectTransform>();

        float width = rt.rect.width - grid.padding.left - grid.padding.right - (grid.spacing.x * (columns - 1));
        float height = rt.rect.height - grid.padding.top - grid.padding.bottom - (grid.spacing.y * (rows - 1));

        float cellWidth = width / columns;
        float cellHeight = height / rows;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
