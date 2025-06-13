using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float amplitude = 10f;     
    public float frequency = 1f;     
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        rectTransform.anchoredPosition = originalPosition + new Vector2(0, yOffset);
    }
}
