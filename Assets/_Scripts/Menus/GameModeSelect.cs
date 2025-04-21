using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameModeSelect : BaseMenu
{

    [SerializeField] Button endless;
    [SerializeField] Button delay;
    [SerializeField] Button beatTheClock;
    [SerializeField] Button back;

    Button activeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.GameModeSelect;

        endless.onClick.AddListener(() => ChangeColour(endless));
        delay.onClick.AddListener(() => ChangeColour(delay));
        beatTheClock.onClick.AddListener(() => ChangeColour(beatTheClock));
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

        ResetColour(activeButton);        

    }

    void ChangeColour(Button button)
    {
        button.GetComponentInChildren<TMP_Text>().color = new Color32(27, 192, 33, 255);
        activeButton = button;
    }

    void ResetColour(Button button)
    {
        button.GetComponentInChildren<TMP_Text>().color = new Color32(50, 50, 50, 255);
    }

    public void JumpToGameMenu()
    {
        MenuManager.Instance.menuToActivate = MenuManager.MenuStates.GameMenu;
        //context.SetActiveMenu(MenuManager.MenuStates.GameMenu);
    }
}
