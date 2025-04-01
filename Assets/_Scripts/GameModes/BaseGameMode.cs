using UnityEngine;

public class BaseGameMode : MonoBehaviour
{    
    protected GameModeManager gameModeManager;
    protected GameManager gameManager;


    public virtual void InitState(GameModeManager ctx)
    {
        gameModeManager = ctx;
        gameManager = GameManager.Instance.GetComponent<GameManager>();
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
