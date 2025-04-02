/// <summary>
/// This class is used as an empty contain for when no game mode is active
/// </summary>
public class None : BaseGameMode
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.None;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();        
    }
}
