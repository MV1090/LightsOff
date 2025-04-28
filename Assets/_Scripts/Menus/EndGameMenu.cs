using TMPro;
using UnityEngine;

public class EndGameMenu : BaseMenu
{
    [Header ("Score Text")]
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text bestScore;

    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.EndGameMenu;
    }

    public override void EnterState()
    {        
        base.EnterState();
        Time.timeScale = 0.0f;

        DisplayScore();
        DisplayBestScore();
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }
       
    public void JumpToMainMenu()
    {
        context.SetActiveMenu(MenuManager.MenuStates.RootMainMenu);
        Debug.Log("Jump to main menu");
    }

    public void JumpToGameMenu()
    {
        context.menuToActivate = MenuManager.MenuStates.GameMenu;
    }

    private void DisplayScore()
    {
        score.text = $"Score: {GameManager.Instance.Score.ToString()}";
    }

    private void DisplayBestScore()
    {
        if (LeaderboardManager.Instance.playerScore == null || GameManager.Instance.Score > LeaderboardManager.Instance.playerScore.Score)
        {
            bestScore.text = "NEW BEST SCORE";
            return;
        }

        bestScore.text = $"Best score: {LeaderboardManager.Instance.playerScore.Score.ToString()}";
    }
    
}
