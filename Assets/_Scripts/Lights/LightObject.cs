using System;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    //Event action to be called each time a light is touched
    public event Action OnLightTouched;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called from input manager when the light is touched
    public void OnTouched()
    {        
        //Call OnLightTouched event
        OnLightTouched?.Invoke();                
    }
}
