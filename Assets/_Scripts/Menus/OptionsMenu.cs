using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : BaseMenu

{

    [SerializeField] Button music;
    [SerializeField] Button soundfx;
    [SerializeField] Button accessibility;
    [SerializeField] Slider activeLight;
    [SerializeField] Slider warningLight;
    [SerializeField] Image light1;
    [SerializeField] Image light2;


    public Gradient colorGradient;

    Button activeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.OptionsMenu;

        activeLight.onValueChanged.AddListener(UpdateActiveColor);
        warningLight.onValueChanged.AddListener(UpdateWarningColor);

        activeLight.value = 0.5f;
        warningLight.value = 0.01f;
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
    void UpdateActiveColor(float value)
    {     
        Color newColor = colorGradient.Evaluate(value);

        light1.color = newColor;

        foreach (LightObject light in LightManager.Instance.allLightObjects)
        {
            light.activeColour = newColor;
        }       
        
        light1.color = newColor;
    }

    void UpdateWarningColor(float value)
    {
        Color newColor = colorGradient.Evaluate(value);

        light2.color = newColor;

        foreach (LightObject light in LightManager.Instance.allLightObjects)
        {
            light.warningColour = newColor;
        }       
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
