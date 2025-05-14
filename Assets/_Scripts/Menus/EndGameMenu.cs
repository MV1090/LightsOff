using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : BaseMenu
{
    [Header ("Score Text")]
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text bestScore;

    [Header ("Buttons")]
    [SerializeField] Button mainMenu;
    [SerializeField] Button replay;    

    public bool previousMenu;

    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.EndGameMenu;
        //LeaderboardManager.Instance.scoreEntered += DisplayScore;
        mainMenu.onClick.AddListener(() => DisableButtons());
        replay.onClick.AddListener(() => DisableButtons());
    }

    public override void EnterState()
    {        
        base.EnterState();

        DisplayScore();
        Time.timeScale = 0.0f;
        EnableButtons();        
        GameManager.Instance.ResetGame();
        DisableButtons();

        previousMenu = true;

        backButton.gameObject.SetActive(false);
        
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;        
    }
       
    public void JumpToMainMenu()
    {
        context.SetActiveMenu(MenuManager.MenuStates.RootMainMenu);
        previousMenu = false;
        Debug.Log("Jump to main menu");
    }

    public void JumpToGameMenu()
    {
        context.menuToActivate = MenuManager.MenuStates.GameMenu;
    }

    private void DisplayScore()
    {
        score.text = $"Score: {GameManager.Instance.Score.ToString()}";
        DisplayBestScore();
    }

    private void DisplayBestScore()
    {
        var mode = GameModeManager.Instance.GetPrevMode();
        var type = GameTypeManager.Instance.GetGameType();
        Debug.Log($"Mode: {mode} + Type: {type}");
        string leaderboardId = GetLeaderboardId();
        Debug.Log($"Leaderboard: " + leaderboardId);

        if (ScoreCacheManager.Instance.scoreCache.TryGetValue(leaderboardId, out int highScore))
        {
            if (GameManager.Instance.Score > highScore)
                bestScore.text = "NEW HIGH SCORE!";
            else
                bestScore.text = $"High score: {highScore}";
        }
        else
        {
            Debug.LogWarning("Leaderboard ID not found in scoreCache!");
            bestScore.text = "No high score yet.";
        }

        ScoreCacheManager.Instance.UpDateScoreCache(leaderboardId);
    }    

    void DisableButtons()
    {
        mainMenu.interactable = false;
        replay.interactable = false;
    }
    public void EnableButtons()
    {
        mainMenu.interactable = true;
        replay.interactable = true;

        //previousMenu = false;
    }
    private string GetLeaderboardId()
    {
        var mode = GameModeManager.Instance.GetPrevMode();
        var type = GameTypeManager.Instance.GetGameType();

        if (LeaderboardManager.Instance.distractionActive)
        {
            return (mode, type) switch
            {
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.ThreeXThree) => "D_D_3X3",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FourXFour) => "D_D_4X4",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FiveXFive) => "D_D_5X5",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.ThreeXThree) => "C_D_3X3",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FourXFour) => "C_D_4X4",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FiveXFive) => "C_D_5X5",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.ThreeXThree) => "E_D_3X3",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FourXFour) => "E_D_4X4",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FiveXFive) => "E_D_5X5",
                _ => null
            };
        }
        else
        {
            return (mode, type) switch
            {
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.ThreeXThree) => "D_3X3",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FourXFour) => "D_4X4",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FiveXFive) => "D_5X5",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.ThreeXThree) => "B_T_C_3X3",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FourXFour) => "B_T_C_4X4",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FiveXFive) => "B_T_C_5X5",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.ThreeXThree) => "E_3X3",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FourXFour) => "E_4X4",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FiveXFive) => "E_5X5",
                _ => null
            };
        }
    }

}
