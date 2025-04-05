using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class GameTypeManager : Singleton<GameTypeManager>
{
    
    public int gridWidth; 
    public int gridHeight;

    public float gridOffsetX;
    public float gridOffsetY;

    [SerializeField] Button threeXGrid;
    [SerializeField] Button fourXGrid;
    [SerializeField] Button fiveXGrid;
    
    public Vector2[,] gridCoordinates;
    private int gridIndex = 0;

    void Start()
    {
        threeXGrid.onClick.AddListener(() => SetGrid(3, 3, 6, -6, 3));
        threeXGrid.onClick.AddListener(() => AdjustListener(threeXGrid, fourXGrid, fiveXGrid));        

        fourXGrid.onClick.AddListener(() => SetGrid(4, 4, 4, -4, 2.5f));
        fourXGrid.onClick.AddListener(() => AdjustListener(fourXGrid, threeXGrid, fiveXGrid));

        fiveXGrid.onClick.AddListener(() => SetGrid(5, 5, 3, -3, 2));
        fiveXGrid.onClick.AddListener(() => AdjustListener(fiveXGrid, fourXGrid, threeXGrid));

        foreach (LightObject lightObject in LightManager.Instance.allLightObjects)
        {
            lightObject.OnGameStart += GameStarted;            
        }
    }

    /// <summary>
    /// When button is pressed sets it to be inactive, more for looks and feel than functionality.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    void AdjustListener(Button a, Button b, Button c)
    {
        a.interactable = false;
        b.interactable = true;
        c.interactable = true;
    }

    /// <summary>
    /// Create grid sized based on width and height. 
    /// Offset is used to set position of lights.
    /// Size is sets how big the objects are. The bigger the grid the smaller the objects. 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="offSetX"></param>
    /// <param name="offSetY"></param>
    /// <param name="objSize"></param>
    //Width and height need to be adjusted to wrok on screen wise instead of set numbers. 
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

    /// <summary>
    /// Sets Buttons active on screen.
    /// </summary>
    public void SetGridActive()
    {
        threeXGrid.gameObject.SetActive(true);
        fourXGrid.gameObject.SetActive(true);
        fiveXGrid.gameObject.SetActive(true);

        if (LightManager.Instance.playableLightObjects.Count <= 0)
        {
            threeXGrid.onClick.Invoke();
        }        
    }

    /// <summary>
    /// Removes buttons when game starts
    /// </summary>
    private void GameStarted()
    {
        threeXGrid.gameObject.SetActive(false);
        fourXGrid.gameObject.SetActive(false);
        fiveXGrid.gameObject.SetActive(false);
    }

}
