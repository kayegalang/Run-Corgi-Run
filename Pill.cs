using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : TimedLifespan
{
    public override void Start()
    {
        secondsOnScreen = GameParameters.PillSecondsOnScreen;
        base.Start();
    }
}
