using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager: Singleton<InputManager>
{    
    private PlayerInput playerInput;

    private InputAction touchPosition;
    private InputAction touchPress;   
    
    private LightObject lightObject;

    //Overrides singleton Awake function
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        touchPosition = playerInput.actions.FindAction("TouchPosition");
        touchPress = playerInput.actions.FindAction("TouchPress");
        Debug.Log("InputManager Loaded");
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

    //Call each time player touches the screen
    private void TouchPressed(InputAction.CallbackContext context)
    {         
        
        //Gets the position on the screen where the player touched
        Vector3 position = Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>());   
        position.z = Camera.main.nearClipPlane;              

        //Raycast to the area of the screen touched by the player, might need to be adjusted based on the save of the area registered as 'touched' by the player... 
        //Need to test on phone
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>()), Vector2.zero);

        //If object is not a light the game is over
        if(hit2D.collider == null)
        {
            if (GameManager.Instance.GetStartGame() != true)
                return;

            GameManager.Instance.InvokeGameOver();            
            return;
        }

        // Check to see if object tapped is a light
        // If object is a light, call OnTouched function. 
        lightObject = hit2D.collider.GetComponent<LightObject>();
        if (lightObject != null)
        {
            lightObject.OnTouched();
            return;
        }   
    }   
}

