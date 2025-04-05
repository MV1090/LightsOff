using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static MenuManager;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] public List<LightObject> allLightObjects = new List<LightObject>();
    [SerializeField] public List<LightObject> playableLightObjects = new List<LightObject>();

    //used to store a reference to the active light index
    public int currentLight = 0;

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
    public void SetLightGrid(int numberOfPlayableLights, float lightSize)
    {
        ResetPlayableLights();
        SetPlayableLightObjects(numberOfPlayableLights);
        SetLightSize(lightSize);
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
    private void ResetPlayableLights()
    {       
        foreach (LightObject obj in playableLightObjects)
        {
            obj.gameObject.SetActive(false);
        }

        playableLightObjects.Clear();
    }

    /// <summary>
    /// Sets all lights in playable object list to be the same size.
    /// </summary>
    /// <param name="size"></param>
    private void SetLightSize(float size)
    {
        foreach (LightObject obj in playableLightObjects)
        {
            obj.SetLightSize(size);
        }
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
        int randomNum = Random.Range(0, playableLightObjects.Count);

        //Check random number is different to active light index
        //Not sure on this functionality, should it always be a different light with no repeats? 
        while (randomNum == currentLight)
        {
            randomNum = Random.Range(0, playableLightObjects.Count);
        }

        //Turn off current active light
        TurnOffLight(currentLight);

        //Store a reference to the current light index
        currentLight = randomNum;

        //Turn on new light        
        playableLightObjects[currentLight].SetLightActive(true);        
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
        StartCoroutine(FlashLight());
    }

    private IEnumerator FlashLight()
    {
        playableLightObjects[currentLight].SetLightActive(false);

        for (int i = 0; i < 6; i++)
        {
            foreach(LightObject obj in playableLightObjects)
            {
                if (obj.spriteRenderer.color.a == 1)
                    obj.spriteRenderer.color = new Color(0, 0, 0, 0);
                else
                    obj.spriteRenderer.color = Color.red;
            }
            yield return new WaitForSeconds(0.2f);
        }

        playableLightObjects[currentLight].SetLightActive(true);
        MenuManager.Instance.SetActiveMenu(MenuStates.EndGameMenu);
        
    }    
}
