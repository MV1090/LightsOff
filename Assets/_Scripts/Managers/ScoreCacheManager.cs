using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ScoreData
{
    public string leaderboardsID;
    public int highScore;
}

public class ScoreCacheManager : Singleton<ScoreCacheManager>
{
    private Dictionary<string, int> scoreCache = new Dictionary<string, int>
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


    public void UpDateScoreCache(string leaderboardID, int value)
    {
        if(scoreCache.ContainsKey(leaderboardID))
            scoreCache[leaderboardID] = value;

        Console.WriteLine($"Updated {leaderboardID} to {value}");
        
        Console.WriteLine("\nUpdated Dictionary:");
        foreach (var kvp in scoreCache)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
    
}
