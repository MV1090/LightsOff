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

    public event Action ScoreToDisplay;

    void Start()
    {
        GameManager.Instance.OnGameOver += UpdateLeaderboard;
    }

    public async Task SubmitAndFetchLeaderboardData(string leaderboardId)
    {
        try
        {
            // Submit the score
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                leaderboardId,
                GameManager.Instance.Score
            );
            Debug.Log($"Score submitted: {JsonConvert.SerializeObject(scoreResponse)}");

            // Get the player's specific score
            playerScore = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
            Debug.Log($"Player score: {JsonConvert.SerializeObject(playerScore)}");

            // Get player-range scores
            scoresAroundPlayer = await LeaderboardsService.Instance.GetPlayerRangeAsync(
                leaderboardId,
                new GetPlayerRangeOptions { RangeLimit = 2 }
            );
            Debug.Log($"Scores around player: {JsonConvert.SerializeObject(scoresAroundPlayer)}");

            // Get top scores
            topFiveScores = await LeaderboardsService.Instance.GetScoresAsync(
                leaderboardId,
                new GetScoresOptions { Offset = 0, Limit = 5 }
            );
            Debug.Log($"Top scores: {JsonConvert.SerializeObject(topFiveScores)}");

            // Notify UI
            ScoreToDisplay?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Leaderboard fetch failed: {ex.Message}");
        }
    }

    void UpdateLeaderboard()
    {
        
        string leaderboardId = GetLeaderboardId();

        if (!string.IsNullOrEmpty(leaderboardId))
        {
            // Fire and forget — no need to await here
            _ = SubmitAndFetchLeaderboardData(leaderboardId);
        }
        else
        {
            Debug.LogWarning("No valid leaderboard ID found.");
        }
    }

    string GetLeaderboardId()
    {
        var mode = GameModeManager.Instance.GetCurrentGameMode();
        var type = GameTypeManager.Instance.GetGameType();

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

