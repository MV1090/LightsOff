using System.Collections;
using UnityEngine;


public class Delay : BaseGameMode
{
    [SerializeField] float lightOnTime;
    [SerializeField] float startLength;
    [SerializeField] float endLength;
    [SerializeField] float duration;
    [SerializeField] public  float currentTime;
    [SerializeField] float shortDelay;
    [SerializeField] float longDelay;

    private Coroutine lifeSpanCoroutine;

    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.Delay;
    }

    //runs when game mode is activated
    public override void EnterState()
    {
        base.EnterState();

        //Add listeners to all light objects
        foreach (LightObject lightObject in LightManager.Instance.allLightObjects)
        {
            lightObject.OnGameStart += GameSpeed;
            lightObject.OnLightTouched += SetLightDelay;           
        }
        GameManager.Instance.OnGameOver += ResetMode;
    }

    // runs when game mode has ended
    public override void ExitState()
    {
        base.ExitState();
        ResetMode();

        //removes all listeners from light objects
        foreach (LightObject lightObject in LightManager.Instance.allLightObjects)
        {
            lightObject.OnGameStart -= GameSpeed;
            lightObject.OnLightTouched -= SetLightDelay;            
        }
        GameManager.Instance.OnGameOver -= ResetMode;
    }

    /// <summary>
    /// Each time a light is pressed this stops the current timer and begins a new one using the new timer length
    /// </summary>
    private void StartLifeSpanTimer()
    {
        if (lifeSpanCoroutine != null)
        {
            StopCoroutine(lifeSpanCoroutine);
        }

        lifeSpanCoroutine = StartCoroutine(LightLifeSpan(1, 0, lightOnTime));        
    }

    /// <summary>
    /// Used to start the timer for the game mode. 
    /// </summary>
    private void GameSpeed()
    {
        StartCoroutine(LightTurnOffRate(startLength, endLength, duration));
    }

    private void SetLightDelay()
    {        
        float randomNum = Random.Range(shortDelay, longDelay);
        StartCoroutine(StartLightDelay(randomNum));
    }

    /// <summary>
    /// To be called any time the game mode needs resetting.
    /// </summary>
    private void ResetMode()
    {
        currentTime = -1;
        StopAllCoroutines();
        lightOnTime = startLength;        
    }

    /// <summary>
    /// Coroutine to set how long each light will be active for, this time decreases over a period. 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator LightTurnOffRate(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            lightOnTime = Mathf.Lerp(start, end, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lightOnTime = end;
    }


    /// <summary>
    /// When a light turns on this Coroutine starts, setting the amount of time the player has to press the light before they lose. 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator LightLifeSpan(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp between the start and end values based on the elapsed time
            currentTime = Mathf.Lerp(start, end, elapsedTime / time);

            elapsedTime += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait until the next frame
        }
        //If coroutine ends set GameOver. 
        currentTime = - 1;
        GameManager.Instance.InvokeGameOver();
    }

    private IEnumerator  StartLightDelay(float time)
    {
        if (lifeSpanCoroutine != null)
        {
            StopCoroutine(lifeSpanCoroutine);
        }

        LightManager.Instance.playableLightObjects[LightManager.Instance.currentLight].SetLightActive(false);
        foreach (var light in LightManager.Instance.falseLightObjects) 
            light.SetLightFalse(false);
         
        yield return new WaitForSeconds(time);

        LightManager.Instance.playableLightObjects[LightManager.Instance.currentLight].SetLightActive(true);
        foreach (var light in LightManager.Instance.falseLightObjects)
            light.SetLightFalse(true);

        lifeSpanCoroutine = StartCoroutine(LightLifeSpan(1, 0, lightOnTime));
    }   

}
