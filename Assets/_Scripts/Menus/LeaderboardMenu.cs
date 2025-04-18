using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : BaseMenu
{
    [Header("Buttons")]
    public Button endless;
    public Button beatTheClock;
    public Button delay;
    public Button threeXThree;
    public Button fourXFour;
    public Button fiveXFive;

    [Header("Text")]
    public TMP_Text scoreText;
    private enum Modes
    {
        Endless, BeatTheClock, Delay
    }

    private enum Types
    {
        ThreeXThree, FourXFour, FiveXFive
    }

    private Types currentType = Types.ThreeXThree;
    private Modes currentMode = Modes.Endless;

    private double score;
    private double rank;
    private string playerID;

    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.LeaderboardMenu;

        LeaderboardManager.Instance.ScoreToDisplay += UpdateText;

        endless.onClick.AddListener(() => SetMode(Modes.Endless));
        endless.onClick.AddListener(() => AdjustListener(endless, beatTheClock, delay));
        endless.onClick.AddListener(() => DisplayLeaderboard());

        beatTheClock.onClick.AddListener(() => SetMode(Modes.BeatTheClock));
        beatTheClock.onClick.AddListener(() => AdjustListener(beatTheClock, endless, delay));
        beatTheClock.onClick.AddListener(() => DisplayLeaderboard());

        delay.onClick.AddListener(() => SetMode(Modes.Delay));
        delay.onClick.AddListener(() => AdjustListener(delay, beatTheClock, endless));
        delay.onClick.AddListener(() => DisplayLeaderboard());

        threeXThree.onClick.AddListener(() => SetType(Types.ThreeXThree));
        threeXThree.onClick.AddListener(() => AdjustListener(threeXThree, fourXFour, fiveXFive));
        threeXThree.onClick.AddListener(() => DisplayLeaderboard());

        fourXFour.onClick.AddListener(() => SetType(Types.FourXFour));
        fourXFour.onClick.AddListener(() => AdjustListener(fourXFour, threeXThree, fiveXFive));
        fourXFour.onClick.AddListener(() => DisplayLeaderboard());

        fiveXFive.onClick.AddListener(() => SetType(Types.FiveXFive));
        fiveXFive.onClick.AddListener(() => AdjustListener(fiveXFive, fourXFour, threeXThree));
        fiveXFive.onClick.AddListener(() => DisplayLeaderboard());               
    }      

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;

        endless.onClick.Invoke();
        threeXThree.onClick.Invoke();
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }        

    private void DisplayLeaderboard()
    {
        switch(currentMode)
        {
            case Modes.Endless:
                Debug.Log(currentMode);
                switch(currentType)
                {
                    case Types.ThreeXThree:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("E_3X3");                        
                        break;
                    case Types.FourXFour:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("E_4X4");                        
                        break;
                    case Types.FiveXFive:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("E_5X5");                        
                        break;
                }                    
             break;

            case Modes.BeatTheClock:
                Debug.Log(currentMode);
                switch (currentType)
                {
                    case Types.ThreeXThree:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("B_T_C_3X3");                        
                        break;
                    case Types.FourXFour:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("B_T_C_4X4");                        
                        break;
                    case Types.FiveXFive:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("B_T_C_5X5");                        
                        break;
                }
                break;

            case Modes.Delay:
                Debug.Log(currentMode);
                switch (currentType)
                {
                    case Types.ThreeXThree:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("D_3X3");                        
                        break;
                    case Types.FourXFour:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("D_4X4");                        
                        break;
                    case Types.FiveXFive:
                        Debug.Log(currentType);
                        LeaderboardManager.Instance.GetPlayerScore("D_5X5");                        
                        break;
                }
                break;
        }
            
    }

    private void UpdateText()
    {
        score = LeaderboardManager.Instance.playerScore.Score;
        rank = LeaderboardManager.Instance.playerScore.Rank;
        playerID = LeaderboardManager.Instance.playerScore.PlayerName;

        scoreText.text = "Rank: " + rank.ToString() + " Score: " + score.ToString() + " " + playerID;
    }


    private void SetMode(Modes mode)
    {
        currentMode = mode;       
    }

    void SetType(Types type)
    {
        currentType = type;
    }

    void AdjustListener(Button a, Button b, Button c)
    {
        a.interactable = false;
        a.GetComponentInChildren<TMP_Text>().color = Color.green;

        b.interactable = true;
        b.GetComponentInChildren<TMP_Text>().color = Color.black;

        c.interactable = true;
        c.GetComponentInChildren<TMP_Text>().color = Color.black;
    }
}
