using System.Collections.Generic;
using UnityEngine;


public class GameModeManager : Singleton<GameModeManager>
{
    public BaseGameMode[] gameModesRef;
    public enum GameModes
    {
        None, Endless, BeatTheClock, Delay
    }

    public Dictionary<GameModes, BaseGameMode> modeDictionary = new Dictionary<GameModes, BaseGameMode>();
    [SerializeField] private BaseGameMode currentBaseMode;
    [SerializeField] private GameModes currentGameMode;
    [SerializeField] private GameModes prevGameMode;
    private BaseGameMode prevBaseMode;

    private void Start()
    {
        foreach (BaseGameMode gameMode in gameModesRef)
        {
            gameMode.InitState(this);

            if (modeDictionary.ContainsKey(gameMode.gameMode))
                continue;

            modeDictionary.Add(gameMode.gameMode, gameMode);
        }

        foreach (GameModes gameMode in modeDictionary.Keys)
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

        if (currentBaseMode != null)
        {
            prevBaseMode = currentBaseMode;
            currentBaseMode.ExitState();
            currentBaseMode.gameObject.SetActive(false);
        }

        currentBaseMode = modeDictionary[newGameMode];
        currentBaseMode.gameObject.SetActive(true);
        currentBaseMode.EnterState();


        prevGameMode = currentGameMode;
        currentGameMode = newGameMode;
    }

    public GameModes GetPrevMode()
    {
        return prevGameMode;
    }

    public BaseGameMode GetBaseGameMode()
    {
        return currentBaseMode;
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
        ActivateGameMode(prevBaseMode.gameMode);
    }

    public GameModes GetCurrentGameMode()
    {
        return currentGameMode;
    }        
        
    public bool IsModeSet(GameModes mode)
    {
        if (currentBaseMode == modeDictionary[mode])
            return true;
        else
            return false;
    }

}
