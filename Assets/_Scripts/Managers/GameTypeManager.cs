using UnityEngine;
using UnityEngine.UI;

public class GameTypeManager : Singleton<GameTypeManager>
{

    private int gridWidth;
    private int gridHeight;

    private float gridOffsetX;
    private float gridOffsetY;

    private float startingPosX;
    private float startingPosY;

    [SerializeField] float startingX;
    [SerializeField] float startingY;
    [SerializeField] float size;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;

    [SerializeField] Button threeXGrid;
    [SerializeField] Button fourXGrid;
    [SerializeField] Button fiveXGrid;
    
    public Vector2[,] gridCoordinates;
    private int gridIndex = 0;

    private GameType currentGameType;

    public enum GameType
    {
        ThreeXThree, FourXFour, FiveXFive
    }


    void Start()
    {
        threeXGrid.onClick.AddListener(() => SetGrid(3, 3, 5.15f, -5.15f, -5.1f, 5.175f, 5.75f));
        threeXGrid.onClick.AddListener(() => AdjustListener(threeXGrid, fourXGrid, fiveXGrid));
        threeXGrid.onClick.AddListener(() => SetGameType(GameType.ThreeXThree));

        fourXGrid.onClick.AddListener(() => SetGrid(4, 4, 3.82f, -3.82f, -5.7f, 5.75f, 4.25f));
        fourXGrid.onClick.AddListener(() => AdjustListener(fourXGrid, threeXGrid, fiveXGrid));
        fourXGrid.onClick.AddListener(() => SetGameType(GameType.FourXFour));

        fiveXGrid.onClick.AddListener(() => SetGrid(5, 5, 3.05f, -3.05f, -6.05f, 6.105f, 3.4f));
        fiveXGrid.onClick.AddListener(() => AdjustListener(fiveXGrid, fourXGrid, threeXGrid));
        fiveXGrid.onClick.AddListener(() => SetGameType(GameType.FiveXFive));

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

    void SetGameType(GameType gameType)
    {
        currentGameType = gameType;
    }

    public GameType GetGameType()
    {
        return currentGameType;
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
    public void SetGrid(int width, int height, float offSetX, float offSetY, float startingX, float startingY, float objSize)
    {      
        gridWidth = width;
        gridHeight = height;
        gridOffsetX = offSetX;
        gridOffsetY = offSetY;
        startingPosX = (Screen.width / 1920) / width + startingX;
        startingPosY = (Screen.height/ 1080) / height + startingY;

        gridIndex = 0;

        LightManager.Instance.SetLightGrid(gridWidth * gridHeight, objSize);     
        
        gridCoordinates = new Vector2[gridWidth, gridHeight];
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float xPos = startingPosX + (x * gridOffsetX);
                float yPos = startingPosY + (y * gridOffsetY);                
                
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
