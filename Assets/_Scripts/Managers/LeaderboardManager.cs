using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    public LeaderboardScoresPage topFiveScores;
    public LeaderboardScores scoresAroundPlayer;
    public LeaderboardEntry playerScore;

    [SerializeField] public bool distractionActive;

    public event Action ScoreToDisplay;
    public event Action noEntryAround;
    public event Action noEntryTop;
    public event Action scoreEntered;

    void Start()
    {
        GameManager.Instance.OnGameOver += UpdateLeaderboard;
    }

    public void SetDistractionActive(bool isActive)
    {
        distractionActive = isActive;
    }
     
    public async Task SubmitLeaderboardData(string leaderboardId, int score)
    {
        try
        {
            // Submit the score
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                leaderboardId,
                score
            );
            Debug.Log($"Score submitted: {JsonConvert.SerializeObject(scoreResponse)}");

            playerScore = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
            Debug.Log($"Player score: {JsonConvert.SerializeObject(playerScore)}");
                        
            // Notify UI
            ScoreToDisplay?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Leaderboard fetch failed: {ex.Message}");
        }
    }

    public async Task FetchAroundLeaderboardData(string leaderboardId)
    {
        try
        {
           scoresAroundPlayer = null;

           scoresAroundPlayer = await LeaderboardsService.Instance.GetPlayerRangeAsync(
               leaderboardId,
               new GetPlayerRangeOptions { RangeLimit = 2 }
           );

            if (scoresAroundPlayer == null || scoresAroundPlayer.Results.Count == 0)
            {
                noEntryAround?.Invoke();
                return;
            }

            Debug.Log($"Scores around player: {JsonConvert.SerializeObject(scoresAroundPlayer)}");
            ScoreToDisplay?.Invoke();
        }
        catch (Exception ex)
        {
            noEntryAround?.Invoke();
            Debug.LogWarning($"Leaderboard fetch failed: {ex.Message}");
        }
    }

    public async Task FetchTopLeaderboardData(string leaderboardId)
    {
        try
        {
            topFiveScores = null;
            
            // Get top scores
            topFiveScores = await LeaderboardsService.Instance.GetScoresAsync(
                leaderboardId,
                new GetScoresOptions { Offset = 0, Limit = 5 }
            );

            if (topFiveScores == null || topFiveScores.Results.Count == 0)
            {
                noEntryTop?.Invoke();
                return;
            }

            Debug.Log($"Top scores: {JsonConvert.SerializeObject(topFiveScores)}");
           
            // Notify UI
            ScoreToDisplay?.Invoke();
        }
        catch (Exception ex)
        {
            noEntryTop?.Invoke();
            Debug.LogWarning($"Leaderboard fetch failed: {ex.Message}");
        }
    }

    public async Task CompareScore(string leaderboardId)
    {
        //if(playerScore!= null)     
        playerScore = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
        scoreEntered.Invoke();
    }

    public void OnlineLeaderboardUpdate(string leaderboardId, int score)
    {
        _ = SubmitLeaderboardData(leaderboardId, score);
    }

    void UpdateLeaderboard()
    {        
        string leaderboardId = GetLeaderboardId();

        if (!string.IsNullOrEmpty(leaderboardId))
        {
            _ = CompareScore(leaderboardId);            
            _ = SubmitLeaderboardData(leaderboardId, GameManager.Instance.Score);            
        }
        else
        {
            Debug.LogWarning("No valid leaderboard ID found.");
        }
    }

    public string GetLeaderboardId()
    {
        var mode = GameModeManager.Instance.GetCurrentGameMode();
        var type = GameTypeManager.Instance.GetGameType();

        if (distractionActive)
        {
            return (mode, type) switch
            {
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.ThreeXThree) => "D_D_3X3",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FourXFour) => "D_D_4X4",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FiveXFive) => "D_D_5X5",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.ThreeXThree) => "C_D_3X3",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FourXFour) => "C_D_4X4",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FiveXFive) => "C_D_5X5",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.ThreeXThree) => "E_D_3X3",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FourXFour) => "E_D_4X4",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FiveXFive) => "E_D_5X5",
                _ => null
            };
        }
        else
        {
            return (mode, type) switch
            {
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.ThreeXThree) => "D_3X3",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FourXFour) => "D_4X4",
                (GameModeManager.GameModes.Delay, GameTypeManager.GameType.FiveXFive) => "D_5X5",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.ThreeXThree) => "B_T_C_3X3",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FourXFour) => "B_T_C_4X4",
                (GameModeManager.GameModes.BeatTheClock, GameTypeManager.GameType.FiveXFive) => "B_T_C_5X5",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.ThreeXThree) => "E_3X3",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FourXFour) => "E_4X4",
                (GameModeManager.GameModes.Endless, GameTypeManager.GameType.FiveXFive) => "E_5X5",
                _ => null
            };
        }
    }
}

