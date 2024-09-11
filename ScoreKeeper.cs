using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScoreKeeper
{
    private static int score;
    private static int highScore;

    public static int GetScore()
    {
        return score;
    }

    public static void SetScore(int resetScore)
    {
        score = resetScore;
        SaveHighScore();
    }

    public static void AddToScore(int amount)
    {
        score = score + amount;
        SaveHighScore();
    }

    private static void SaveHighScore()
    {
        int highScore = LoadHighScore();
        if (score > highScore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    public static int LoadHighScore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
}
