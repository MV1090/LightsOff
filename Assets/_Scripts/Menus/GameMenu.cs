using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameMenu : BaseMenu
{
    [Header("Score")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gameText;
    [SerializeField] Slider timerSlider;

    [SerializeField] Gradient colorGradient;
    [SerializeField] Slider bottomStrip;

    [SerializeField] EndLess EndlessTime;
    [SerializeField] Delay DelayTime;

    private float startTime;
    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.GameMenu;

        if (scoreText)
        {
            GameManager.Instance.OnScoreValueChange.AddListener(UpdateScoreText);
            scoreText.text = GameManager.Instance.Score.ToString();
        }

        foreach (LightObject light in LightManager.Instance.allLightObjects)
        {
            light.OnGameStart += () => backButton.gameObject.SetActive(false); 
        }

        GameManager.Instance.OnGameOver += () => UpdateTimerBar(-1.0f);
    }

    public override void EnterState()
    {        
        base.EnterState();
        
        GameTypeManager.Instance.SetGridActive();

        Time.timeScale = 1.0f;    

        if (GameModeManager.Instance.GetCurrentGameMode() == GameModeManager.GameModes.Endless)
        {
            gameText.text = "ENDLESS";
            startTime = 1;
            timerSlider.maxValue = startTime;
        }

        else if (GameModeManager.Instance.GetCurrentGameMode() == GameModeManager.GameModes.Delay)
        {
            gameText.text = "Delay";
            startTime = 1;
            timerSlider.maxValue = startTime;
        }

        else
        {
            startTime = GameManager.Instance.GetCurrentTime();
            timerSlider.maxValue = startTime;
            gameText.text = "COUNTDOWN";
        }
        UpdateTimerBar(startTime);
        AdManager.Instance.HideBannerAD();
    }

    public override void ExitState()
    {
        base.ExitState();        
        LightManager.Instance.ResetPlayableLights();
        Time.timeScale = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GetStartGame())
            return;

        if (GameModeManager.Instance.GetCurrentGameMode() == GameModeManager.GameModes.BeatTheClock)
        {
            UpdateTimerBar(GameManager.Instance.GetCurrentTime());
            return;
        }

        if (GameModeManager.Instance.GetCurrentGameMode() == GameModeManager.GameModes.Endless)
        {
            UpdateTimerBar(EndlessTime.currentTime);
            return;
        }

        if (GameModeManager.Instance.GetCurrentGameMode() == GameModeManager.GameModes.Delay)
        {
            UpdateTimerBar(DelayTime.currentTime);
            return;
        }
    }

    void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    void UpdateTimerBar(float value)
    {
        Color newColor = colorGradient.Evaluate(value/startTime);
        timerSlider.image.color = newColor;
        timerSlider.value = value;        
    }
}
