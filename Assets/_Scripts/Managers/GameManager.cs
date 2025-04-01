using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public bool TestMode = true;
    public UnityEvent<int> OnScoreValueChange;
    public event Action OnGameOver;

    [SerializeField] float StartTime; // Set the total time for the countdown    
    public float currentTime;

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
    public void InvokeGameOver()
    {
        gameOver = true;
        OnGameOver?.Invoke();
        startGame = false;
        currentTime = 0;
        Debug.Log("Game Over");
    }
    public void ResetGame()
    {
        gameOver = false;
        currentTime = StartTime;
        Score = 0;
    }
}
