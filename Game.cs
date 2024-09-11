using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public UI UI;
    public AudioManager AudioManager;
    
    public Image[] Lives;
    public Sprite RegularCorgiSprite;
    public Sprite HalfDamagedCorgiSprite;
    public Sprite FullDamagedCorgiSprite;
    
    //public GameTimer GameTimer;
    
    public Corgi Corgi;

    private bool isRunning = false;
    private float livesRemaining;
    public void StartGame()
    {
        ResetLifeUI();
        livesRemaining = 3f;
        ScoreKeeper.SetScore(0);

        isRunning = true;
        UI.HideStartScreen();
        UI.HideEndScreen();
        
        //GameTimer.StartTimer(60);
        
        Corgi.StartGame();
        AudioManager.PlayButtonSelectSound();
        AudioManager.StopMusic();
        AudioManager.PlayRandomGameMusic();
    }

    private void ResetLifeUI()
    {
        for (int i = 0; i < Lives.Length; ++i)
        {
            Lives[i].sprite = RegularCorgiSprite;
        }
    }

    public bool HasGameJustEnded()
    {
        if (isRunning && livesRemaining <= 0)
            return true;
        return false;
    }
    
    // Start is called before the first frame update
    public void Start()
    {
        UI.ShowStartScreen();
        UI.HideEndScreen();
        AudioManager.PlayStartScreenMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasGameJustEnded())
            EndGame();
    }

    public void EndGame()
    {
        //GameTimer.StopTimer();
        UI.ShowEndScreen();
        isRunning = false;
        AudioManager.StopAllSounds();
        AudioManager.PlayEndScreenMusic();
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public void LoseLife()
    {
        livesRemaining--;
        UpdateLifeUI();
    }

    public void GainLife()
    {
        livesRemaining++;
        if (livesRemaining > 3)
        {
            livesRemaining = 3;
        }
        UpdateLifeUI();
    }

        public void LoseHalfLife()
    {
        livesRemaining -= 0.5f;
        UpdateLifeUI();
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < Lives.Length; i++)
        {
            if (i < Mathf.FloorToInt(livesRemaining))
            {
                // The player has full lives in these positions
                Lives[i].sprite = RegularCorgiSprite;
            }
            else if (i < Mathf.CeilToInt(livesRemaining))
            {
                // These positions will show half or full damaged corgis
                if (livesRemaining % 1 == 0.5f)
                {
                    // If there's a half life left
                    Lives[i].sprite = HalfDamagedCorgiSprite;
                }
                else
                {
                    // If there are no remaining lives beyond full lives
                    Lives[i].sprite = FullDamagedCorgiSprite;
                }
            }
            else
            {
                // The remaining positions show fully damaged corgis
                Lives[i].sprite = FullDamagedCorgiSprite;
            }
        }
    }

}
