using UnityEngine;

public class RootMainMenu : BaseMenu
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.RootMainMenu;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
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

    //public void JumpToOptionsMenu()
    //{
    //    context.SetActiveMenu(MenuManager.MenuStates.OptionsMenu);
    //}

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


}
