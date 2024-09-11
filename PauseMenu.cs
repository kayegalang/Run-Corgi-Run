using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public Game Game;

    public AudioSource SoundEffects;
    public AudioSource Music;
    public AudioSource CorgiRunningSound;

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        Pause.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Corgi.isPlaying)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        
        SoundEffects.Pause();
        CorgiRunningSound.Pause();
        Music.volume = 0.3f;
    }

    public void ResumeGame()
    {
        CorgiRunningSound.Play();
        SoundEffects.Play();
        
        Pause.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        Music.volume = 0.5f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Game.EndGame();
        Game.Start();
        isPaused = false;
        Pause.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
