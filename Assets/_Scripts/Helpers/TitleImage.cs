using UnityEngine;
using UnityEngine.UI;
public class TitleImage : MonoBehaviour
{
    public Image displayImage;         
    public Sprite[] lightFrames;       
    public float bpm = 140f;           

    private float beatInterval;        
    private float timer;
    [SerializeField] private int currentFrame;

    void Start()
    {
        beatInterval = (60f / bpm) * 2;
        timer = 0f;
        currentFrame = 0;
        Debug.Log("BeatSyncedAnimation started with " + lightFrames.Length + " frames.");
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= beatInterval)
        {
            timer -= beatInterval; // maintain timing accuracy

            // Cycle to next frame
            currentFrame = (currentFrame + 1) % lightFrames.Length;
            displayImage.sprite = lightFrames[currentFrame];
            Debug.Log("Changed to frame: " + currentFrame);
        }
    }
}
