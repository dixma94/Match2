using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad
{
    private const string GAME_PREFS_MAX_SCORE = "MAX SCORE";
    public int maximumScore;

    public GameSaveLoad()
    {
        maximumScore = PlayerPrefs.GetInt(GAME_PREFS_MAX_SCORE);
    }

    public void Save(int movesCount)
    {
        if (movesCount > maximumScore)
        {
            maximumScore = movesCount;
            PlayerPrefs.SetInt(GAME_PREFS_MAX_SCORE, maximumScore);
            PlayerPrefs.Save();
        }
    }
   



}
