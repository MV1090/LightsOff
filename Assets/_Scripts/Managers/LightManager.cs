using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] public List<LightObject> allLightObjects = new List<LightObject>();
    [SerializeField] public List<LightObject> playableLightObjects = new List<LightObject>();

    //used to store a reference to the active light index
    int currentLight = 0;

    private void OnEnable()
    {
        //Bind event listener to listen for each time OnLightTouched is called
        //Each time OnLightTouched is called, call activate new light
        foreach (LightObject obj in allLightObjects)
        {
            obj.OnLightTouched += ActivateNewLight;            
            //obj.SetLightActive(false);
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
        //SetPlayableLightObjects(15);             
        Debug.Log(allLightObjects.Count);
    }   

    public void SetLightGrid(int numberOfPlayableLights, float lightSize)
    {
        ResetPlayableLights();
        SetPlayableLightObjects(numberOfPlayableLights);
        SetLightSize(lightSize);
    }
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

    private void ResetPlayableLights()
    {       
        foreach (LightObject obj in playableLightObjects)
        {
            obj.gameObject.SetActive(false);
        }

        playableLightObjects.Clear();
    }

    private void SetLightSize(float size)
    {
        foreach (LightObject obj in playableLightObjects)
        {
            obj.SetLightSize(size);
        }
    }

    // Turn on a new Light
    void ActivateNewLight()
    {
        if (currentLight > playableLightObjects.Count)
            currentLight = 0;

        // Generate a random number 
        int randomNum = Random.Range(0, playableLightObjects.Count);

        //Check random number is different to active light index
        //Not sure on this functionality, should it always be a different light with no repeats? 
        while (randomNum == currentLight)
        {
            randomNum = Random.Range(0, playableLightObjects.Count);
        }

        //Turn off current active light
        TurnOffLight(currentLight);

        //Turn on new light        
        playableLightObjects[randomNum].SetLightActive(true);

        //Store a reference to the current light index
        currentLight = randomNum;
    }
  
    //Turn off current Light
    void TurnOffLight(int activeLight)
    {
        playableLightObjects[activeLight].SetLightActive(false);
    }

    public void AddToLightObjects(LightObject light)
    {
        allLightObjects.Add(light);
    }

    
    
}
