using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class InputManager: MonoBehaviour
{
    [SerializeField] GameObject player;

    private PlayerInput playerInput;

    private InputAction touchPosition;
    private InputAction touchPress;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPosition = playerInput.actions.FindAction("TouchPosition");
        touchPress = playerInput.actions.FindAction("TouchPress");
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
        position.z = player.transform.position.z;
        
        player.transform.position = position;
    }   

}

