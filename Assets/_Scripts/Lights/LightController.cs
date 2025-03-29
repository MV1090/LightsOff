using UnityEngine;

public class LightController : MonoBehaviour
{

    [SerializeField] LightObject[] lightObjects;

    //used to store a reference to the active light index
    int currentLight = 0;

    private void OnEnable()
    {
        //Bind event listener to listen out for each time OnLightTouched is called
        //Each time OnLightTouched is called, call activate new light
        foreach (LightObject obj in lightObjects)
        {
            obj.OnLightTouched += ActivateNewLight;
            obj.gameObject.SetActive(false);
        }        
    }

    private void OnDisable()
    {
        foreach (LightObject obj in lightObjects)
        {
            obj.OnLightTouched -= ActivateNewLight;
        }
    }
       
    void Start()
    {
        ActivateNewLight();
        Debug.Log(lightObjects.Length);
    }

    void Update()
    {
        
    }


    // Turn on a new Light
    void ActivateNewLight()
    {        
        // Generate a random number 
        int randomNum = Random.Range(0, lightObjects.Length);

        //Check random number is different to active light index
        //Not sure on this functionality, should it always be a different light with no repeats? 
        while (randomNum == currentLight)
        {
            randomNum = Random.Range(0, lightObjects.Length);
        }

        //Turn off current active light
        TurnOffLight(currentLight);

        //Turn on new light
        lightObjects[randomNum].gameObject.SetActive(true);        

        //Store a reference to the current light index
        currentLight = randomNum;



    }
  
    //Turn off current Light
    void TurnOffLight(int activeLight)
    {
        lightObjects[activeLight].gameObject.SetActive(false);
    }
}
