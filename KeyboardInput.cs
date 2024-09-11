using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Corgi Corgi;
    public PoopPlacer PoopPlacer;
    public Game Game;
    public AudioManager AudioManager;

    void Update()
    {
        if (Game.IsRunning() && !PauseMenu.isPaused)
        {
            // Check if any movement keys are pressed
            bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

            // Move the Corgi based on input
            if (Input.GetKey(KeyCode.W))
            {
                Corgi.MoveManually(new Vector2(0, 1));
                AudioManager.PlayCorgiRunningSound();
            }

            if (Input.GetKey(KeyCode.A))
            {
                Corgi.MoveManually(new Vector2(-1, 0));
                AudioManager.PlayCorgiRunningSound();
            }

            if (Input.GetKey(KeyCode.S))
            {
                Corgi.MoveManually(new Vector2(0, -1));
                AudioManager.PlayCorgiRunningSound();
            }

            if (Input.GetKey(KeyCode.D))
            {
                Corgi.MoveManually(new Vector2(1, 0));
                AudioManager.PlayCorgiRunningSound();
            }

            // Stop the running sound if not moving
            if (!isMoving)
            {
                AudioManager.StopCorgiRunningSound();
            }

            // Place poop and play sound
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PoopPlacer.Place(Corgi.transform.position);
                AudioManager.PlayCorgiPoopSound();
            }
        }
    }
}

