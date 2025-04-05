using UnityEngine;

public class BeatTheClock : BaseGameMode
{
    bool stateActive;
    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.BeatTheClock;
    }

    // runs when game mode has activated
    override public void EnterState()
    {
        stateActive = true;
    }

    // runs when game mode has ended
    override public void ExitState()
    {
        stateActive = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        //check to see if game mode is active
        if (stateActive)
        {
            //Check to see if the game has started
            if (gameManager.GetStartGame() == true)
            {
                //Each frame subtract from current time, decreasing the remaining game time left. 
                gameManager.currentTime -= Time.deltaTime;

                //Once the timer has finished set game over state
                if (gameManager.currentTime < 0)
                {
                    gameManager.InvokeGameOver();
                }
            }
        }
    }
}
