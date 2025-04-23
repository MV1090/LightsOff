using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;
    private GridLayoutGroup grid;
    private RectTransform rt;

    private void Start()
    {
        if (grid == null) 
            grid = GetComponent<GridLayoutGroup>();
        
        rt = GetComponent<RectTransform>();

        SetGrid(columns, rows);
    }

    public void SetGrid(int col, int row)
    {
        float width = rt.rect.width - grid.padding.left - grid.padding.right - (grid.spacing.x * (columns - 1));
        float height = rt.rect.height - grid.padding.top - grid.padding.bottom - (grid.spacing.y * (rows - 1));

        float cellWidth = width / col;
        float cellHeight = height / row;

        grid.constraintCount = col;
        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
