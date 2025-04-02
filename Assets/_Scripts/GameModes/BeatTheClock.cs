using UnityEngine;

public class BeatTheClock : BaseGameMode
{
    bool stateActive;
    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.BeatTheClock;
    }

    override public void EnterState()
    {
        stateActive = true;
    }

    override public void ExitState()
    {
        stateActive = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (stateActive)
        {
            if (gameManager.GetStartGame() == true)
            {
                gameManager.currentTime -= Time.deltaTime;

                if (gameManager.currentTime < 0)
                {
                    gameManager.InvokeGameOver();
                }
            }
        }
    }
}
