using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameModeSelect : BaseMenu
{

    [SerializeField] Button endless;
    [SerializeField] Button delay;
    [SerializeField] Button beatTheClock;
    //[SerializeField] Button back;

    [SerializeField] Toggle distraction;

    Button activeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.GameModeSelect;

        endless.onClick.AddListener(() => DisableButtons());
        delay.onClick.AddListener(() => DisableButtons());
        beatTheClock.onClick.AddListener(() => DisableButtons());

        distraction.onValueChanged.AddListener(LightManager.Instance.ToggleDistraction);
    }

    public override void EnterState()
    {
        base.EnterState();
        EnableButtons();
        backButton.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
       
        LightManager.Instance.ToggleDistraction(distraction.isOn);                
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;             
    }


    void DisableButtons()
    {
        endless.interactable = false;
        delay.interactable = false;
        beatTheClock.interactable = false;        
    }

    void EnableButtons()
    {     
        endless.interactable = true;
        delay.interactable = true;
        beatTheClock.interactable = true;        
    }

    void ChangeColour(Button button)
    {
        button.GetComponentInChildren<TMP_Text>().color = new Color32(27, 192, 33, 255);
        activeButton = button;
    }

    void ResetColour(Button button)
    {
        if(activeButton != null) 
        button.GetComponentInChildren<TMP_Text>().color = new Color32(50, 50, 50, 255);
    }

    public void JumpToGameMenu()
    {
        MenuManager.Instance.menuToActivate = MenuManager.MenuStates.GameMenu;
    }
}
