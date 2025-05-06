using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTypeManager : Singleton<GameTypeManager>
{

    private int gridWidth;
    private int gridHeight;
    public int gridSize;

    [SerializeField] GridScaler gs;

    [SerializeField] Button threeXGrid;
    [SerializeField] Button fourXGrid;
    [SerializeField] Button fiveXGrid;
    
    
    //private int gridIndex = 0;

    private GameType currentGameType;

    public enum GameType
    {
        ThreeXThree, FourXFour, FiveXFive
    }


    void Start()
    {
        threeXGrid.onClick.AddListener(() => SetGrid(3, 3));
        threeXGrid.onClick.AddListener(() => AdjustListener(threeXGrid, fourXGrid, fiveXGrid));
        threeXGrid.onClick.AddListener(() => SetGameType(GameType.ThreeXThree));

        fourXGrid.onClick.AddListener(() => SetGrid(4, 4));
        fourXGrid.onClick.AddListener(() => AdjustListener(fourXGrid, threeXGrid, fiveXGrid));
        fourXGrid.onClick.AddListener(() => SetGameType(GameType.FourXFour));

        fiveXGrid.onClick.AddListener(() => SetGrid(5, 5));
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
        a.GetComponentInChildren<TMP_Text>().color = new Color32(0, 231, 0, 255);
        b.interactable = true;
        b.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
        c.interactable = true;
        c.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
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
    public void SetGrid(int columns, int rows)
    {         
        LightManager.Instance.SetLightGrid(columns * rows);        
        gs.SetGrid(columns, rows);
        gridSize = columns * rows;
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
