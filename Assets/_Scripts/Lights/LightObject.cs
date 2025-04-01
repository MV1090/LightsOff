using System;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    //Event action to be called each time a light is touched
    public event Action OnLightTouched;

    private void Awake()
    {
        LightManager.Instance.AddToLightObjects(this);
    }
    
    //Called from input manager when the light is touched
    public void OnTouched()
    {
        if (GameManager.Instance.gameOver == true)
            return;

        //Start game after first light is pressed
        if(GameManager.Instance.GetStartGame() == false)
            GameManager.Instance.SetStartGame(true);


        //Call OnLightTouched event
        GameManager.Instance.Score++;
        Debug.Log(GameManager.Instance.Score);
        OnLightTouched?.Invoke();                
    }
}
