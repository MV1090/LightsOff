using System.Collections.Generic;
using UnityEngine;

public class GameModeManager: Singleton<GameModeManager>
{
    public BaseGameMode[] gameModesRef;
    public enum GameModes
    {
        None, Endless, BeatTheClock
    }

    public GameModes currentMode;
    

    private void Start()
    {
        foreach(BaseGameMode gameMode in gameModesRef)
        {
            gameMode.InitState(this);
        }

        SetGameMode(GameModes.None);

        GameManager.Instance.OnGameOver += () => SetGameMode(GameModes.None);
    }

    public void SetGameMode(GameModes gameMode)
    {
        currentMode = gameMode; 
    }

    public GameModes GetGameMode()
    {
        return currentMode;
    }

    public void SetBeatTheClock()
    {
        SetGameMode(GameModes.BeatTheClock);
    }
}
