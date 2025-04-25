using UnityEngine;

public class Shutter : MonoBehaviour
{

    private Animator anim;
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
}
