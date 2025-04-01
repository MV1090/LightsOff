using System.Collections;
using UnityEngine;

public class EndLess : BaseGameMode
{
    [SerializeField] float LightOnTime;
    [SerializeField] float startLength;
    [SerializeField] float endLength;
    [SerializeField] float duration;

    [SerializeField] float currentTime;

    private Coroutine currentCoroutine;

    public override void InitState(GameModeManager ctx)
    {
        base.InitState(ctx);
        gameModeManager.EndlessModeSet += ModeStartUp;
    }


    void Start()
    {
                          
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    private void ResetTimer()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Start a new coroutine and store its reference
        currentCoroutine = StartCoroutine(LightLifeSpan(1, 0, LightOnTime));
    }

    private void ModeStartUp()
    {
        foreach (LightObject lightObject in LightManager.Instance.lightObjects)
        {
            lightObject.OnGameStart += () => StartCoroutine(LightTurnOffRate(startLength, endLength, duration));
            lightObject.OnLightTouched += ResetTimer;
            Debug.Log("Subscribed to: " + lightObject.gameObject.name);
        }
    }

    // Coroutine to interpolate between two float values over a duration
    private IEnumerator LightTurnOffRate(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp between the start and end values based on the elapsed time
            LightOnTime = Mathf.Lerp(start, end, elapsedTime / time);

            // You can use this value to update a variable, UI element, etc.
            Debug.Log("Current Value: " + LightOnTime);

            elapsedTime += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait until the next frame
        }

        // Ensure the final value is exactly the end value
        LightOnTime = end;
        Debug.Log("Final Value: " + LightOnTime);
    }

    private IEnumerator LightLifeSpan(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp between the start and end values based on the elapsed time
            currentTime = Mathf.Lerp(start, end, elapsedTime / time);

            // You can use this value to update a variable, UI element, etc.
            Debug.Log("Current Value: " + currentTime);

            elapsedTime += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait until the next frame
        }

        GameManager.Instance.InvokeGameOver();
        // Ensure the final value is exactly the end value
        //currentTime = end;
    }

}
