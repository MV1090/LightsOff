using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : BaseMenu
{
    [Header("Score")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Slider timerSlider;

    [SerializeField] Gradient colorGradient;

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
    }

    public override void EnterState()
    {        
        base.EnterState();

        GameTypeManager.Instance.SetGridActive();        
        GameManager.Instance.ResetGame();
        Time.timeScale = 1.0f;

        if (GameModeManager.Instance.GetCurrentGameMode() != GameModeManager.GameModes.BeatTheClock)
            timerSlider.gameObject.SetActive(false);
        else
        {
            startTime = GameManager.Instance.GetCurrentTime();
            timerSlider.maxValue = startTime;
            timerSlider.gameObject.SetActive(true);
        }
    }

    public override void ExitState()
    {
        base.ExitState();      

        LightManager.Instance.ResetPlayableLights();
        Time.timeScale = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!timerSlider.IsActive())
            return;

        UpdateTimerBar(GameManager.Instance.GetCurrentTime());
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
