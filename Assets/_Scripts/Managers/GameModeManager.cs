using System;
using System.Collections.Generic;
using UnityEngine;


public class GameModeManager: Singleton<GameModeManager>
{     
    public BaseGameMode[] gameModesRef;
    public enum GameModes
    {
        None, Endless, BeatTheClock, Delay
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

        //Set none game mode active at game over, this removes all game mode funtionality ands stops any mode from running.
        GameManager.Instance.OnGameOver += () => ActivateGameMode(GameModes.None);
    }

    /// <summary>
    /// Used to set which game mode is active.
    /// </summary>
    /// <param name="newGameMode"></param>
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

    /// <summary>
    /// Set beat the clock mode active
    /// </summary>
    public void SetBeatTheClock()
    {
        ActivateGameMode(GameModes.BeatTheClock);
    }

    /// <summary>
    /// Set Endless mode active
    /// </summary>
    public void SetEndlessMode()
    {
        ActivateGameMode(GameModes.Endless);        
    }

    /// <summary>
    /// Set Delay mode active
    /// </summary>
    public void SetDelayMode()
    {
        ActivateGameMode(GameModes.Delay);
    }

    /// <summary>
    /// Sets the previous game mode active, used when game is restarted instead of returning to main menu
    /// </summary>
    public void ResetGame()
    {
        ActivateGameMode(prevMode.gameMode);
    }

    public bool IsModeSet(GameModes mode)
    {
        if (currentMode == modeDictionary[mode])
            return true;
        else
            return false;
    }

}
