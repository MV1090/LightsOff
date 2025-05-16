using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputManager: Singleton<InputManager>
{    
    private PlayerInput playerInput;

    private InputAction touchPosition;
    private InputAction touchPress;   
    
    private LightObject lightObject;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;  
    
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        touchPosition = playerInput.actions.FindAction("TouchPosition");
        touchPress = playerInput.actions.FindAction("TouchPress");
        //Debug.Log("InputManager Loaded");
    }

    private void OnEnable()
    {
        //Binds function to be called each time the player presses the screen 
        touchPress.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPress.performed -= TouchPressed;
    }

    /// <summary>
    /// Called each tiem the player touches the screen
    /// </summary>
    /// <param name="context"></param>
    private void TouchPressed(InputAction.CallbackContext context)
    {
        if (MenuManager.Instance.menuLoaded == false)
            return;

        if (GameManager.Instance.gameOver == true)
            return;

        //Gets the position on the screen where the player touched
        Vector2 touchPos = touchPosition.ReadValue<Vector2>();              

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = touchPos            
        };                

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);
        //Debug.Log(results.Count);

        bool hitTarget = false;

        foreach (RaycastResult result in results)
        {
            if (LightManager.Instance.playableLightObjects.Contains(result.gameObject.GetComponent<LightObject>()))
            {
                lightObject = result.gameObject.GetComponent<LightObject>();

                if (lightObject != null)
                {
                    hitTarget = true;   
                                        
                    if (!lightObject.GetIsLightActive())
                    {
                        if (GameManager.Instance.GetStartGame() == false)
                            return;

                        //GameManager.Instance.InvokeGameOver();
                        lightObject.MissedTouch();
                        return;
                    }

                    if(!GameManager.Instance.gameOver)
                    SFXManager.Instance.PlayClickedSound();

                    lightObject.OnTouched();
                    return;
                }
            }
        }

        if (!hitTarget)
        {
            if (GameManager.Instance.GetStartGame() != false)
            {
                lightObject.MissedTouch();
                //GameManager.Instance.InvokeGameOver();
                return;
            }                      
        }

    }   
}

