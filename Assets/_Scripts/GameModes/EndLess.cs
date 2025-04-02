using System.Collections;
using UnityEngine;

public class EndLess : BaseGameMode
{
    [SerializeField] float lightOnTime;
    [SerializeField] float startLength;
    [SerializeField] float endLength;
    [SerializeField] float duration;
    [SerializeField] float currentTime;

    private Coroutine currentCoroutine;

    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameMode = GameModeManager.GameModes.Endless;
    }

    public override void EnterState()
    {
        base.EnterState();

        foreach (LightObject lightObject in LightManager.Instance.lightObjects)
        {
            lightObject.OnGameStart += TurnOffLight;
            lightObject.OnLightTouched += StartLifeSpanTimer;
            GameManager.Instance.OnGameOver += ResetMode;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        ResetMode();

        foreach (LightObject lightObject in LightManager.Instance.lightObjects)
        {
            lightObject.OnGameStart -= TurnOffLight;
            lightObject.OnLightTouched -= StartLifeSpanTimer;
            GameManager.Instance.OnGameOver -= ResetMode;
        }
    }

    private void StartLifeSpanTimer()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        
        currentCoroutine = StartCoroutine(LightLifeSpan(1, 0, lightOnTime));
        Debug.Log(lightOnTime + " " + currentTime);
    }

    private void TurnOffLight()
    {
        StartCoroutine(LightTurnOffRate(startLength, endLength, duration));
    }

    private void ResetMode()
    {
        StopAllCoroutines();
        lightOnTime = startLength;        
    }

    // Coroutine to interpolate between two float values over a duration
    private IEnumerator LightTurnOffRate(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp between the start and end values based on the elapsed time
            lightOnTime = Mathf.Lerp(start, end, elapsedTime / time);            

            elapsedTime += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait until the next frame
        }        
        lightOnTime = end;        
    }

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
        GameManager.Instance.InvokeGameOver();       
    }

}
