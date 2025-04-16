using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine;


public class LeaderboardManager : Singleton<LeaderboardManager>
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnGameOver += UpdateLeaderboard;
    }

    public async void AddScore(string leaderboardID)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, GameManager.Instance.Score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }
    
    void UpdateLeaderboard()
    {
        switch (GameModeManager.Instance.GetCurrentGameMode())
        {
            case GameModeManager.GameModes.None:
                Debug.Log(GameModeManager.GameModes.None);
                break;

            case GameModeManager.GameModes.Delay:
                Debug.Log(GameModeManager.GameModes.Delay);

                switch(GameTypeManager.Instance.GetGameType())
                {
                    case GameTypeManager.GameType.ThreeXThree:
                        Debug.Log(GameTypeManager.GameType.ThreeXThree);
                        AddScore("D_3X3");
                        break;

                    case GameTypeManager.GameType.FourXFour:
                        Debug.Log(GameTypeManager.GameType.FourXFour);
                        AddScore("D_4X4");
                        break;

                    case GameTypeManager.GameType.FiveXFive:
                        Debug.Log(GameTypeManager.GameType.FiveXFive);
                        AddScore("D_5X5");
                        break;
                }
                break;

            case GameModeManager.GameModes.BeatTheClock:
                Debug.Log(GameModeManager.GameModes.BeatTheClock);

                switch (GameTypeManager.Instance.GetGameType())
                {
                    case GameTypeManager.GameType.ThreeXThree:
                        Debug.Log(GameTypeManager.GameType.ThreeXThree);
                        AddScore("B_T_C_3X3");
                        break;

                    case GameTypeManager.GameType.FourXFour:
                        Debug.Log(GameTypeManager.GameType.FourXFour);
                        AddScore("B_T_C_4X4");
                        break;

                    case GameTypeManager.GameType.FiveXFive:
                        Debug.Log(GameTypeManager.GameType.FiveXFive);
                        AddScore("B_T_C_5X5");
                        break;
                }
                break;

            case GameModeManager.GameModes.Endless:
                Debug.Log(GameModeManager.GameModes.Endless);

                switch (GameTypeManager.Instance.GetGameType())
                {
                    case GameTypeManager.GameType.ThreeXThree:
                        Debug.Log(GameTypeManager.GameType.ThreeXThree);
                        AddScore("E_3X3");
                        break;

                    case GameTypeManager.GameType.FourXFour:
                        Debug.Log(GameTypeManager.GameType.FourXFour);
                        AddScore("E_4X4");
                        break;

                    case GameTypeManager.GameType.FiveXFive:
                        Debug.Log(GameTypeManager.GameType.FiveXFive);
                        AddScore("E_5X5");
                        break;
                }
                break;



        }
    }
}
