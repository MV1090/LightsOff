using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : BaseMenu

{

    [SerializeField] Button music;
    [SerializeField] Button soundfx;
    [SerializeField] Button accessibility;
    [SerializeField] Button back;

    Button activeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.OptionsMenu;
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

    public void MusicToggle()
    {
        Debug.Log("Music Toggle On/Off");
    }

    public void SoundFxToggle()
    {
        Debug.Log("Sound FX Toggle On/Off");
    }

    public void SelectLightColours()
    {
        Debug.Log("Choose light colours here");
    }

}
