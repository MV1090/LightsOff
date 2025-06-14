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

    private bool firstPlay = true;

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
        firstPlay = PlayerPrefs.GetInt("FirstPlay", firstPlay ? 1 : 0) == 1;
        currentTime = StartTime;
        Score = 0;
    }
    // Update is called once per frame
    void Update()
    {                
       
    }
    public void SetStartGame(bool hasGameStarted)
    {
        startGame = hasGameStarted;

        if (GetFirstPlay() == true)
            SetFirstPlay();
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
        currentTime = 0;      
    }
    public void ResetGame()
    {
        gameOver = false;
        startGame = false;
        currentTime = StartTime;
        Score = 0;
    }

    public void SetFirstPlay()
    {
        firstPlay = false;
        PlayerPrefs.SetInt("FirstPlay", firstPlay ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetFirstPlay() 
    {
        return firstPlay;
    }

}
