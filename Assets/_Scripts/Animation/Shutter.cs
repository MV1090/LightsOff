using UnityEngine;
using UnityEngine.UI;

public class Shutter : MonoBehaviour
{

    private Animator anim;

    [SerializeField] EndGameMenu endGameMenu;
    public Button backButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        LightManager.Instance.gameEnded += () => PlayAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimStart()
    {
        PlayShutterSound();
        DeactivateButton();
    }

    public void OnAnimEnd()
    {        
        MenuLoaded();
    }


    public void PlayShutterSound()
    {
        SFXManager.Instance.PlayShutterSound();
    }

    public void PlayAnimation()
    {
        anim.SetTrigger("play");
    }

    public void loadNextMenu()
    {
        MenuManager.Instance.SetActiveMenu(MenuManager.Instance.menuToActivate);
        Debug.Log("Menu Loaded");
    }

    public void ShowAD()
    {
        //if(MenuManager.Instance.menuToActivate == MenuManager.MenuStates.EndGameMenu)
        //{
        //    AdManager.Instance.LoadNonRewardedAd();
        //    AdManager.Instance.ShowNonRewardedAd();
        //    Debug.Log("ShowAds");
        //}       
    }
    public void MenuLoaded()
    {
        if (MenuManager.Instance.menuToActivate != MenuManager.MenuStates.EndGameMenu)
        {
            MenuManager.Instance.menuLoaded = true;
        }
        
    }

    public void DeactivateButton()
    {
        if(!backButton.IsActive())
            return;

        backButton.gameObject.SetActive(false);
    }

    public void ActivateButtons()
    {        
        if (endGameMenu.previousMenu == false)
        {
            backButton.gameObject.SetActive(true);
        }
        endGameMenu.EnableButtons();
    }
}
