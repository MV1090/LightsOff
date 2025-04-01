using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class MenuManager : Singleton<MenuManager>
{
    public BaseMenu[] allMenus;
    public enum MenuStates
    {
        MainMenu, EndGameMenu, GameMenu
    }

    public BaseMenu currentMenu;
    public Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (allMenus == null)
            return;

        foreach (BaseMenu menu in allMenus)
        {
            if(menu == null) continue;

            menu.InitState(this);

            if(menuDictionary.ContainsKey(menu.state))
                continue;

            menuDictionary.Add(menu.state, menu);
        }

        foreach(MenuStates state in menuDictionary.Keys)
        {
            menuDictionary[state].gameObject.SetActive(false);
        }

        SetActiveMenu(MenuStates.MainMenu);

        GameManager.Instance.OnGameOver += () => SetActiveMenu(MenuStates.EndGameMenu);
    }

    public void SetActiveMenu(MenuStates newMenu, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newMenu))
            return;

        if(currentMenu != null)
        {
            currentMenu.ExitState();
            currentMenu.gameObject.SetActive(false);
        }

        currentMenu = menuDictionary[newMenu];
        currentMenu.gameObject.SetActive(true);
        currentMenu.EnterState();

        if(!isJumpingBack)
        {
            menuStack.Push(newMenu);
        }

    }

    public void JumpBack()
    {
        if (menuStack.Count >= 1)
        {
            SetActiveMenu(MenuStates.MainMenu);
        }
        else
        {
            menuStack.Pop();
            SetActiveMenu(menuStack.Peek(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
