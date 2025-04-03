using UnityEngine;
using UnityEngine.UI;

public class GameTypeManager : Singleton<GameTypeManager>
{
    // Define the size of the grid
    public int gridWidth; // 3 columns
    public int gridHeight; // 3 rows

    public float gridOffsetX;
    public float gridOffsetY;

    [SerializeField] Button threeXGrid;
    [SerializeField] Button fourXGrid;
    [SerializeField] Button fiveXGrid;

    // Create a 2D array to store the X and Y coordinates
    public Vector2[,] gridCoordinates;
    private int gridIndex = 0;

    void Start()
    {
        threeXGrid.onClick.AddListener(() => SetGrid(3, 3, 6, -6, 3));
        threeXGrid.onClick.AddListener(() => AdjustListener(threeXGrid, fourXGrid, fiveXGrid));
        threeXGrid.onClick.Invoke();

        fourXGrid.onClick.AddListener(() => SetGrid(4, 4, 4, -4, 2.5f));
        fourXGrid.onClick.AddListener(() => AdjustListener(fourXGrid, threeXGrid, fiveXGrid));

        fiveXGrid.onClick.AddListener(() => SetGrid(5, 5, 3, -3, 2));
        fiveXGrid.onClick.AddListener(() => AdjustListener(fiveXGrid, fourXGrid, threeXGrid));
    }


    void AdjustListener(Button a, Button b, Button c)
    {
        a.interactable = false;
        b.interactable = true;
        c.interactable = true;
    }

    public void SetGrid(int width, int height, int offSetX, int offSetY, float objSize)
    {      
        gridWidth = width;
        gridHeight = height;
        gridOffsetX = offSetX;
        gridOffsetY = offSetY;

        gridIndex = 0;

        LightManager.Instance.SetLightGrid(gridWidth * gridHeight, objSize);     
        
        gridCoordinates = new Vector2[gridWidth, gridHeight];
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float xPos = x * gridOffsetX;
                float yPos = y * gridOffsetY;                
                
                gridCoordinates[x, y] = new Vector2(xPos, yPos);

                LightManager.Instance.playableLightObjects[gridIndex].transform.position = gridCoordinates[x, y];

                gridIndex++;
            }
        }
    }
}
