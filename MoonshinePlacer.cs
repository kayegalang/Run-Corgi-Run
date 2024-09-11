using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonshinePlacer : RandomObjectPlacer
{
    public void Start()
    {
        minimumTimeUntilCreate = GameParameters.MoonshineMinimumTimeToCreate;
        maximumTimeUntilCreate = GameParameters.MoonshineMaximumTimeToCreate;
    }

    protected override void Place()
    {
        Vector3 position = SpriteTools.RandomTopOfScreenLocationWorldSpace();
        Instantiate(ObjectPrefab, position, Quaternion.identity);
        IsWaitingToCreate = false;
        AudioManager.PlayMoonshineFallingSound();
    }
}
