using System;
using System.Collections.Generic;
using UnityEngine;


public class GameModeManager: Singleton<GameModeManager>
{
    //public event Action EndlessModeSet;    

    public BaseGameMode[] gameModesRef;
    public enum GameModes
    {
        None, Endless, BeatTheClock
    }

    public Dictionary<GameModes, BaseGameMode> modeDictionary = new Dictionary<GameModes, BaseGameMode>();
    [SerializeField]private BaseGameMode currentMode;
    private BaseGameMode prevMode;    

    private void Start()
    {
        foreach(BaseGameMode gameMode in gameModesRef)
        {
            gameMode.InitState(this);

            if (modeDictionary.ContainsKey(gameMode.gameMode))
                continue;

            modeDictionary.Add(gameMode.gameMode, gameMode);
        }

        foreach(GameModes gameMode in modeDictionary.Keys)
        {
            modeDictionary[gameMode].gameObject.SetActive(false);
        }

        ActivateGameMode(GameModes.None);

        GameManager.Instance.OnGameOver += () => ActivateGameMode(GameModes.None);
    }

    public void ActivateGameMode(GameModes newGameMode)
    {
        if (!modeDictionary.ContainsKey(newGameMode))
            return;
       
        if(currentMode != null)
        {
            prevMode = currentMode;
            currentMode.ExitState();
            currentMode.gameObject.SetActive(false);
        }

        currentMode = modeDictionary[newGameMode];
        currentMode.gameObject.SetActive(true);
        currentMode.EnterState();        
    }

    public BaseGameMode GetGameMode()
    {
        return currentMode;
    }

    public void SetBeatTheClock()
    {
        ActivateGameMode(GameModes.BeatTheClock);
    }

    public void SetEndlessMode()
    {
        ActivateGameMode(GameModes.Endless);        
    }

    public void ResetGame()
    {
        ActivateGameMode(prevMode.gameMode);
    }

}
