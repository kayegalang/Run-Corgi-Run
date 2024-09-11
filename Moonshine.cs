using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonshine : TimedLifespan
{
    public override void Start()
    {
        secondsOnScreen = GameParameters.MoonshineSecondsOnScreen;
        base.Start();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Poop"))
        {
            BlockedByPoop();
        }
    }

    void BlockedByPoop()
    {
        Destroy(gameObject);
        ScoreKeeper.AddToScore(1);
    }
}
