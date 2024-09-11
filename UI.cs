using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public CanvasGroup StartScreenCanvasGroup;
    public CanvasGroup EndScreenCanvasGroup;
    //public GameTimer GameTimer;
    public Text ScoreText;
    public Text HighScoreText;
    public Text EndScoreText;
    public Text EndHighScoreText;
    //public Text TimeText;

    public void ShowStartScreen()
    {
        CanvasGroupDisplayer.Show(StartScreenCanvasGroup);
    }
    public void HideStartScreen()
    {
        CanvasGroupDisplayer.Hide(StartScreenCanvasGroup);
    }

    public void ShowEndScreen()
    {
        CanvasGroupDisplayer.Show(EndScreenCanvasGroup);
    }

    public void HideEndScreen()
    {
        CanvasGroupDisplayer.Hide(EndScreenCanvasGroup);
    }

    /* public void ShowTime(string time)
    {
        TimeText.text = time;
    }
    */

    public void ShowScore(int score)
    {
        ScoreText.text = "Score: " + score;
        EndScoreText.text = "Score: " + score;
    }

    public void ShowHighScore(int highScore)
    {
        HighScoreText.text = highScore.ToString();
        EndHighScoreText.text = "High Score: " + highScore;
    }
    

    // Update is called once per frame
    void Update()
    {
        ShowScore(ScoreKeeper.GetScore());
        ShowHighScore(ScoreKeeper.LoadHighScore());
        //ShowTime(GameTimer.GetTimeAsString());
    }
}
