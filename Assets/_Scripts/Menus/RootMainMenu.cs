using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;
public class RootMainMenu : BaseMenu
{
    [SerializeField] TMP_Text playerID;

    [SerializeField] Button newGame;
    [SerializeField] Button leaderBoard;
    [SerializeField] Button options;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.RootMainMenu;

       //newGame.onClick.AddListener(() => newGame.GetComponentInChildren<TMP_Text>().color = Color.green);
        //leaderBoard.onClick.AddListener(() => leaderBoard.GetComponentInChildren<TMP_Text>().color = Color.green);
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
        playerID.text = "Player: " + AuthenticationService.Instance.PlayerName;                
    }

    public override void ExitState()
    {
        //newGame.GetComponentInChildren<TMP_Text>().color = new Color(50, 50, 50);
        // leaderBoard.GetComponentInChildren<TMP_Text>().color = new Color(50, 50, 50);

        base.ExitState();
        Time.timeScale = 1.0f;       
    }

    public void JumpToGameModeSelect()
    {
        context.SetActiveMenu(MenuManager.MenuStates.GameModeSelect);
    }

    public void JumpToOptionsMenu()
    {
        context.SetActiveMenu(MenuManager.MenuStates.OptionsMenu);
    }

    public void JumpToLeaderboardMenu()
    {
        context.SetActiveMenu(MenuManager.MenuStates.LeaderboardMenu);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
