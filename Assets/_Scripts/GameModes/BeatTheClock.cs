using UnityEngine;

public class BeatTheClock : BaseGameMode
{
    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
       // if (gameModeManager.currentMode == GameModeManager.GameModes.BeatTheClock)
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
