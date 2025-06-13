using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : BaseMenu

{
    
    [SerializeField] Toggle music;
    [SerializeField] Toggle soundfx;
    [SerializeField] Toggle accessibility;
    [SerializeField] Slider activeLight;
    [SerializeField] Slider warningLight;
    [SerializeField] Image light1;
    [SerializeField] Image light2;    

    public AudioMixer audioMixer;
    public Gradient colorGradient;

    Button activeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.OptionsMenu;

        activeLight.onValueChanged.AddListener(UpdateActiveColor);
        warningLight.onValueChanged.AddListener(UpdateWarningColor);

        soundfx.onValueChanged.AddListener(SoundFxToggle);
        music.onValueChanged.AddListener(MusicToggle);

        activeLight.value = 0.5f;
        warningLight.value = 0.01f;
    }

    public override void EnterState()
    {
        base.EnterState();
        backButton.gameObject.SetActive(true);
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

    public void MusicToggle(bool isOn)
    {
        if (!music.isOn)
            audioMixer.SetFloat("musicVolume", -80.0f);
        else
            audioMixer.SetFloat("musicVolume", 0.0f);
        //Debug.Log("Music Toggle On/Off");
    }

    void SoundFxToggle(bool isOn)
    {
        if(!isOn)
            audioMixer.SetFloat("SFXVolume", -88.0f);
        else
            audioMixer.SetFloat("SFXVolume", 0.0f);

        //Debug.Log("Sound FX Toggle On/Off");
    }

    public void SelectLightColours()
    {
        //Debug.Log("Choose light colours here");
    }

    public void JumpToTutorialMenu()
    {
        context.SetActiveMenu(MenuManager.MenuStates.TutorialMenu);
    }
}
