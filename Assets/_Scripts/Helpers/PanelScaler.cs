//using UnityEditor.Rendering;
using UnityEngine;

public class PanelScaler : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void Update()
    {
       
    }

    void ApplySafeArea()
    {
        // Get the safe area of the screen
        Rect safeArea = Screen.safeArea;

        // Calculate the anchor and size based on the safe area
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        // Convert the anchor points to normalized coordinates
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Apply the anchor points and size to the RectTransform
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.sizeDelta = Vector2.zero; // Reset sizeDelta to ensure the RectTransform fits the safe area
    }
}
