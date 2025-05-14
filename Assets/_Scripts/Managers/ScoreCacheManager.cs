using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string leaderboardsID;
    public int highScore;
}

[System.Serializable]
public class ScoreListWrapper
{
    public List<ScoreData> scores;
}

public class ScoreCacheManager : Singleton<ScoreCacheManager>
{
    public Dictionary<string, int> scoreCache = new Dictionary<string, int>
    {
        {"D_D_3X3",0},
        {"D_D_4X4",0},
        {"D_D_5X5",0},
        {"C_D_3X3",0},
        {"C_D_4X4",0},
        {"C_D_5X5",0},
        {"E_D_3X3",0},
        {"E_D_4X4",0},
        {"E_D_5X5",0},
        {"D_3X3",0},
        {"D_4X4",0 },
        {"D_5X5",0},
        {"B_T_C_3X3",0},
        {"B_T_C_4X4",0},
        {"B_T_C_5X5",0},
        {"E_3X3",0},
        {"E_4X4",0},
        {"E_5X5",0}
    };

    protected override void Awake()
    {
        base.Awake();
        LoadScores();
    }

    public void SaveScores()
    {
        List<ScoreData> scoreDataList = new List<ScoreData>();

        foreach (var score in scoreCache)
        {
            scoreDataList.Add(new ScoreData() { leaderboardsID = score.Key, highScore = score.Value});
        }

        string json = JsonUtility.ToJson(new ScoreListWrapper() { scores = scoreDataList });

        PlayerPrefs.SetString("cachedScores", json);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        if(PlayerPrefs.HasKey("cachedScores"))
        {
            string json = PlayerPrefs.GetString("cachedScores");
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(json);

            scoreCache.Clear();

            foreach (var score in wrapper.scores)
                scoreCache[score.leaderboardsID] = score.highScore;
                                        
        }
        else
        {
            Debug.Log("\nNo dictionary to load:");
        }
    }

    public async void SyncScoresAfterLogin()
    {
        // Wait until player is authenticated
        while (!Unity.Services.Authentication.AuthenticationService.Instance.IsSignedIn)
        {
            await Task.Yield();
        }

        Debug.Log("[ScoreCacheManager] Player is signed in. Syncing cached scores...");

        if (IsConnectedToInternet())
        {
            foreach (var score in scoreCache)
            {
                LeaderboardManager.Instance.OnlineLeaderboardUpdate(score.Key, score.Value);
                Debug.Log($"Key: {score.Key} Value: {score.Value}");
            }
        }
    }

    public void UpDateScoreCache(string leaderboardID)
    {
        int value = GameManager.Instance.Score;

        if(scoreCache.ContainsKey(leaderboardID))
        {
            if(GameManager.Instance.Score > scoreCache[leaderboardID])
             scoreCache[leaderboardID] = value;
        }            
        //Debug.Log($"Updated {leaderboardID} to {value}");

        Debug.Log("\nUpdated Dictionary:");
        foreach (var kvp in scoreCache)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }        
        SaveScores();
    }
    public bool IsConnectedToInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

}
