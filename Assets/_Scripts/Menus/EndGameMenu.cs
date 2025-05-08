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
        LeaderboardManager.Instance.ScoreToDisplay += DisplayScore;
        mainMenu.onClick.AddListener(() => DisableButtons());
        replay.onClick.AddListener(() => DisableButtons());
    }

    public override void EnterState()
    {        
        base.EnterState();
        Time.timeScale = 0.0f;
        EnableButtons();
        //DisplayScore();
        //DisplayBestScore();
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
        if (LeaderboardManager.Instance.playerScore == null || GameManager.Instance.Score > LeaderboardManager.Instance.playerScore.Score)
        {
            bestScore.text = "NEW BEST SCORE!";
            return;
        }
        //Debug.Log("Score: " + LeaderboardManager.Instance.playerScore.Score.ToString() + "Game Score: " + GameManager.Instance.Score);
        bestScore.text = $"Best score: {LeaderboardManager.Instance.playerScore.Score.ToString()}";
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


}
