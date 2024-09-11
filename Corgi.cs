using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Corgi : MonoBehaviour
{
    public AudioManager AudioManager;
    public SpriteRenderer CorgiSpriteRenderer;
    public Sprite SoberSprite;
    public Sprite DrunkSprite;
    public Game Game;
    public bool isDrunk = false;
    public bool isPlastered = false;

    private int lastRandomDirection = 1;
    private int randomMoveCountdown = 0;

    public static bool isPlaying = false;
    private bool isMoving;
    private float drunkTimeRemaining;
    private Coroutine soberingCoroutine = null;

    void Start()
    {
        drunkTimeRemaining = 0f;
    }
    public void Update()
    {
        if(HasGameJustEnded())
            ResetCorgi();
        
        if (isPlastered == true)
        {
            MoveRandomly();
        }
    }

    public void StartGame()
    {
        isPlaying = true;
    }

    private bool HasGameJustEnded()
    {
        if (!Game.IsRunning() && isPlaying == true)
        {
            isPlaying = false;
            return true;
        }

        return false;
    }

    public void MoveManually(Vector2 direction)
    {
        if(!isPlastered)
            Move(direction);
    }
    public void Move(Vector2 direction)
    {
        direction.Normalize();
        direction = ApplyDrunkenness(direction);
        
        FaceCorrectDirection(direction);
        
        float xAmount = direction.x * GameParameters.CorgiMoveAmount * Time.deltaTime;
        float yAmount = direction.y * GameParameters.CorgiMoveAmount * Time.deltaTime;
        
        Vector3 moveAmount = new Vector3(xAmount, yAmount, 0f);
        CorgiSpriteRenderer.transform.Translate(moveAmount);
        KeepOnScreen();
    }
    

    private void MoveRandomly()
    {
        int newDirection = lastRandomDirection;

        if (randomMoveCountdown == 0)
        {
            newDirection = Random.Range(1, 5);
            randomMoveCountdown = Random.Range(20, 50);
            lastRandomDirection = newDirection;
        }

        switch (newDirection)
        {
            case 1: //right
                Move(new Vector2(1, 0));
                break;
            case 2: //left
                Move(new Vector2(-1, 0));
                break;
            case 3: //up
                Move(new Vector2(0, 1));
                break;
            case 4: //down
                Move(new Vector2(0, -1));
                break;
        }

        randomMoveCountdown--;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Beer")
        {
            GetDrunk();
            AudioManager.PlayDrunkSound();
        }
        if (col.gameObject.tag == "Pill")
        {
            SoberUp();
            StopCoroutine(WaitToSoberUp());
            AudioManager.PlayPillSound();
        }
        if (col.gameObject.tag == "Bone")
        {
            ScoreKeeper.AddToScore(1);
            print(ScoreKeeper.GetScore());
            AudioManager.PlayBoneSound();
        }
        if (col.gameObject.tag == "Moonshine")
        {
            GetPlastered();
            AudioManager.PlayDrunkSound();
        }
        
        Destroy(col.gameObject);
    }

    public void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            CorgiSpriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            CorgiSpriteRenderer.flipX = true;
        }
            
    }

    private void KeepOnScreen()
    {
        CorgiSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(CorgiSpriteRenderer);
    }

    private void GetDrunk()
    {
        drunkTimeRemaining = GameParameters.TimeDrunk;
        isDrunk = true;
        Inebriate();
        Game.LoseHalfLife();
    }

    IEnumerator WaitToSoberUp()
    {
        while (drunkTimeRemaining > 0)
        {
            Debug.Log(drunkTimeRemaining);
            yield return new WaitForSeconds(1f);
            drunkTimeRemaining -= 1f;
        }

        SoberUp();
    }

    private void SoberUp()
    {
        isDrunk = false;
        isPlastered = false;
        ChangeToSoberSprite();
        drunkTimeRemaining = 0f;
        soberingCoroutine = null;
    }

    private void ChangeToDrunkSprite()
    {
        CorgiSpriteRenderer.sprite = DrunkSprite;
    }

    private void ChangeToSoberSprite()
    {
        CorgiSpriteRenderer.sprite = SoberSprite;
    }

    private Vector2 ApplyDrunkenness(Vector2 direction)
    {
        if (isDrunk || isPlastered)
        {
            direction.x = direction.x * -1;
            direction.y = direction.y * -1;
        }

        return direction;
    }

    private void GetPlastered()
    {
        drunkTimeRemaining = GameParameters.TimePlastered;
        isPlastered = true;
        Inebriate();
        Game.LoseLife();
    }

    private void Inebriate()
    {
        ChangeToDrunkSprite();
        if (soberingCoroutine == null)
        {
            soberingCoroutine = StartCoroutine(WaitToSoberUp());
        }
    }

    private void ResetCorgi()
    {
        SoberUp();
        ResetPosition();
        DestroyAllPoop();
    }

    private void DestroyAllPoop()
    {
        foreach (GameObject placedObject in GameObject.FindGameObjectsWithTag("Poop"))
        {
            Destroy(placedObject);
        }
    }

    private void ResetPosition()
    {
        CorgiSpriteRenderer.transform.position = new Vector3(0f, 0f, 0f);
        CorgiSpriteRenderer.flipX = false;
    }
}
