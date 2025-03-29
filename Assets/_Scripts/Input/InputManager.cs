using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager: Singleton<InputManager>
{
    [SerializeField] GameObject player;

    private PlayerInput playerInput;

    private InputAction touchPosition;
    private InputAction touchPress;


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
        touchPress.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPress.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>());   
        position.z = Camera.main.nearClipPlane;
        //position.z = player.transform.position.z;        

        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>()), Vector2.zero);
        if(hit2D.collider != null)
        {
            Debug.Log(hit2D.collider.transform.gameObject.name);
        }

    }   
}

