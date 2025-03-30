using TMPro;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{

    [Header("Text")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scoreText)
        {
            GameManager.Instance.OnScoreValueChange.AddListener(UpdateScoreText);
            scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOver == true)
            return;
        
        float seconds = Mathf.FloorToInt(GameManager.Instance.GetCurrentTime() % 60);
        float milSeconds = (GameManager.Instance.GetCurrentTime() % 1) * 100;
        timerText.text = string.Format("{0:00}:{1:00}", seconds, milSeconds);
    }
    void UpdateScoreText(int value)
    {
        scoreText.text = "Score: " + value.ToString();
    }
}
