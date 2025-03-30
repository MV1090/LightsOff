using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public bool TestMode = true;
    public UnityEvent<int> OnScoreValueChange;

    [SerializeField] float StartTime; // Set the total time for the countdown    
    private float currentTime;

    private bool startGame = false;
    public bool gameOver = false;

    private int _score = 0;
    public int Score
    {
        get => _score;

        set
        {
            _score = value;

            if (TestMode) Debug.Log("Score has been set to: " + _score.ToString());

            OnScoreValueChange?.Invoke(_score);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = StartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
            return;

        if (startGame == true)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                currentTime = 0;
                gameOver = true;
            }
                       
                        
        }
    }

    public void SetStartGame(bool hasGameStarted)
    {
        startGame = hasGameStarted; 
    }

    public bool GetStartGame()
    {
        return startGame;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
