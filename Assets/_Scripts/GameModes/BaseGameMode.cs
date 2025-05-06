using UnityEngine;

public class BaseGameMode : MonoBehaviour
{
    public GameModeManager.GameModes gameMode;
    protected GameModeManager gameModeManager;
    protected GameManager gameManager;   

    public virtual void InitState(GameModeManager ctx)
    {
        gameModeManager = ctx;
        gameManager = GameManager.Instance.GetComponent<GameManager>();
        
    }

    public virtual void EnterState()
    {
       
    }

    public virtual void ExitState()
    {
        
    }
      

    public virtual void OnEnable()
    {

    }
    public virtual void OnDisable() 
    {

    }

    public virtual void Update()
    {

    }

}
