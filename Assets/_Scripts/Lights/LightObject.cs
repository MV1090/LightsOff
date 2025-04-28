using System;
using UnityEngine;
using UnityEngine.UI;

public class LightObject : MonoBehaviour
{
    //Event action to be called each time a light is touched
    public event Action OnLightTouched;
    public event Action OnGameStart;

    public Image Image;
    private CircleCollider2D circleCollider;

    [SerializeField]private bool isActive;
        
    public Color activeColour = new Color32(57,248,48,255);
    public Color warningColour = new Color32(219,26,14,255);
    public readonly Color blank = new Color32(87, 87, 87, 255);

    private void Awake()
    {
        LightManager.Instance.AddToLightObjects(this);
        //spriteRenderer = GetComponent<SpriteRenderer>();
        Image = GetComponent<Image>();
        circleCollider = GetComponent<CircleCollider2D>();

        ImageColour(blank);
    }

    public void ImageColour(Color color)
    {        
         Image.color = color;       
    }
 
    /// <summary>
    /// Updates game manager score and broadcast to all listeners that the light has been touched. 
    /// </summary>
    //Called from input manager when the light is touched
    //Should on invoke on touched event, move all game manager logic into game manager and set up a listener to run the onTouched is called instead. 
    public void OnTouched()
    {       
        if (GameManager.Instance.gameOver == true)
            return;

        //Start game after first light is pressed
        if(GameManager.Instance.GetStartGame() == false)
        {
            GameManager.Instance.SetStartGame(true);
            OnGameStart?.Invoke();
            OnLightTouched?.Invoke();
            return;
        }         

        //Call OnLightTouched event
        GameManager.Instance.Score++;        
        OnLightTouched?.Invoke();                
    }

    /// <summary>
    /// Called when the state of the light has changed.
    /// </summary>
    /// <param name="_isActive"></param>
    public void SetLightActive(bool _isActive)
    {
        isActive = _isActive;

        if (_isActive)
        {
            ImageColour(activeColour);
        }

        else if (!_isActive)
        {        
            ImageColour(blank);
        }
    }

    /// <summary>
    /// Getter for the light state
    /// </summary>
    /// <returns></returns>
    public bool GetIsLightActive()
    {
        return isActive;
    }  
    
}
