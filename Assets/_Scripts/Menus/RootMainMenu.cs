using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;
public class RootMainMenu : BaseMenu
{
    [SerializeField] TMP_Text playerID;
    PlayerInfo playerInfo;
    [SerializeField] Button newGame;
    [SerializeField] Button leaderBoard;
    [SerializeField] Button options;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.RootMainMenu;
        Authentication.OnPlayerNameReady += HandlePlayerNameReady;

        // Optional: if name is already ready (e.g. player returned to menu)
        if (Authentication.Instance != null && !string.IsNullOrEmpty(Authentication.Instance.PlayerDisplayName))
        {
            HandlePlayerNameReady(Authentication.Instance.PlayerDisplayName);
        }
    }
    void HandlePlayerNameReady(string playerName)
    {
        //Debug.Log("Got player name: " + playerName);
        playerID.text = "Player:\n " + playerName;
    }
    public override void EnterState()
    {
        base.EnterState();
        backButton.gameObject.SetActive(false);
        Time.timeScale = 0.0f;        
        //AdManager.Instance.ShowBannerAD();     
    }

    public override void ExitState()
    {
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
