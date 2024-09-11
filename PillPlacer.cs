using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPlacer : RandomObjectPlacer
{
    public void Start()
    {
        minimumTimeUntilCreate = GameParameters.PillMinimumTimeToCreate;
        maximumTimeUntilCreate = GameParameters.PillMaximumTimeToCreate;
    }
}
