using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MemoryMode : BaseGameMode
{
    [SerializeField] List<LightObject> sequenceList = new List<LightObject>();
    [SerializeField] List<LightObject> playerInputList = new List<LightObject>();

    [SerializeField] private int sequenceIndex = 5;

    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.Memory;
    }

    //runs when game mode is activated
    public override void EnterState()
    {
        base.EnterState();

        foreach (LightObject lightObject in LightManager.Instance.allLightObjects)
        {
            lightObject.OnGameStart += MemoryTurn;
            //lightObject.OnLightTouched += ;
            GameManager.Instance.OnGameOver += ResetMode;
        }
    }

    public override void ExitState()
    {
        base.ExitState();        
    }

    private void PlayerTurn()
    {

    }

    private void MemoryTurn() 
    {
        foreach(LightObject lightObject in LightManager.Instance.playableLightObjects)
            lightObject.SetLightActive(false);

        StartCoroutine(LightSequence());
    }

    private void ResetMode()
    {
        sequenceIndex = 0;
        sequenceList.Clear();
        playerInputList.Clear();
    }

    private IEnumerator LightSequence()
    {
        Debug.Log("ModeStarted");
        yield return new WaitForSeconds(1);

        for (int i = -1; i < sequenceIndex; i++)
        {            
            LightManager.Instance.ActivateNewLight();
            sequenceList.Add(LightManager.Instance.playableLightObjects[LightManager.Instance.currentLight]);
            yield return new WaitForSeconds(1);
            LightManager.Instance.playableLightObjects[LightManager.Instance.currentLight].SetLightActive(false);            
        }

        sequenceIndex++;
    }
}
