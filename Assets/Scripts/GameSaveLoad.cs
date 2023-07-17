using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad
{
    private const string GAME_PREFS_MAX_SCORE = "MAX SCORE";
    public static int maximumScore;

    public static void Save(int movesCount)
    {
        maximumScore = movesCount;
        PlayerPrefs.SetInt(GAME_PREFS_MAX_SCORE, GameSaveLoad.maximumScore);
        PlayerPrefs.Save();
    }

    public static int LoadMovesCount()
    {
       return PlayerPrefs.GetInt(GAME_PREFS_MAX_SCORE);
    }

}
