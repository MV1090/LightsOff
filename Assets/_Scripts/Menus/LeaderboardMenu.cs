using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : BaseMenu
{
    [Header("Buttons")]
    [SerializeField] Button endless;
    [SerializeField] Button beatTheClock;
    [SerializeField] Button delay;
    [SerializeField] Button threeXThree;
    [SerializeField] Button fourXFour;
    [SerializeField] Button fiveXFive;
        
    [Header("TopRank")]
    [SerializeField] List <TMP_Text> topRank = new List<TMP_Text>();  

    [Header("TopName")]
    [SerializeField] List<TMP_Text> topName = new List<TMP_Text>();

    [Header("TopScore")]
    [SerializeField] List<TMP_Text> topScore = new List<TMP_Text>();

    [Header("PlayerRank")]
    [SerializeField] List<TMP_Text> playerRank = new List<TMP_Text>();

    [Header("PlayerName")]
    [SerializeField] List<TMP_Text> playerName = new List<TMP_Text>();

    [Header("PlayerScore")]
    [SerializeField] List<TMP_Text> playerScore = new List<TMP_Text>();

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
        //endless.onClick.AddListener(() => DisplayLeaderboard());

        beatTheClock.onClick.AddListener(() => SetMode(Modes.BeatTheClock));
        beatTheClock.onClick.AddListener(() => AdjustListener(beatTheClock, endless, delay));
        //beatTheClock.onClick.AddListener(() => DisplayLeaderboard());

        delay.onClick.AddListener(() => SetMode(Modes.Delay));
        delay.onClick.AddListener(() => AdjustListener(delay, beatTheClock, endless));
        //delay.onClick.AddListener(() => DisplayLeaderboard());

        threeXThree.onClick.AddListener(() => SetType(Types.ThreeXThree));
        threeXThree.onClick.AddListener(() => AdjustListener(threeXThree, fourXFour, fiveXFive));
        //threeXThree.onClick.AddListener(() => DisplayLeaderboard());

        fourXFour.onClick.AddListener(() => SetType(Types.FourXFour));
        fourXFour.onClick.AddListener(() => AdjustListener(fourXFour, threeXThree, fiveXFive));
        //fourXFour.onClick.AddListener(() => DisplayLeaderboard());

        fiveXFive.onClick.AddListener(() => SetType(Types.FiveXFive));
        fiveXFive.onClick.AddListener(() => AdjustListener(fiveXFive, fourXFour, threeXThree));
        //fiveXFive.onClick.AddListener(() => DisplayLeaderboard());               
    }      

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
        backButton.gameObject.SetActive(true);
        StartCoroutine(DelayedLeaderboardInit());
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }

    private void DisplayLeaderboard()
    {
        string leaderboardId = GetLeaderboardId(currentMode, currentType);

        if (!string.IsNullOrEmpty(leaderboardId))
        {
           _= LeaderboardManager.Instance.SubmitAndFetchLeaderboardData(leaderboardId);
        }
        else
        {
            Debug.LogWarning("Invalid leaderboard ID for selected mode/type");
        }
    }

    private string GetLeaderboardId(Modes mode, Types type)
    {
        return (mode, type) switch
        {
            (Modes.Endless, Types.ThreeXThree) => "E_3X3",
            (Modes.Endless, Types.FourXFour) => "E_4X4",
            (Modes.Endless, Types.FiveXFive) => "E_5X5",
            (Modes.BeatTheClock, Types.ThreeXThree) => "B_T_C_3X3",
            (Modes.BeatTheClock, Types.FourXFour) => "B_T_C_4X4",
            (Modes.BeatTheClock, Types.FiveXFive) => "B_T_C_5X5",
            (Modes.Delay, Types.ThreeXThree) => "D_3X3",
            (Modes.Delay, Types.FourXFour) => "D_4X4",
            (Modes.Delay, Types.FiveXFive) => "D_5X5",
            _ => null
        };
    }
    private void UpdateText()
    {
        TopScores();
        PlayerScores();
    }

    private void TopScores()
    {
        if (LeaderboardManager.Instance.topFiveScores == null)
            return;

        for (int i = 0; i < topRank.Count; i++)
        {
            if (i > LeaderboardManager.Instance.topFiveScores.Results.Count - 1)
            {
                topRank[i].text = " ";
                continue;
            }
            int rank = LeaderboardManager.Instance.topFiveScores.Results[i].Rank + 1;

            topRank[i].text = rank.ToString();
        }

        string localPlayerId = AuthenticationService.Instance.PlayerId;

        for (int i = 0; i < playerName.Count; i++)
        {
            if (i > LeaderboardManager.Instance.topFiveScores.Results.Count - 1)
            {
                topName[i].text = " ";
                continue;
            }

            var entry = LeaderboardManager.Instance.topFiveScores.Results[i];
            bool isLocalPlayer = entry.PlayerId == localPlayerId;

            string playerId = entry.PlayerId;

            // Highlight or mark the local player visually
            if (isLocalPlayer)
            {
                topName[i].text = $"<b><color=green>{entry.PlayerName}</color></b>";
            }
            else
            {
                topName[i].text = entry.PlayerName;
            }
        }

        for (int i = 0; i < topScore.Count; i++)
        {
            if (i > LeaderboardManager.Instance.topFiveScores.Results.Count - 1)
            {
                topScore[i].text = " ";
                continue;
            }

            topScore[i].text = LeaderboardManager.Instance.topFiveScores.Results[i].Score.ToString();
        }
    }
    private void PlayerScores()
    {
        if (LeaderboardManager.Instance.scoresAroundPlayer == null)
            return;

        for (int i = 0; i < playerRank.Count; i++)
        {
            if (i > LeaderboardManager.Instance.scoresAroundPlayer.Results.Count - 1)
            {
                playerRank[i].text = " ";
                continue;
            }
            int rank = LeaderboardManager.Instance.scoresAroundPlayer.Results[i].Rank + 1;

            playerRank[i].text = rank.ToString();
        }

        string localPlayerId = AuthenticationService.Instance.PlayerId;

        for (int i = 0; i < playerName.Count; i++)
        {
            if (i > LeaderboardManager.Instance.scoresAroundPlayer.Results.Count - 1)
            {
                playerName[i].text = " ";
                continue;
            }

            var entry = LeaderboardManager.Instance.scoresAroundPlayer.Results[i];
            bool isLocalPlayer = entry.PlayerId == localPlayerId;

            string playerId = entry.PlayerId;

            // Highlight or mark the local player visually
            if (isLocalPlayer)
            {
                playerName[i].text = $"<b><color=green>{entry.PlayerName}</color></b>";
            }
            else
            {                
                playerName[i].text = entry.PlayerName;
            }
        }

        for (int i = 0; i < playerScore.Count; i++)
        {
            if (i > LeaderboardManager.Instance.scoresAroundPlayer.Results.Count - 1)
            {
                playerScore[i].text = " ";
                continue;
            }

            playerScore[i].text = LeaderboardManager.Instance.scoresAroundPlayer.Results[i].Score.ToString();
        }
    }

    private void SetMode(Modes mode)
    {
        currentMode = mode;
        DisplayLeaderboard();
    }

    void SetType(Types type)
    {
        currentType = type;
        DisplayLeaderboard();
    }

    void AdjustListener(Button a, Button b, Button c)
    {
        a.interactable = false;
        a.GetComponentInChildren<TMP_Text>().color = Color.green;

        b.interactable = true;
        b.GetComponentInChildren<TMP_Text>().color = Color.white;

        c.interactable = true;
        c.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    private IEnumerator DelayedLeaderboardInit()
    {
        yield return new WaitForEndOfFrame();

        currentMode = Modes.Endless;
        currentType = Types.ThreeXThree;

        endless.onClick.Invoke();
        threeXThree.onClick.Invoke();
        DisplayLeaderboard();
    }
}
