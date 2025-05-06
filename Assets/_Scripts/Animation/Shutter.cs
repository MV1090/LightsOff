using UnityEngine;

public class Shutter : MonoBehaviour
{

    private Animator anim;

    [SerializeField] EndGameMenu endGameMenu;
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
        if(MenuManager.Instance.menuToActivate == MenuManager.MenuStates.EndGameMenu)
        {
            AdManager.Instance.LoadNonRewardedAd();
            AdManager.Instance.ShowNonRewardedAd();
            Debug.Log("ShowAds");
        }       
    }
    public void MenuLoaded()
    {
        if (MenuManager.Instance.menuToActivate != MenuManager.MenuStates.EndGameMenu)
        {
            MenuManager.Instance.menuLoaded = true;
        }
        
    }

    public void ActivateButtons()
    {
        endGameMenu.EnableButtons();
    }
}
