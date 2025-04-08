using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public float baseHeight = 1920f;
    public float cameraScaler;

    public bool testMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        AdjustCameraSize();

        if (testMode)
            cameraScaler = 60.0f;
        else
            cameraScaler = 225;
    }

    private void Update()
    {
        AdjustCameraSize();
    }
    void AdjustCameraSize()
    {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float aspectRatio = screenWidth / screenHeight;

        // Adjust camera size based on aspect ratio and reference width
        mainCamera.orthographicSize = baseHeight / (cameraScaler * aspectRatio);
    }
}
