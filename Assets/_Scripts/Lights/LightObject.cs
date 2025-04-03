using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    //Event action to be called each time a light is touched
    public event Action OnLightTouched;
    public event Action OnGameStart;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    private bool isActive;
    private void Awake()
    {
        LightManager.Instance.AddToLightObjects(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }       
    //Called from input manager when the light is touched
    public void OnTouched()
    {
        if (GameManager.Instance.gameOver == true)
            return;

        //Start game after first light is pressed
        if(GameManager.Instance.GetStartGame() == false)
        {
            GameManager.Instance.SetStartGame(true);
            OnGameStart?.Invoke();
        }         

        //Call OnLightTouched event
        GameManager.Instance.Score++;        
        OnLightTouched?.Invoke();                
    }
    public void SetLightActive(bool _isActive)
    {
        isActive = _isActive;

        if (_isActive)
        {
            spriteRenderer.color = Color.green;           
        }
            
        else if(!_isActive)
        {
            spriteRenderer.color = Color.red;           
        }           
    }
    public bool IsLightActive()
    {
        return isActive;
    }
    public void SetLightSize(float size)
    {
        spriteRenderer.size = new Vector2 (size, size);
        circleCollider.radius = (size/ 2) + 0.25f;
    }
}
