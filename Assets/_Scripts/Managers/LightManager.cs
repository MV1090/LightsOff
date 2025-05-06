using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] public List<LightObject> allLightObjects = new List<LightObject>();
    [SerializeField] public List<LightObject> playableLightObjects = new List<LightObject>();
    [SerializeField] public List<LightObject> falseLightObjects = new List<LightObject>();
    //used to store a reference to the active light index
    public int currentLight = 0;

    
    List<int> falseLightsIndex = new List<int>();
    int maxFalseLights;
    int falseLightTracker = 0;
    int incrementTime; 
    Coroutine incrementFalseLight;

    public event Action gameEnded;

    private void OnEnable()
    {        
        //Bind event listener to listen for each time OnLightTouched is called
        //Each time OnLightTouched is called, call activate new light
        foreach (LightObject obj in allLightObjects)
        {
            obj.OnLightTouched += ActivateNewLight;           
            obj.gameObject.SetActive(false);
        }      
    }

    private void OnDisable()
    {
        foreach (LightObject obj in allLightObjects)
        {
            obj.OnLightTouched -= ActivateNewLight;
        }
    }
       
    void Start()
    {
        OnEnable();                     
        Debug.Log(allLightObjects.Count);
        GameManager.Instance.OnGameOver += gameOver;        
    }   

    /// <summary>
    /// Runs all the functions needed to adjust lights when grid size changes
    /// </summary>
    /// <param name="numberOfPlayableLights"></param>
    /// <param name="lightSize"></param>
    //Called from GridTypeManager
    public void SetLightGrid(int numberOfPlayableLights)
    {
        ResetPlayableLights();
        SetPlayableLightObjects(numberOfPlayableLights);        
    }

    /// <summary>
    /// When grid size is set this function takes in the new grid size and adds the same number of light objects to the playableLightObjects list.
    /// Then sets those game objects active, these objects are now used for game play. 
    /// </summary>
    /// <param name="numberOfPlayableLights"></param>
    private void SetPlayableLightObjects(int numberOfPlayableLights)
    {
        for(int i = 0; i < numberOfPlayableLights; ++ i)
        {
            playableLightObjects.Add(allLightObjects[i]);
            playableLightObjects[i].gameObject.SetActive(true);
            playableLightObjects[i].SetLightActive(false);

            Debug.Log(playableLightObjects[i] + " " + allLightObjects[i]);
        }

        ActivateNewLight();
    }

    /// <summary>
    /// When grid size changes this makes sure to clear the playable object list before the new one is set, making sure the correct amount of objects are in the list.
    /// </summary>
    public void ResetPlayableLights()
    {       
        foreach (LightObject obj in playableLightObjects)
        {
            obj.gameObject.SetActive(false);
        }

        playableLightObjects.Clear();
    }
    
    /// <summary>
    /// Sets a new light to be active and turns off the current light
    /// </summary>
    void ActivateNewLight()
    {
        //When changing grid size checks to make sure current light isn't bigger than the new grid size, needed to set new grid properly.
        if (currentLight > playableLightObjects.Count -1)
            currentLight = 0;

        // Generate a random number 
        int randomNum = UnityEngine.Random.Range(0, playableLightObjects.Count);

        //Check random number is different to active light index
        //Not sure on this functionality, should it always be a different light with no repeats? 
        while (randomNum == currentLight)
        {
            randomNum = UnityEngine.Random.Range(0, playableLightObjects.Count);
        }

        //Turn off current active light
        TurnOffLight(currentLight);

        //Store a reference to the current light index
        currentLight = randomNum;

        //Turn on new light        
        playableLightObjects[currentLight].SetLightActive(true);        
    }
  
    void ActivateFalseLights()
    {
        falseLightObjects.Clear();

        if (falseLightsIndex != null)
        {
            for (int i = 0; i < falseLightsIndex.Count; i++)
            {
                int index = falseLightsIndex[i];

                if (index == currentLight)
                    continue;

                if (index > playableLightObjects.Count -1)
                    continue;

                playableLightObjects[index].SetLightFalse(false);               
            }

            falseLightsIndex.Clear();            
        }
               
        //turn on new false lights
        for(int i = 0; i < falseLightTracker; i++)
        {
            // Generate a random number 
            int randomNum = UnityEngine.Random.Range(0, playableLightObjects.Count);

            //Check random number is different to active light index
            //Not sure on this functionality, should it always be a different light with no repeats? 
            while(randomNum == currentLight || falseLightsIndex.Contains(randomNum))
            {
                randomNum = UnityEngine.Random.Range(0, playableLightObjects.Count);
            }


            Debug.Log(randomNum);
            playableLightObjects[randomNum].SetLightFalse(true);            
            falseLightsIndex.Add(randomNum);
            falseLightObjects.Add(playableLightObjects[randomNum]);
        }
        Debug.Log("Distraction Mode on");
    }

    public void ToggleDistraction(bool isOn)
    {
        foreach (LightObject obj in allLightObjects)
        {
            obj.OnLightTouched -= ActivateFalseLights;
            obj.OnGameStart -= StartFalseTimer;
            if (isOn)
            {
                obj.OnLightTouched += ActivateFalseLights;
                obj.OnGameStart += StartFalseTimer;
            }
        }

        Debug.Log("Distraction Mode " + (isOn ? "On" : "Off"));
    }
    /// <summary>
    /// Turn off current Light
    /// </summary>
    /// <param name="activeLight"></param>
    void TurnOffLight(int activeLight)
    {
        playableLightObjects[activeLight].SetLightActive(false);
    }

    /// <summary>
    /// Adds light object to the list.
    /// </summary>
    /// <param name="light"></param>
    //Called from light objects at awake to store all objects in the list.
    public void AddToLightObjects(LightObject light)
    {
        allLightObjects.Add(light);
    }

    private void gameOver()
    {        
        StopIncrementing();
        falseLightObjects.Clear();
        falseLightTracker = 0;

        if (playableLightObjects == null)
            return;

        if (GameModeManager.Instance.IsModeSet(GameModeManager.GameModes.BeatTheClock) && GameManager.Instance.currentTime <= 0)
            StartCoroutine(FlashLight(playableLightObjects[currentLight].activeColour));
        
        else
            StartCoroutine(FlashLight(playableLightObjects[currentLight].warningColour));
    }

    private IEnumerator FlashLight(Color lightColour)
    {
        //playableLightObjects[currentLight].SetLightActive(true);

        foreach (LightObject obj in playableLightObjects)
        {
            obj.Image.color = obj.warningColour;
            obj.SetLightActive(true);            
        }

        for (int i = 0; i < 7; i++)
        {           
            foreach(LightObject obj in playableLightObjects)
            {

                if (obj.Image.color == obj.activeColour)
                {
                    obj.Image.color = obj.warningColour;
                }
                else
                {
                    obj.Image.color = obj.activeColour;
                }

            }
            yield return new WaitForSeconds(0.2f);

            if(i == 5)
            {
                gameEnded?.Invoke();
            }
        }
        yield return new WaitForSeconds(0.5f);

        playableLightObjects[currentLight].SetLightActive(true);             
    }

    void StartFalseTimer()
    {
        incrementFalseLight = StartCoroutine(FalseLightScaler());
    }
    public void StopIncrementing()
    {
        if (incrementFalseLight != null)
        {
            StopCoroutine(incrementFalseLight);
        }
    }
    private IEnumerator FalseLightScaler()
    {
        maxFalseLights = (GameTypeManager.Instance.gridSize/ 3 * 2);
        incrementTime = 8 - (int)Math.Sqrt(GameTypeManager.Instance.gridSize);

        Debug.Log("increment time: " + incrementTime);
        while (falseLightTracker < maxFalseLights)
        {
            yield return new WaitForSeconds(incrementTime);

            falseLightTracker++;
        }    
    }

}
