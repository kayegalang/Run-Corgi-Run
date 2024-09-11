using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomObjectPlacer : MonoBehaviour
{
    public AudioManager AudioManager;
    public Game Game;
    public GameObject ObjectPrefab;

    protected bool IsWaitingToCreate = false;
    protected float minimumTimeUntilCreate = 1f;
    protected float maximumTimeUntilCreate = 3f;

    private float secondsUntilCreation;
    private Coroutine TimerCouroutine;
    private bool hasReset = false;
    private Vector3[] readoutsCorners;

    public void Update()
    {
        if (Game.IsRunning())
        {
            hasReset = false;
            if (IsWaitingToCreate == false)
            {
                secondsUntilCreation = Random.Range(minimumTimeUntilCreate, maximumTimeUntilCreate);
                TimerCouroutine = StartCoroutine(CountdownUntilCreation());
            }
        }
        else if (!hasReset)
        {
            ResetPlacer();
        }
    }

    IEnumerator CountdownUntilCreation()
    {
        IsWaitingToCreate = true;
        yield return new WaitForSeconds(secondsUntilCreation);
        Place();
    }

    protected virtual void Place()
    {
        GameObject spawnedObject = Instantiate(ObjectPrefab, SpriteTools.RandomLocationWorldSpace(), Quaternion.identity);
        spawnedObject.transform.position = SpriteTools.ConstrainToScreen(spawnedObject.GetComponent<SpriteRenderer>());
        IsWaitingToCreate = false;
        AudioManager.PlayItemSpawnSound();
    }

    private bool IsInsideUIPanel(Vector3 position)
    {
        Vector3 bottomLeft = readoutsCorners[0];
        Vector3 topRight = readoutsCorners[2];

        position = Camera.main.WorldToScreenPoint(position);

        return position.x >= bottomLeft.x && position.x <= topRight.x &&
               position.y >= bottomLeft.y && position.y <= topRight.y;
    }

    private void ResetPlacer()
    {
        IsWaitingToCreate = false;
        if (TimerCouroutine != null)
            StopCoroutine(TimerCouroutine);
        DeleteAllPlacedObjects();
        hasReset = true;
    }

    private void DeleteAllPlacedObjects()
    {
        foreach (GameObject placedObject in GameObject.FindGameObjectsWithTag(ObjectPrefab.tag))
        {
            Destroy(placedObject);
        }
    }
}