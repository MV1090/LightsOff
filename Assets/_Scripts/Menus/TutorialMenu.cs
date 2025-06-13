using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : BaseMenu
{
    int tutorialTracker;
    [SerializeField]string[] tutorialDescription;
    [SerializeField] Image lightsImage;
    [SerializeField] Image greenPointer;
    [SerializeField] Image redPointer;
    [SerializeField] Image barPointer;
    [SerializeField] Slider timer;
    [SerializeField] Gradient colorGradient;
    [SerializeField] Image playPointer;
    [SerializeField] TMP_Text playGameText;
 

    [Multiline][SerializeField] TMP_Text descriptionText;    

    static public TutorialMenu instance;

    private float timerBar;

    public override void InitState(MenuManager ctx)
    {
        base.InitState(ctx);
        state = MenuManager.MenuStates.TutorialMenu;
        playGameText.gameObject.SetActive(false);
        playPointer.gameObject.SetActive(false);
        instance = this;
        
    }
    public override void EnterState()
    {
        base.EnterState();
        tutorialTracker = 0;
        timerBar = 15;
        PlayTutorial();
    }
    public override void ExitState()
    {
        base.ExitState();
        barPointer.gameObject.SetActive(false);
    } 

    public void PlayTutorial()
    {     
        switch (tutorialTracker)
        {
            case 0:
                PhaseOne();
                break;

            case 1:
                PhaseTwo();
                break;

            case 2:
                PhaseThree();
                break;

            case 3: 
                PhaseFour();
                break;
        }

        tutorialTracker++;
    }

    private void PhaseOne()
    {
        lightsImage.gameObject.SetActive(true);
        greenPointer.gameObject.SetActive(true);
        redPointer.gameObject.SetActive(true);
        barPointer.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        descriptionText.text = tutorialDescription[tutorialTracker];
        descriptionText.rectTransform.anchorMin = new Vector2(0.13f, 0.15f);
        descriptionText.rectTransform.anchorMax = new Vector2(0.87f, 0.22f);        
    }

    private void PhaseTwo()
    {
        lightsImage.gameObject.SetActive(false);
        greenPointer.gameObject.SetActive(false);
        redPointer.gameObject.SetActive(false);
        barPointer.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        descriptionText.text = tutorialDescription[tutorialTracker];
        descriptionText.rectTransform.anchorMin = new Vector2(0.13f, 0.3f);
        descriptionText.rectTransform.anchorMax = new Vector2(0.87f, 0.5f);        
    }
    private void PhaseThree() 
    {        
        barPointer.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        descriptionText.text = tutorialDescription[tutorialTracker];
        descriptionText.rectTransform.anchorMin = new Vector2(0.13f, 0.2f);
        descriptionText.rectTransform.anchorMax = new Vector2(0.87f, 0.8f);
        
        if (GameManager.Instance.GetFirstPlay() == true)
        {
            playGameText.gameObject.SetActive(true);
            playPointer.gameObject.SetActive(true);
            descriptionText.rectTransform.anchorMin = new Vector2(0.13f, 0.35f);
            descriptionText.rectTransform.anchorMax = new Vector2(0.87f, 0.8f);
        }
    }

    private void PhaseFour() 
    {
        if (GameManager.Instance.GetFirstPlay() == false)
        {
            tutorialTracker = 0;
            timerBar = 15;
            PhaseOne();
            return;
        }
        else
        {
            context.SetActiveMenu(MenuManager.MenuStates.GameModeSelect);
            playGameText.gameObject.SetActive(false);
            playPointer.gameObject.SetActive(false);
        }                
    }

    private void Update()
    {       
        if (timer.gameObject.activeInHierarchy)
        {
            timerBar -= Time.deltaTime;
            UpdateTimerBar(timerBar);
            if (timerBar < 0)
            {
                timerBar = 15;
            }
        }
    }

    void UpdateTimerBar(float value)
    {
        Color newColor = colorGradient.Evaluate(value / timer.maxValue);
        timer.image.color = newColor;
        timer.value = value;
    }
}
